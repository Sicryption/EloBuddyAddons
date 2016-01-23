using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UnsignedAnnie
{
    internal class Program
    {
        public static Menu ComboMenu, DrawingsMenu, SettingsMenu, LaneClear, LastHit, Killsteal, Harass, menu;
        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Targeted R;
        public static Spell.Targeted Ignite;
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        public static int Mana { get { return (int)_Player.Mana; } }
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Annie")
                return;

            //Hacks.AntiAFK = true;
            Bootstrap.Init(null);

            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 625, SkillShotType.Cone);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Targeted(SpellSlot.R, 745);

            menu = MainMenu.AddMenu("Unsigned Annie", "UnsignedAnnie");

            ComboMenu = menu.AddSubMenu("Combo", "combomenu");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("QU", new CheckBox("Use Q"));
            ComboMenu.Add("WU", new CheckBox("Use W"));
            ComboMenu.Add("EU", new CheckBox("Use E"));
            ComboMenu.Add("RU", new CheckBox("Use R"));
            ComboMenu.Add("IU", new CheckBox("Use Items"));
            ComboMenu.Add("IgU", new CheckBox("Use Ignite"));

            LaneClear = menu.AddSubMenu("Lane Clear", "laneclear");
            LaneClear.AddGroupLabel("Lane Clear Settings");
            LaneClear.Add("LCQ", new CheckBox("Use Q"));
            LaneClear.Add("LCW", new CheckBox("Use W"));

            Harass = menu.AddSubMenu("Harass", "harass");
            Harass.AddGroupLabel("Harass Settings");
            Harass.Add("HQ", new CheckBox("Use Q"));
            Harass.Add("HW", new CheckBox("Use W"));

            LastHit = menu.AddSubMenu("Last Hit", "lasthitmenu");
            LastHit.AddGroupLabel("Last Hit Settings");
            LastHit.Add("LHQ", new CheckBox("Use Q"));
            LastHit.Add("LHW", new CheckBox("Use W"));

            Killsteal = menu.AddSubMenu("Killsteal", "killstealmenu");
            Killsteal.AddGroupLabel("Killsteal Settings");
            Killsteal.Add("KSER", new CheckBox("Activate Killsteal"));
            Killsteal.Add("KSQ", new CheckBox("Use Q"));
            Killsteal.Add("KSW", new CheckBox("Use W"));
            Killsteal.Add("KSR", new CheckBox("Use R"));
            Killsteal.Add("KSI", new CheckBox("Use Ignite"));

            DrawingsMenu = menu.AddSubMenu("Drawings", "drawingsmenu");
            DrawingsMenu.AddGroupLabel("Drawings Settings");
            DrawingsMenu.Add("DQ", new CheckBox("Draw Q/W"));
            DrawingsMenu.Add("DR", new CheckBox("Draw R"));
            
            SettingsMenu = menu.AddSubMenu("Settings", "settingsmenu");
            SettingsMenu.AddGroupLabel("Combo Settings");
            SettingsMenu.Add("SS", new CheckBox("Prepare Stun at Base"));
            SettingsMenu.Add("SHM", new CheckBox("Auto-Use Mana and Health Potions"));
            SettingsMenu.Add("ST", new CheckBox("Auto-Control Tibbers"));

            SpellDataInst Sum1 = _Player.Spellbook.GetSpell(SpellSlot.Summoner1);
            SpellDataInst Sum2 = _Player.Spellbook.GetSpell(SpellSlot.Summoner2);
            if (Sum1.Name == "summonerdot")
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 600);
            else if (Sum2.Name == "summonerdot")
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 600);
            
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnBuffGain += OnBuffGain;
            Obj_AI_Base.OnBuffLose += OnBuffLose;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Program.DrawingsMenu["DQ"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.DrawCircle(_Player.Position, Q.Range, System.Drawing.Color.BlueViolet);
            }

            if (Program.DrawingsMenu["DR"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.DrawCircle(_Player.Position, R.Range, System.Drawing.Color.BlueViolet);
            }

        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                AnnieFunctions.Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                AnnieFunctions.LastHit();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                AnnieFunctions.Harrass();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                AnnieFunctions.LaneClear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                AnnieFunctions.Flee();
            }
            if (Program.SettingsMenu["SS"].Cast<CheckBox>().CurrentValue)
            {
                AnnieFunctions.StackMode();
            }
            if (Program.Killsteal["KSER"].Cast<CheckBox>().CurrentValue)
            {
                AnnieFunctions.KillSteal();
            }
            if (Program.SettingsMenu["SHM"].Cast<CheckBox>().CurrentValue)
            {
                AnnieFunctions.UseItems();
            }
            if (Program.SettingsMenu["ST"].Cast<CheckBox>().CurrentValue)
            {
                AnnieFunctions.ControlTibbers();
            }
        }

        static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs buff)
        {
            //if (sender.IsMe && buff.Buff.Name == "yasuoq3w")
            //    Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear);
        }
        static void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs buff)
        {
            //if (sender.IsMe && buff.Buff.Name == "yasuoq3w")
            //    Q = new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear);
        }
    }
}
