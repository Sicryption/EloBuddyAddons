using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using UnsignedEvade;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

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
            if(WindWallMenu["WW"].Cast<CheckBox>().CurrentValue && !WindWallMenu.Get<CheckBox>("WWGOT").CurrentValue)
                TryWindWall();
        }
        
        public static void OnGameLoad()
        {
            WindWallMenu = Program.menu.AddSubMenu("Wind Wall Menu", "WWM");
            WindWallMenu.Add("WW", new CheckBox("Auto-Use Wind Wall"));
            WindWallMenu.Add("WWGOT", new CheckBox("GameOnTick (Increased FPS)"));
            WindWallMenu.Add("WWD", new CheckBox("Debug"));
            WindWallMenu.Add("WWCN", new CheckBox("Use Champion Names"));
            WindWallMenu.Get<CheckBox>("WWCN").OnValueChange += ToggleNameFormat;
            WindWallMenu.Get<CheckBox>("WWGOT").OnValueChange += ToggleUpdateFormat;
            WindWallMenu.AddGroupLabel("Enemy Skillshot Toggles");
            foreach (AIHeroClient client in EntityManager.Heroes.Enemies)
                foreach (SpellInfo info in SpellDatabase.SpellList)
                {
                    if (info.ChampionName == client.ChampionName)
                    {
                        EnemyProjectileInformation.Add(info);
                        WindWallMenu.Add("WWBN" + info.SpellName, new CheckBox(info.SpellName));
                    }
                }
            ToggleNameFormat(WindWallMenu.Get<CheckBox>("WWCN"), null);
        }

        public static void OnCreate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient)
            {
                ProjectileList.Add(missile);
                if (DebugMode)
                {
                    //string spellName = missile.SpellCaster.Spellbook.GetSpell(SpellSlot.E).Name;
                    //string spellName2 = missile.SpellCaster.Spellbook.GetSpell(SpellSlot.W).Name;
                    string misName = missile.SData.Name;

                    Chat.Print("Projectile: " + misName + " has been created.");// Spell Name: " + spellName);
                    //Chat.Print("Projectile: " + misName + " has been created. Spell Name: " + spellName + ", " + spellName2);

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
                            if(DebugMode)
                                Chat.Print("Attempt to windwall");
                            Program.W.Cast(_Player.Position.Extend(missile.Position, Program.W.Range).To3DWorld());
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
            //projectile came from ally
            //if enemy is not a champion
            //if projectile name = info's spell name
            //if player is out of range by 1.5x the amount (just so you dont walk into it)
            if (!missile.SpellCaster.IsEnemy ||
                missile.SpellCaster.Type != GameObjectType.AIHeroClient ||
                missile.SData.Name != info.MissileName ||
                missile.Distance(_Player) >= Program.W.Range)
                return false;

            //check if checkbox for spell is enabled
            if (WindWallMenu.Get<CheckBox>("WWBN" + info.SpellName) != null)
                return WindWallMenu.Get<CheckBox>("WWBN" + info.SpellName).CurrentValue;
            return false;
        }

        private static void ToggleNameFormat(ValueBase sender, EventArgs args)
        {
            if (sender.Cast<CheckBox>().CurrentValue)
            {
                foreach(SpellInfo info in EnemyProjectileInformation)
                    WindWallMenu.Get<CheckBox>("WWBN" + info.SpellName).DisplayName = info.ChampionName + "'s " + info.Slot.ToString();
            }
            else
            {
                foreach (SpellInfo info in EnemyProjectileInformation)
                    WindWallMenu.Get<CheckBox>("WWBN" + info.SpellName).DisplayName = info.MissileName;
            }
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
