using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using UnsignedEvade;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

/*
    E away from linear skillshots
    Add is cc in spell info
*/
namespace UnsignedYasuo
{
    class WindWall
    {
        public static List<MissileClient> ProjectileList = new List<MissileClient>();
        public static List<SpellInfo> EnemyProjectileInformation = new List<SpellInfo>();
        public static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static AIHeroClient _Player = Program._Player;
        public static Menu WindWallMenu;
        public static bool DebugMode
        {
            get
            {
                return WindWallMenu.Get<CheckBox>("WWD").CurrentValue;
            }
        }
        
        public static void GameOnTick(EventArgs args)
        {
            if (WindWallMenu["WW"].Cast<CheckBox>().CurrentValue && WindWallMenu.Get<CheckBox>("WWGOT").CurrentValue)
                TryWindWall();
        }
        public static void GameOnUpdate(EventArgs args)
        {
            if (WindWallMenu["WW"].Cast<CheckBox>().CurrentValue && !WindWallMenu.Get<CheckBox>("WWGOT").CurrentValue)
                TryWindWall();
        }
        
        public static void OnGameLoad()
        {
            WindWallMenu = Program.menu.AddSubMenu("Wind Wall Menu", "WWM");
            WindWallMenu.Add("WW", new CheckBox("Auto-Use Wind Wall"));
            WindWallMenu.Add("WWGOT", new CheckBox("GameOnTick (Increased FPS)"));
            WindWallMenu.Add("WWD", new CheckBox("Debug", false));
            WindWallMenu.Add("WWCN", new CheckBox("Use Champion Names"));
            WindWallMenu.Get<CheckBox>("WWCN").OnValueChange += ToggleNameFormat;
            WindWallMenu.Get<CheckBox>("WWGOT").OnValueChange += ToggleUpdateFormat;
            WindWallMenu.AddGroupLabel("Enemy Projectile Toggles");
            foreach (AIHeroClient client in EntityManager.Heroes.Enemies)
                foreach (SpellInfo info in SpellDatabase.SpellList)
                {
                    if (info.ChampionName == client.ChampionName)
                    {
                        EnemyProjectileInformation.Add(info);
                        WindWallMenu.Add("WWBN" + info.MissileName, new CheckBox(info.SpellName));
                    }
                }
            ToggleNameFormat(WindWallMenu.Get<CheckBox>("WWCN"), null);
            ToggleUpdateFormat(WindWallMenu.Get<CheckBox>("WWGOT"), null);
        }

