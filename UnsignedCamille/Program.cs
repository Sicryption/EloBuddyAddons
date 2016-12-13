using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using SharpDX;
using System.Linq;

//dont cast w during e
//lasthit x with e

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
            W = new Spell.Skillshot(SpellSlot.W, 600, EloBuddy.SDK.Enumerations.SkillShotType.Cone, 250, int.MaxValue, 50, DamageType.Physical)
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
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            ModeManager.LastAutoTime = Game.Time;
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

            if (menu.GetCheckboxValue("Draw Walls for E") && E.IsReady())
            {
                if (E.Name == "CamilleE")
                {
                    List<Vector2> wallPos = GetWallPositions(Camille.Position);
                    for (int i = 0; i < wallPos.Count() - 1; i++)
                    {
                        if (wallPos[i].IsWall() && wallPos[i + 1].IsWall() && wallPos[i].IsInRange(wallPos[i + 1], 100) && wallPos[i + 1].IsInRange(Camille, Program.E.Range - 50))
                            Drawing.DrawLine(wallPos[i].To3D().WorldToScreen(), wallPos[i + 1].To3D().WorldToScreen(), 1, System.Drawing.Color.Red);
                    }
                }
                else if(Camille.HasBuff("camilleeonwall"))
                {
                    List<Vector2> dashablePos = GetDashablePositions(Camille.Position);
                    
                    for (int i = 0; i < dashablePos.Count() - 1; i++)
                    {
                        if (dashablePos[i].IsInRange(dashablePos[i + 1], 50))
                            Drawing.DrawLine(dashablePos[i].To3D().WorldToScreen(), dashablePos[i + 1].To3D().WorldToScreen(), 1, System.Drawing.Color.Red);
                    }
                }
            }
        }

        private static Vector2 GetFirstWallHit(Vector2 StartPosition, Vector2 EndPosition)
        {
            Vector2 wallPos = Vector2.Zero;
            int divisor = 20;
            Vector2 distDividend = (StartPosition - EndPosition) / divisor;
            for (int i = 0; i < divisor; i++)
            {
                Vector2 tempPos = StartPosition + (distDividend * i);
                if (tempPos.IsWall())
                {
                    wallPos = tempPos;
                    break;
                }
            }

            return wallPos;
        }

        public static List<Vector2> GetWallPositions(Vector3 sourcePosition)
        {
            List<Vector2> wallPos = new List<Vector2>();
            Vector2 extendedPos = sourcePosition.Extend(sourcePosition + new Vector3(0, 100, 0), E.Range);
            for (float i = 0; i < 360; i += 0.5f)
            {
                Vector2 Pos = GetFirstWallHit(sourcePosition.To2D(), extendedPos.RotateAroundPoint(sourcePosition.To2D(), MathUtil.DegreesToRadians(i)));
                if (Pos != Vector2.Zero)
                    wallPos.Add(Pos);
            }
            return wallPos;
        }

        public static List<Vector2> GetDashablePositions(Vector3 sourcePosition)
        {
            List<Vector2> wallPos = new List<Vector2>();
            Vector2 extendedPos = sourcePosition.Extend(sourcePosition + new Vector3(0, 100, 0), 450);
            for (float i = 0; i < 360; i += 0.5f)
            {
                Vector2 Pos = extendedPos.RotateAroundPoint(sourcePosition.To2D(), MathUtil.DegreesToRadians(i));
                if (Pos != Vector2.Zero && !Pos.IsWall())
                    wallPos.Add(Pos);
            }
            return wallPos;
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