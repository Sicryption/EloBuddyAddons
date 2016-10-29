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
*/
namespace UnsignedYasuo
{
    class WindWall
    {
        public static List<MissileClient> ProjectileList = new List<MissileClient>();
        public static List<SpellInfo> EnemyProjectileInformation = new List<SpellInfo>();
        public static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static AIHeroClient Yasuo = Program.Yasuo;
        public static Menu WindWallMenu;

        enum CCTypeComboBoxOptions
        {
            BlockAll = 0,
            BlockCC = 1,
            BlockCCExcludingSlows = 2,
            Custom = 3
        }

        public static void GameOnTick(EventArgs args)
        {
            if (MenuHandler.GetCheckboxValue(WindWallMenu, "Use Wind Wall") && MenuHandler.GetCheckboxValue(WindWallMenu, "Increase FPS"))
                TryWindWall();
        }
        public static void GameOnUpdate(EventArgs args)
        {
            if(MenuHandler.GetCheckboxValue(WindWallMenu, "Use Wind Wall") && !MenuHandler.GetCheckboxValue(WindWallMenu, "Increase FPS"))
                TryWindWall();
        }
        public static void Initialize()
        {
            WindWallMenu = MenuHandler.AddSubMenu(MenuHandler.mainMenu, "Wind Wall");
            MenuHandler.AddCheckboxes(ref WindWallMenu, "Use Wind Wall", "Increase FPS", "Debug_false", "Use Champion Names");
            WindWallMenu.AddSeparator(1);
            ComboBox WWCCComboBox = MenuHandler.AddComboBox(WindWallMenu, "Spells To Block: ", 2, "Block All", "Block CC", "Block CC but Slows", "Custom");
            
            WindWallMenu.AddGroupLabel("Enemy Projectile Toggles");
            foreach (AIHeroClient client in EntityManager.Heroes.Enemies)
                foreach (SpellInfo info in SpellDatabase.SpellList)
                {
                    if (info.ChampionName == client.ChampionName)
                    {
                        EnemyProjectileInformation.Add(info);
                        MenuHandler.AddCheckbox(ref WindWallMenu, "WindWallSpell."+info.SpellName+".WindWallMissile."+info.MissileName);
                    }
                }

            Game.OnTick += GameOnTick;
            Game.OnUpdate += GameOnUpdate;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDelete += OnDelete;
            Obj_AI_Base.OnUpdatePosition += OnUpdate;
            CheckBox ChampionNames = MenuHandler.GetCheckbox(WindWallMenu, "Use Champion Names");
            ChampionNames.OnValueChange += ToggleNameFormat;
            WWCCComboBox.OnValueChange += ToggleBlockFormat;
            ToggleBlockFormat(WWCCComboBox, null);
            ToggleNameFormat(ChampionNames, null);
        }
        public static void OnCreate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient)
                ProjectileList.Add(missile);
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
                            if (info.ChannelType == SpellDatabase.ChannelType.None)
                                Program.W.Cast(Yasuo.Position.Extend(missile.Position, Program.W.Range).To3DWorld());
                            else
                                Program.W.Cast(Yasuo.Position.Extend(missile.StartPosition, Program.W.Range).To3DWorld());
                        }
        }

        public static bool CollisionCheck(MissileClient missile, SpellInfo info)
        {
            bool variable =  Prediction.Position.Collision.LinearMissileCollision(
                Yasuo, missile.StartPosition.To2D(), missile.StartPosition.To2D().Extend(missile.EndPosition, info.Range),
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
                    !missile.IsInRange(Yasuo, Program.W.Range * 2))
                    return false;
                else if (missile.SpellCaster.Name == "Diana" &&
                    !missile.SData.Name.Contains(info.MissileName))
                    return false;
           
            //doesnt seem to work
            //checks if the ability is a lock on projectile and the target is me
            if (info.ProjectileType == SpellDatabase.ProjectileType.LockOnProjectile
                && missile.Target != Yasuo)
                return false;

            //checks if channeling ability is too close to player
            if (info.ChannelType != SpellDatabase.ChannelType.None)
                //if enemy skillshot is far enough away and E is ready continue. else return false
                if (!HandleChannelingSpells(missile, info))
                    return false;

            CheckBox toggleBox = MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName);
            //check if checkbox for spell is enabled
            if ((toggleBox != null && toggleBox.IsVisible) || MenuHandler.GetComboBoxText(WindWallMenu, "Spells To Block: ") == "Block All")
                return toggleBox.CurrentValue;
            return false;
        }

        public static bool HandleChannelingSpells(MissileClient missile, SpellInfo info)
        {
            //returning if player should windwall
            
            if (missile.SpellCaster.Position.IsInRange(Yasuo, Program.W.Range)
                && Program.E.IsReady())
            {
                //if can dash though enemy to evade, do so
                if ((info.ChannelType == SpellDatabase.ChannelType.Linear
                    || info.ChannelType == SpellDatabase.ChannelType.Cone)
                    && missile.SpellCaster.IsInRange(Yasuo, Program.E.Range)
                    && !missile.SpellCaster.HasBuff("YasuoDashWrapper"))
                {
                    Program.E.Cast(missile.SpellCaster);
                    return false;
                }
                else if(info.ChannelType == SpellDatabase.ChannelType.Circular
                    && missile.SpellCaster.IsInRange(Yasuo, Program.E.Range)
                    && !missile.SpellCaster.HasBuff("YasuoDashWrapper"))
                {
                    Program.W.Cast(Yasuo.Position.Extend(missile.StartPosition, Program.W.Range).To3DWorld());
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
                && a.IsInRange(Yasuo, Program.E.Range)
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
                    MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).DisplayName = info.ChampionName + "'s " + info.Slot.ToString();
            else
                foreach (SpellInfo info in EnemyProjectileInformation)
                    MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).DisplayName = info.MissileName;
        }
        private static void ToggleBlockFormat(ValueBase sender, EventArgs args)
        {
            int value = sender.Cast<ComboBox>().CurrentValue;
            if (value == (int)CCTypeComboBoxOptions.Custom)
                foreach (SpellInfo info in EnemyProjectileInformation)
                    MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).IsVisible = true;
            else if (value == (int)CCTypeComboBoxOptions.BlockAll)
                foreach (SpellInfo info in EnemyProjectileInformation)
                    MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).IsVisible = false;
            else if (value == (int)CCTypeComboBoxOptions.BlockCC)
                foreach (SpellInfo info in EnemyProjectileInformation)
                {
                    if(info.CCType != BuffType.Internal)
                        MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).IsVisible = true;
                    else
                        MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).IsVisible = false;
                }
            else if (value == (int)CCTypeComboBoxOptions.BlockCCExcludingSlows)
                foreach (SpellInfo info in EnemyProjectileInformation)
                {
                    if (info.CCType != BuffType.Internal && info.CCType != BuffType.Slow)
                        MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).IsVisible = true;
                    else
                        MenuHandler.GetCheckbox(WindWallMenu, "WindWallSpell." + info.SpellName + ".WindWallMissile." + info.MissileName).IsVisible = false;
                }
        }
    }
}
