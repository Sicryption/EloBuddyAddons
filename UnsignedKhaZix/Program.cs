using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using SharpDX;
using System.Linq;

namespace KhaZix
{
    internal class Program
    {
        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        //radius is 300 so width is 600
        public static Spell.Skillshot E;
        public static Spell.Active R;

        public static AIHeroClient Kha = null;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Kha = Player.Instance;
            if (Kha.Hero != Champion.Khazix)
                return;


            Q = new Spell.Targeted(SpellSlot.Q, (uint)(Player.Instance.Spellbook.GetSpell(SpellSlot.E).Name.Contains("Long") ? 375 : 325), DamageType.Physical);
            //radius is 300 so width is 600
            W = new Spell.Skillshot(SpellSlot.W, 1025, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 250, 1700, 70, DamageType.Physical);
            E = new Spell.Skillshot(SpellSlot.E, (uint)(Player.Instance.Spellbook.GetSpell(SpellSlot.E).Name.Contains("Long") ? 900 : 700), EloBuddy.SDK.Enumerations.SkillShotType.Circular, 250, 1000, 600, DamageType.Physical);
            R = new Spell.Active(SpellSlot.R, Kha.HasBuff("khazixrstealth") ? 0 : (uint)(1.25f * (Player.Instance.MoveSpeed * 1.4f)));

            MenuHandler.Initialize();

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            ModeHandler.LastAutoTime = Game.Time;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Kha.IsDead)
                return;

            Menu menu = MenuHandler.Drawing;
            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            if (menu.GetCheckboxValue("Draw Q Range"))
                Q.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw W Range"))
                W.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw E Range"))
                E.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw R Run Range"))
                R.DrawRange(drawColor);
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Kha.IsDead)
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
        
        private static void Game_OnTick(EventArgs args)
        {
            if (Kha.IsDead)
                return;

            Q.Range = (uint)(Q.Name.Contains("Long") ? 375 : 325);
            //radius is 300 so width is 600
            E.Range = (uint)(E.Name.Contains("Long") ? 900 : 700);
            R.Range = Kha.HasBuff("khazixrstealth") ? 0 : (uint)(1.25f * (Player.Instance.MoveSpeed * 1.4f));

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