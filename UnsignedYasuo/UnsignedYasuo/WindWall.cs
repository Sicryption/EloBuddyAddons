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

        public static void GameOnTick()
        {
            TryWindWall();
        }
        
        public static void OnGameLoad()
        {
            WindWallMenu = Program.menu.AddSubMenu("Wind Wall Menu", "WWM");
            WindWallMenu.Add("WW", new CheckBox("Auto-Use Wind Wall (BETA)"));
            WindWallMenu.Add("WWD", new CheckBox("Debug"));
            WindWallMenu.AddGroupLabel("Enemy Skillshot Toggles");
            foreach (AIHeroClient client in EntityManager.Heroes.Enemies)
                foreach (SpellInfo info in SpellDatabase.SpellList)
                {
                    if (info.ChampionName == client.ChampionName)
                    {
                        EnemyProjectileInformation.Add(info);
                        WindWallMenu.Add("WW" + info.SpellName, new CheckBox(info.SpellName));
                    }
                }
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
                    Chat.Print("Projectile: " + missile.SData.Name + " has been created.");
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
            return Prediction.Position.Collision.LinearMissileCollision(
                _Player, missile.StartPosition.To2D(), missile.StartPosition.To2D().Extend(missile.EndPosition, info.Range),
                info.MissileSpeed, info.Width, info.Delay);
        }

        public static bool ShouldWindWall(MissileClient missile, SpellInfo info)
        {
            //checks if:
            //projectile came from ally
            //if enemy is not a champion
            //if projectile name = info's spell name
            if (!missile.SpellCaster.IsEnemy ||
                missile.SpellCaster.Type != GameObjectType.AIHeroClient ||
                missile.SData.Name != info.SpellName)
                return false;

            //check if checkbox for spell is enabled
            if (WindWallMenu.Get<CheckBox>("WW" + info.SpellName) != null)
                return WindWallMenu.Get<CheckBox>("WW" + info.SpellName).CurrentValue;
            return false;
        }
    }
}
