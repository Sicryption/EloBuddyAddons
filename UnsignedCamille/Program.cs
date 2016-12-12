using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using SharpDX;
using System.Linq;

namespace UnsignedCamille
{
    internal class Program
    {
        public static Spell.Active Q, Q2;
        public static Spell.Skillshot W, W2, E;
        public static Spell.Targeted R;
        public static AIHeroClient Camille => Player.Instance;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Camille")
                return;

            MenuHandler.Initialize();

            Q = new Spell.Active(SpellSlot.Q);
            Q2 = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 640, EloBuddy.SDK.Enumerations.SkillShotType.Cone, 250, int.MaxValue, 50, DamageType.Physical)
            {
                ConeAngleDegrees = 45,
            };
            W2 = new Spell.Skillshot(SpellSlot.W, 325, EloBuddy.SDK.Enumerations.SkillShotType.Cone, 250, int.MaxValue, 50, DamageType.Physical)
            {
                ConeAngleDegrees = 45,
            };
            E = new Spell.Skillshot(SpellSlot.E, 1100, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 250, 1000, 80);
            R = new Spell.Targeted(SpellSlot.R, 475);

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
        }

        //add qTime to ModeManager
        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if(sender.IsMe)
            {

            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Camille.IsDead)
                return;

            Menu menu = MenuHandler.Drawing;

            if (menu.GetCheckboxValue("Draw Combo Damage"))
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria()))
                {
                    int hpBarWidth = 96;
                    float enemyHPPercentAfterCombo = Math.Max((100 * ((enemy.Health - enemy.ComboDamage()) / enemy.MaxHealth)), 0);
                    //Vector2 FriendlyHPBarOffset = new Vector2(26, 3);
                    Vector2 EnemyHPBarOffset = new Vector2(2, 9.5f);
                    Vector2 CurrentHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(100 * enemy.HealthPercent / hpBarWidth, 0);
                    Vector2 EndHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(enemyHPPercentAfterCombo, 0);
                    if (enemyHPPercentAfterCombo == 0)
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Green);
                    else
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Yellow);
                }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Camille.IsDead)
                return;

            Menu menu = MenuHandler.Drawing;
            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            if (menu.GetCheckboxValue("Draw W Inner Range"))
                W2.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw W Inner Range"))
                W.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw E Range"))
                E.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw R Range"))
                R.DrawRange(drawColor);
        }

        private static void Game_OnTick(EventArgs args)
        {
            ModeManager.hasDoneActionThisTick = false;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                ModeManager.Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                ModeManager.JungleClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                ModeManager.LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                ModeManager.LaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                ModeManager.Harass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                ModeManager.Flee();
            if (MenuHandler.Killsteal.GetCheckboxValue("Killsteal"))
                ModeManager.Killsteal();
        }
    }
}