using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Linq;

namespace UnsignedRengar
{
    internal class Program
    {
        public static AIHeroClient Rengar;
        public static Spell.Active W,
            R;
        public static Spell.Skillshot E,
            Q,
            Q2;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Rengar")
                return;

            Rengar = Player.Instance;
            ModeHandler.Rengar = Player.Instance;

            MenuHandler.Initialize();

            Q = new Spell.Skillshot(SpellSlot.Q, 324, EloBuddy.SDK.Enumerations.SkillShotType.Cone, 250, 3000, 55, DamageType.Physical)
            {
                ConeAngleDegrees = 180,
                AllowedCollisionCount = int.MaxValue,
                
            };
            Q2 = new Spell.Skillshot(SpellSlot.Q, 450, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 500, 3000, 55, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            W = new Spell.Active(SpellSlot.W, 450, DamageType.Magical);
            E = new Spell.Skillshot(SpellSlot.E, 1000, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 250, 1500, 70, DamageType.Physical)
            {
                AllowedCollisionCount = 1,
            };
            R = new Spell.Active(SpellSlot.R, 2000);

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
        }
        
        private static void Drawing_OnDraw(EventArgs args)
        {
            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            int qhit = 0;
            Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, 
                EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList(), Rengar.Position, out qhit);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Q"))
                //ty eb for .Direction being broken. new Geometry.Polygon.Sector(Rengar.Position, Rengar.Direction, Q.ConeAngleDegrees, Q.Range).Draw(drawColor);
                Q.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw W"))
                W.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw E"))
                E.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw R Detection Range"))
                R.DrawRange(drawColor, 3);

            AIHeroClient closestEnemy = EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && a.IsInRange(Rengar, 3000)).FirstOrDefault();

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Arrow to R Target") && Rengar.HasBuff("RengarR") && closestEnemy != null)
                Rengar.Position.DrawArrow(closestEnemy.Position, drawColor);
            }

        private static void Game_OnTick(EventArgs args)
        {
            ModeHandler.hasDoneActionThisTick = false;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                ModeHandler.Combo();
        }
    }
}