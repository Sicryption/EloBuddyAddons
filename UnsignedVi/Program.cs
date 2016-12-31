using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Linq;
using SharpDX;

namespace UnsignedVi
{
    internal class Program
    {
        public static AIHeroClient Vi;
        public static Spell.Active E, W;
        public static Spell.Chargeable Q;
        public static Spell.Targeted R;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Vi")
                return;

            Vi = Player.Instance;
            ModeHandler.Vi = Player.Instance;

            MenuHandler.Initialize();

            Q = new Spell.Chargeable(SpellSlot.Q, 250, 725, 4, 250, 1250, 55, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            W = new Spell.Active(SpellSlot.W, 0);
            E = new Spell.Active(SpellSlot.E, 175, DamageType.Physical);
            R = new Spell.Targeted(SpellSlot.R, 800, DamageType.Physical);

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            ModeHandler.LastAutoTime = Game.Time;
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

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw E"))
                E.DrawRange(drawColor, 3);

            Obj_AI_Base hoverObject = EntityManager.Enemies.Where(a => !a.IsDead && a.IsTargetable && a.IsInRange(Vi, E.Range) && a.Distance(Game.CursorPos) <= 75).OrderBy(a => a.Distance(Game.CursorPos)).FirstOrDefault();
            if (hoverObject != null && MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw E Sector on Target"))
                    new Geometry.Polygon.Sector(Vi.Position, hoverObject.Position, (float)(45 * Math.PI / 180), 600).Draw(drawColor);

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