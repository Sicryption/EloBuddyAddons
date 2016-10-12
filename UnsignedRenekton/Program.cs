using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedRenekton
{
    internal class Program
    {
        public static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static Menu ComboMenu, DrawingsMenu, LaneClear, LastHit, Killsteal, Harass, menu;
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Skillshot E = new Spell.Skillshot(SpellSlot.E, 450, SkillShotType.Linear);
        public static Spell.Active R;
        public static Spell.Targeted Ignite;
        public static int AARange = 125;
        public static int QRange = 325;
        public static int WRange = 175;
        public static int EmpERange = 900;
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Renekton")
                return;

            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W);
            R = new Spell.Active(SpellSlot.R);

            menu = MainMenu.AddMenu("Unsigned Renekton", "Unsigned Renekton");

            ComboMenu = menu.AddSubMenu("Combo", "combomenu");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("CQ", new CheckBox("Use Q"));
            ComboMenu.Add("CW", new CheckBox("Use W"));
            ComboMenu.Add("CE", new CheckBox("Use E"));
            ComboMenu.Add("CR", new CheckBox("Use R"));
            ComboMenu.Add("CI", new CheckBox("Use Items"));

            LaneClear = menu.AddSubMenu("Lane Clear", "laneclear");
            LaneClear.AddGroupLabel("Lane Clear Settings");
            LaneClear.Add("LCSF", new CheckBox("Save Fury"));
            LaneClear.Add("LCQ", new CheckBox("Use Q"));
            LaneClear.Add("LCE", new CheckBox("Use E"));
            LaneClear.Add("LCI", new CheckBox("Use Items"));

            Harass = menu.AddSubMenu("Harass", "harass");
            Harass.AddGroupLabel("Harass Settings");
            Harass.Add("HQ", new CheckBox("Use Q"));
            Harass.Add("HW", new CheckBox("Use W"));
            Harass.Add("HE", new CheckBox("Use E"));
            Harass.Add("HI", new CheckBox("Use Items"));

            LastHit = menu.AddSubMenu("Last Hit", "lasthitmenu");
            LastHit.AddGroupLabel("Last Hit Settings");
            LastHit.Add("LHSF", new CheckBox("Save Fury"));
            LastHit.Add("LHQ", new CheckBox("Use Q"));
            LastHit.Add("LHE", new CheckBox("Use E", false));

            Killsteal = menu.AddSubMenu("Killsteal", "killstealmenu");
            Killsteal.AddGroupLabel("Killsteal Settings");
            Killsteal.Add("KSER", new CheckBox("Activate KS"));
            Killsteal.Add("KSQ", new CheckBox("Use Q"));
            Killsteal.Add("KSW", new CheckBox("Use W"));
            Killsteal.Add("KSE", new CheckBox("Use E"));
            Killsteal.Add("KSI", new CheckBox("Use Ignite"));

            DrawingsMenu = menu.AddSubMenu("Drawings", "drawingsmenu");
            DrawingsMenu.AddGroupLabel("Drawings Settings");
            DrawingsMenu.Add("DAA", new CheckBox("Draw AA"));
            DrawingsMenu.Add("DQ", new CheckBox("Draw Q"));
            DrawingsMenu.Add("DW", new CheckBox("Draw W"));
            DrawingsMenu.Add("DE", new CheckBox("Draw E"));
            DrawingsMenu.Add("DEE", new CheckBox("Draw Emp E"));

            SpellDataInst Sum1 = _Player.Spellbook.GetSpell(SpellSlot.Summoner1);
            SpellDataInst Sum2 = _Player.Spellbook.GetSpell(SpellSlot.Summoner2);
            if (Sum1.Name == "summonerdot")
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 600);
            else if (Sum2.Name == "summonerdot")
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 600);
            
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            if (Program.DrawingsMenu["DAA"].Cast<CheckBox>().CurrentValue)
                Drawing.DrawCircle(_Player.Position, AARange, System.Drawing.Color.White);
            if (Program.DrawingsMenu["DQ"].Cast<CheckBox>().CurrentValue && Q.IsLearned)
                Drawing.DrawCircle(_Player.Position, QRange, System.Drawing.Color.BlueViolet);
            if (Program.DrawingsMenu["DW"].Cast<CheckBox>().CurrentValue && W.IsLearned)
                Drawing.DrawCircle(_Player.Position, WRange, System.Drawing.Color.Wheat);
            if (Program.DrawingsMenu["DE"].Cast<CheckBox>().CurrentValue && E.IsLearned)
                Drawing.DrawCircle(_Player.Position, E.Range, System.Drawing.Color.OrangeRed);
            if (Program.DrawingsMenu["DEE"].Cast<CheckBox>().CurrentValue && E.IsLearned)
                Drawing.DrawCircle(_Player.Position, EmpERange, System.Drawing.Color.Red);
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                RenektonFunctions.Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                RenektonFunctions.LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                RenektonFunctions.Harrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                RenektonFunctions.LaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                RenektonFunctions.Flee();
            if (Killsteal["KSER"].Cast<CheckBox>().CurrentValue)
                RenektonFunctions.KillSteal();
        }
    }
}
