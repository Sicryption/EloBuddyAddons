using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedYasuo
{
    internal class Program
    {
        public static Spell.Skillshot Q, Q3, EQ, Flash;
        public static Spell.Skillshot W;
        public static Spell.Targeted E, Ignite;
        public static Spell.Active R;
        public static AIHeroClient Yasuo { get { return ObjectManager.Player; } }
        public static int currentPentaKills = 0;
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Yasuo")
                return;

            #region SpellSetup
            Q = new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear, 250, int.MaxValue, 55, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
                SourcePosition = Yasuo.Position
            };
            Q3 = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 300, 1200, 90, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
                SourcePosition = Yasuo.Position
            };
            W = new Spell.Skillshot(SpellSlot.W, 400, SkillShotType.Cone, 250);
            EQ = new Spell.Skillshot(SpellSlot.Q, 375, SkillShotType.Circular, 0, int.MaxValue, 375, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Targeted(SpellSlot.E, 475, DamageType.Magical)
            {
                CastDelay = 250,
            };
            R = new Spell.Active(SpellSlot.R, 1200, DamageType.Physical)
            {
                CastDelay = 0,
            };
            Ignite = new Spell.Targeted(Yasuo.GetSpellSlotFromName("SummonerDot"), 600, DamageType.True)
            {
                CastDelay = 0,
            };
            Flash = new Spell.Skillshot(Yasuo.GetSpellSlotFromName("SummonerFlash"), 425, SkillShotType.Linear);
            #endregion

            #region Initializers
            MenuHandler.Initialize();
            #endregion

            #region Events
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Game.OnTick += Game_OnTick;
            #endregion

            #region Variable Setup
            currentPentaKills = Yasuo.PentaKills;
            #endregion

            Orbwalker.DisableMovement = true;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Yasuo.IsDead)
                return;

            YasuoFunctions.didActionThisTick = false;
            
            YasuoFunctions.AutoHarrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                YasuoFunctions.Flee();
            else if(Orbwalker.ActiveModesFlags != Orbwalker.ActiveModes.None)
            {
                Orbwalker.MoveTo(Game.CursorPos);

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                    YasuoFunctions.Combo();
                if (MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Activate Killsteal"))
                    YasuoFunctions.KS();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                    YasuoFunctions.LastHit();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                    YasuoFunctions.Harrass();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                    YasuoFunctions.JungleClear();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                    YasuoFunctions.LaneClear();
            }

            if (Yasuo.PentaKills > currentPentaKills)
            {
                Chat.Print("Nice Penta! Make sure to screenshot it and post it on the UnsignedYasuo thread to show off!");
                
                currentPentaKills = Yasuo.PentaKills;
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.Name == Yasuo.Name && args.SData.Name == "YasuoDashWrapper")
                YasuoCalcs.YasuoLastEStartTime = args.Time;
        }

        //complete
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Yasuo.IsDead)
                return;

            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Q") && !Yasuo.HasBuff("YasuoQ3W") && Q.IsLearned)
                Q.DrawRange(drawColor);
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Q") && Yasuo.HasBuff("YasuoQ3W") && Q.IsLearned)
                Q3.DrawRange(drawColor);
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw W") && W.IsLearned)
                W.DrawRange(drawColor);
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw E") && E.IsLearned)
                E.DrawRange(drawColor);
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw EQ") && E.IsLearned && Q.IsLearned)
                EQ.DrawRange(drawColor);
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw R") && R.IsLearned)
                R.DrawRange(drawColor);
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Beyblade") && R.IsLearned && Flash != null && E.IsLearned && Q.IsLearned)
                Drawing.DrawCircle(Yasuo.Position, E.Range + Flash.Range + (EQ.Range / 2), drawColor);
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Turret Range"))
                foreach (Obj_AI_Turret turret in EntityManager.Turrets.Enemies.Where(a => !a.IsDead))
                    turret.DrawCircle((int)turret.GetAutoAttackRange() + 35, drawColor);

            Obj_AI_Base hoverObject = EntityManager.Enemies.Where(a => !a.IsDead && a.IsTargetable && a.IsInRange(Yasuo, E.Range) && a.Distance(Game.CursorPos) <= 75).OrderBy(a => a.Distance(Game.CursorPos)).FirstOrDefault();
            if (hoverObject != null)
            {
                if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw EQ on Target"))
                    Drawing.DrawCircle(YasuoCalcs.GetDashingEnd(hoverObject), EQ.Range, drawColor);
                if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw E End Position on Target"))
                    Drawing.DrawLine(Yasuo.Position.WorldToScreen(), YasuoCalcs.GetDashingEnd(hoverObject).WorldToScreen(), 3, drawColor);
            }

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Wall Dashes") && E.IsLearned)
                foreach (WallDash wd in YasuoWallDashDatabase.wallDashDatabase.Where(a=>a.startPosition.Distance(Yasuo) <= 1300))
                    if (EntityManager.MinionsAndMonsters.Combined.Where(a => a.MeetsCriteria() && a.Name == wd.unitName && a.ServerPosition.Distance(wd.dashUnitPosition) <= 2).FirstOrDefault() != null)
                    {
                        wd.startPosition.DrawArrow(wd.endPosition, System.Drawing.Color.Red, 1);
                        Geometry.Polygon.Circle dashCircle = new Geometry.Polygon.Circle(wd.endPosition, 120);
                        dashCircle.Draw(System.Drawing.Color.Red, 1);
                    }
        }
    }
}
