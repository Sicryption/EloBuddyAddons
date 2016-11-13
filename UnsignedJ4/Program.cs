using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Linq;
using SharpDX;

namespace UnsignedJarvanIV
{
    internal class Program
    {
        public static AIHeroClient JarvanIV;
        public static Spell.Skillshot Q, E;
        public static Spell.Active W;
        public static Spell.Targeted R;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "JarvanIV")
                return;

            JarvanIV = Player.Instance;
            ModeHandler.JarvanIV = Player.Instance;

            MenuHandler.Initialize();

            Q = new Spell.Skillshot(SpellSlot.Q, 770, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 250, null, 180, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            W = new Spell.Active(SpellSlot.W, 600);
            E = new Spell.Skillshot(SpellSlot.E, 830, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 250, null, 75, DamageType.Magical)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Targeted(SpellSlot.R, 650, DamageType.Physical);

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Enemy Health after Combo"))
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria()))
                {
                    int hpBarWidth = 96;
                    float enemyHPPercentAfterCombo = Math.Max((100 * ((enemy.Health - enemy.ComboDamage()) / enemy.MaxHealth)), 0);
                    //Vector2 FriendlyHPBarOffset = new Vector2(26, 3);
                    Vector2 EnemyHPBarOffset = new Vector2(2, 9.5f);
                    Vector2 CurrentHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(100 * enemy.HealthPercent / hpBarWidth, 0);
                    Vector2 EndHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(enemyHPPercentAfterCombo, 0);
                    if(enemyHPPercentAfterCombo == 0)
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Green);
                    else
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Yellow);
                }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Q"))
                Q.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw W"))
                W.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw E"))
                E.DrawRange(drawColor, 3);
            
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw R"))
                R.DrawRange(drawColor, 3);
            
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Killable Text"))
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && a.Health < a.ComboDamage()))
                    Drawing.DrawText(enemy.Position.WorldToScreen(), System.Drawing.Color.GreenYellow, "Killable", 15);
        }

        private static void Game_OnTick(EventArgs args)
        {
            ModeHandler.hasDoneActionThisTick = false;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                ModeHandler.Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                ModeHandler.JungleClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                ModeHandler.LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                ModeHandler.LaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                ModeHandler.Harass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                ModeHandler.Flee();
            if (MenuHandler.Killsteal.GetCheckboxValue("Killsteal"))
                ModeHandler.Killsteal();
        }
    }
}