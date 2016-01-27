using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedEvade
{
    internal class Program
    {
        public static List<MissileClient> DrawingArray = new List<MissileClient>();

        public static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static Menu mainMenu, DodgeMenu;
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            mainMenu = MainMenu.AddMenu("Unsigned Evade", "Unsigned Evade");

            DodgeMenu = mainMenu.AddSubMenu("Dodge Menu");
            DodgeMenu.AddGroupLabel("Dodge Settings");

            SpellDatabase.Initialize();
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnDelete += OnDelete;
            GameObject.OnCreate += OnCreate;
        }
        
        private static void OnCreate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient)
            {
                Chat.Print(missile.SData.Name + " has been added");
                DrawingArray.Add(missile);
            }
        }

        private static void OnDelete(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient)
            {
                Chat.Print(missile.SData.Name + " has been destroyed");
                DrawingArray.Remove(missile);
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            foreach(MissileClient missile in DrawingArray)
            {
                foreach (SpellInfo info in SpellDatabase.SpellList)
                {
                    if (missile == null || info == null || missile.SData == null)
                        return;
                    //drawing spell
                    if (missile.SData.Name == info.SpellName 
                        && info.SkillshotType == SkillShotType.Linear)
                        Geometry.DrawRectangle(missile, info);
                    if (missile.SData.Name == info.SpellName && info.SkillshotType == SkillShotType.Circular)
                        Geometry.DrawCircle(missile, info);
                }
            }
        }
       
        private static void Game_OnTick(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            foreach(MissileClient missile in DrawingArray)
            {
                foreach (SpellInfo info in SpellDatabase.SpellList)
                {
                    bool willHitPlayer = Prediction.Position.Collision.LinearMissileCollision(_Player, missile.StartPosition.WorldToGrid(), 
                        Geometry.CalculateEndPosition(missile, info).WorldToGrid(),
                        info.MissileSpeed, info.Width, info.Delay);

                    //issue
                    if(willHitPlayer)
                    {
                        Chat.Print("KAAP");
                        Spell.Active flash = new Spell.Active(SpellSlot.Summoner1);
                        if (flash.IsReady())
                        {
                            Chat.Print("KAA2P");
                            flash.Cast(missile.StartPosition);
                        }
                    }
                }
            }
        }
    }
}