        public static void OnCreate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient)
            {
                ProjectileList.Add(missile);
                if (DebugMode)
                {
                    Spellbook SpellBook = missile.SpellCaster.Spellbook;
                    string misName = missile.SData.Name;

                    Chat.Print("Projectile: " + misName + " has been created.");// Spell Name: " + spellName);
                    Chat.Print("Q Name: " + SpellBook.GetSpell(SpellSlot.Q).Name + ", W Name: " + SpellBook.GetSpell(SpellSlot.W).Name);
                    //Chat.Print("E Name: " + SpellBook.GetSpell(SpellSlot.E).Name + ", R Name: " + SpellBook.GetSpell(SpellSlot.R).Name);

                }
            }
        }

        public static void OnDelete(GameObject obj, EventArgs args)
        {
            if (obj == null)
                return;

            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient &&
                ProjectileList.Contains(missile))
            {
                if (DebugMode)
                    Chat.Print("Projectile: " + missile.SData.Name + " has been destroyed.");
                ProjectileList.Remove(missile);
            }
        }
        
        public static void OnUpdate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient &&
                ProjectileList.Contains(missile))
            {
                ProjectileList.Remove(missile);
                ProjectileList.Add(missile);
            }
        }
        
        private static void TryWindWall()
        {
            if (Program.W.IsReady() && Program.W.IsLearned)
                foreach (MissileClient missile in ProjectileList)
                    foreach (SpellInfo info in EnemyProjectileInformation)
                        if (ShouldWindWall(missile, info) && CollisionCheck(missile, info))
                        {
                            if (DebugMode)
                                Chat.Print("Attempt to windwall");

                            if (info.ChannelType == SpellDatabase.ChannelType.None)
                                Program.W.Cast(_Player.Position.Extend(missile.Position, Program.W.Range).To3DWorld());
                            else
                                Program.W.Cast(_Player.Position.Extend(missile.StartPosition, Program.W.Range).To3DWorld());
                        }
        }

        public static bool CollisionCheck(MissileClient missile, SpellInfo info)
        {
            bool variable =  Prediction.Position.Collision.LinearMissileCollision(
                _Player, missile.Position.To2D(), missile.StartPosition.To2D().Extend(missile.EndPosition, info.Range),
                info.MissileSpeed, info.Width, info.Delay);

            return variable;
        }

        public static bool ShouldWindWall(MissileClient missile, SpellInfo info)
        {
            //checks if:
            //if projectile name = info's spell name
            //if player is within W range (therefore W will force block it)

            if (missile.SpellCaster.Name != "Diana")
                if (missile.SData.Name != info.MissileName ||
                    !missile.IsInRange(_Player, Program.W.Range * 2))
                    return false;
                else if (missile.SpellCaster.Name == "Diana" &&
                    !missile.SData.Name.Contains(info.MissileName))
                    return false;

            //doesnt seem to work
            //checks if the ability is a lock on projectile and the target is me
            if (info.ProjectileType == SpellDatabase.ProjectileType.LockOnProjectile
                && missile.Target != _Player)
                return false;



            //checks if channeling ability is too close to player
            if (info.ChannelType != SpellDatabase.ChannelType.None)
                //if enemy skillshot is far enough away and E is ready continue. else return false
                if (!HandleChannelingSpells(missile, info))
                    return false;

            //check if checkbox for spell is enabled
            if (WindWallMenu.Get<CheckBox>("WWBN" + info.MissileName) != null)
                return WindWallMenu.Get<CheckBox>("WWBN" + info.MissileName).CurrentValue;
            return false;
        }

        public static bool HandleChannelingSpells(MissileClient missile, SpellInfo info)
        {
            //returning if player should windwall
            
            if (missile.SpellCaster.Position.IsInRange(_Player, Program.W.Range)
                && Program.E.IsReady())
            {
                //if can dash though enemy to evade, do so
                if ((info.ChannelType == SpellDatabase.ChannelType.Linear
                    || info.ChannelType == SpellDatabase.ChannelType.Cone)
                    && missile.SpellCaster.IsInRange(_Player, Program.E.Range)
                    && !missile.SpellCaster.HasBuff("YasuoDashWrapper"))
                {
                    Program.E.Cast(missile.SpellCaster);
                    return false;
                }
                else if(info.ChannelType == SpellDatabase.ChannelType.Circular
                    && missile.SpellCaster.IsInRange(_Player, Program.E.Range)
                    && !missile.SpellCaster.HasBuff("YasuoDashWrapper"))
                {
                    Program.W.Cast(_Player.Position.Extend(missile.StartPosition, Program.W.Range).To3DWorld());
                    Program.E.Cast(missile.SpellCaster);
                    return false;
                }
                //if enemy is in w range and dashing through them is unavailalble or wont help, dash to nearby enemies to block it.
                else
                {
                    var target = FindEnemyToBlockChannelingSpell(missile, info);
                    if (target != null)
                        Program.E.Cast(target);
                }
                
                return false;
            }
            //is e is not ready, it didnt block it so return no
            else if (!Program.E.IsReady())
                return false;
            //outside of Wind Wall range, so it will be blocked
            else
                return true;
        }

        public static Obj_AI_Base FindEnemyToBlockChannelingSpell(MissileClient missile, SpellInfo info)
        {
            Obj_AI_Base target = ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy
                && a.IsInRange(_Player, Program.E.Range)
                && !a.IsDead
                && !a.IsInvulnerable
                && !a.HasBuff("YasuoDashWrapper")
                && !YasuoCalcs.IsUnderTurret(YasuoCalcs.GetDashingEnd(a))
                && YasuoCalcs.GetDashingEnd(a).IsInRange(missile.StartPosition, Program.W.Range)).FirstOrDefault();

            return target;
        }
        private static void ToggleNameFormat(ValueBase sender, EventArgs args)
        {
            if (sender.Cast<CheckBox>().CurrentValue)
                foreach(SpellInfo info in EnemyProjectileInformation)
                    WindWallMenu.Get<CheckBox>("WWBN" + info.MissileName).DisplayName = info.ChampionName + "'s " + info.Slot.ToString();
            else
                foreach (SpellInfo info in EnemyProjectileInformation)
                    WindWallMenu.Get<CheckBox>("WWBN" + info.MissileName).DisplayName = info.MissileName;
        }
        private static void ToggleUpdateFormat(ValueBase sender, EventArgs args)
        {
            if (sender.Cast<CheckBox>().CurrentValue)
                sender.DisplayName = "GameOnTick (Increased FPS)";
            else
                sender.DisplayName = "GameOnUpdate (Increased Performance)";
        }
    }
}
