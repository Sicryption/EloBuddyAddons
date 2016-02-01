using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UnsignedYasuo
{
    internal class Program
    {
        public static Menu ComboMenu, DrawingsMenu, KSMenu, LaneClear, LastHit, Harass, menu;
        public static Spell.Skillshot Q;
        public static int EQRange = 375;
        public static Spell.SpellBase W;
        public static Spell.Targeted E;
        public static Spell.Active R;
        public static Spell.Targeted Ignite;
        public static HitChance QHitChance = HitChance.Unknown;
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Yasuo")
                return;

            Q = new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear);
            W = new Spell.Skillshot(SpellSlot.W, 400, SkillShotType.Linear);
            E = new Spell.Targeted(SpellSlot.E, 475);
            R = new Spell.Active(SpellSlot.R);// range 1200

            menu = MainMenu.AddMenu("Unsigned Yasuo", "UnsignedYasuo");
            menu.Add("ABOUT", new Label("This Addon was designed by Chaos"));
            menu.Add("QHitChance", new Slider("Q Hit Chance: Medium", 2, 0, 3));

            ComboMenu = menu.AddSubMenu("Combo", "combomenu");

            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("CQ", new CheckBox("Use Q"));
            ComboMenu.Add("CE", new CheckBox("Use E"));
            ComboMenu.Add("CEQ", new CheckBox("Use EQ"));
            ComboMenu.Add("CR", new CheckBox("Use R"));
            ComboMenu.Add("CI", new CheckBox("Use Items"));
            ComboMenu.Add("CEUT", new CheckBox("E Under Turret", false));

            LaneClear = menu.AddSubMenu("Lane Clear", "laneclear");
            LaneClear.AddGroupLabel("Lane Clear Settings");
            LaneClear.Add("LCQ", new CheckBox("Use Q"));
            LaneClear.Add("LCE", new CheckBox("Use E"));
            LaneClear.Add("LCEQ", new CheckBox("Use EQ"));
            LaneClear.Add("LCELH", new CheckBox("Only E for Last Hit"));
            LaneClear.Add("LCEUT", new CheckBox("E Under Turret", false));
            LaneClear.Add("LCI", new CheckBox("Use Items (Hydra/Timat)"));

            Harass = menu.AddSubMenu("Harass", "harass");
            Harass.AddGroupLabel("Harass Settings");
            Harass.Add("HQ", new CheckBox("Use Q"));
            Harass.Add("HE", new CheckBox("Use E"));
            Harass.Add("HEQ", new CheckBox("Use EQ"));
            Harass.Add("HEUT", new CheckBox("E Under Turret", false));
            Harass.Add("HI", new CheckBox("Use Items (Hydra/Timat)"));
            Harass.AddGroupLabel("Auto-Harass Settings");
            Harass.Add("AHQ", new CheckBox("Auto-Harass with Q"));
            Harass.Add("AH3Q", new CheckBox("Auto-Harass with 3rd Q", false));

            LastHit = menu.AddSubMenu("Last Hit", "lasthitmenu");
            LastHit.AddGroupLabel("Last Hit Settings");
            LastHit.Add("LHQ", new CheckBox("Use Q"));
            LastHit.Add("LHE", new CheckBox("Use E"));
            LastHit.Add("LHEQ", new CheckBox("Use EQ"));
            LastHit.Add("LHEUT", new CheckBox("E Under Turret", false));

            KSMenu = menu.AddSubMenu("Kill Steal", "ksmenu");

            KSMenu.AddGroupLabel("Kill Steal Settings");
            KSMenu.Add("EnableKS", new CheckBox("KS"));
            KSMenu.Add("KSQ", new CheckBox("KS with Q"));
            KSMenu.Add("KS3Q", new CheckBox("KS with 3rd Q"));
            KSMenu.Add("KSE", new CheckBox("KS with E"));
            KSMenu.Add("KSEQ", new CheckBox("KS with EQ"));
            KSMenu.Add("KSI", new CheckBox("KS with Ignite"));
            KSMenu.Add("KSEUT", new CheckBox("E Under Turret", false));

            DrawingsMenu = menu.AddSubMenu("Drawings", "drawingsmenu");

            DrawingsMenu.AddGroupLabel("Drawings Settings");
            DrawingsMenu.Add("DQ", new CheckBox("Draw Q"));
            DrawingsMenu.Add("DW", new CheckBox("Draw W"));
            DrawingsMenu.Add("DE", new CheckBox("Draw E"));
            DrawingsMenu.Add("DR", new CheckBox("Draw R"));
            DrawingsMenu.Add("DT", new CheckBox("Draw Turret Range", false));

            Spellbook spell = _Player.Spellbook;
            SpellDataInst Sum1 = spell.GetSpell(SpellSlot.Summoner1);
            SpellDataInst Sum2 = spell.GetSpell(SpellSlot.Summoner2);
            if (Sum1.Name == "Ignite")
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 600);
            else if (Sum2.Name == "Ignite")
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 600);

            Game.OnTick += Game_OnTick;
            Game.OnTick += WindWall.GameOnTick;
            Game.OnUpdate += WindWall.GameOnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnBuffGain += OnBuffGain;
            Obj_AI_Base.OnBuffLose += OnBuffLose;
            Obj_AI_Base.OnCreate += WindWall.OnCreate;
            Obj_AI_Base.OnDelete += WindWall.OnDelete;
            Obj_AI_Base.OnUpdatePosition += WindWall.OnUpdate;
            menu.Get<Slider>("QHitChance").OnValueChange += OnHitChanceSliderChange;
            UnsignedEvade.SpellDatabase.Initialize();
            WindWall.OnGameLoad();

            OnHitChanceSliderChange(menu.Get<Slider>("QHitChance"), null);
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            if (DrawingsMenu["DQ"].Cast<CheckBox>().CurrentValue && Q.IsLearned)
                Drawing.DrawCircle(_Player.Position, Q.Range, System.Drawing.Color.BlueViolet);
            if (DrawingsMenu["DE"].Cast<CheckBox>().CurrentValue && E.IsLearned)
                Drawing.DrawCircle(_Player.Position, E.Range, System.Drawing.Color.BlueViolet);
            if (DrawingsMenu["DW"].Cast<CheckBox>().CurrentValue && W.IsLearned)
                Drawing.DrawCircle(_Player.Position, W.Range, System.Drawing.Color.BlueViolet);
            if (DrawingsMenu["DR"].Cast<CheckBox>().CurrentValue && R.IsLearned)
                Drawing.DrawCircle(_Player.Position, 1200, System.Drawing.Color.BlueViolet);

            if (DrawingsMenu["DT"].Cast<CheckBox>().CurrentValue)
                foreach (Obj_AI_Turret t in EntityManager.Turrets.Enemies)
                    Drawing.DrawCircle(t.Position, 875, System.Drawing.Color.BlueViolet);
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            YasuoFunctions.GetQType();
            YasuoFunctions.AutoHarrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                YasuoFunctions.Combo();
            if (KSMenu["EnableKS"].Cast<CheckBox>().CurrentValue)
                YasuoFunctions.KS();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                YasuoFunctions.LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                YasuoFunctions.Harrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                YasuoFunctions.LaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                YasuoFunctions.Flee();
        }

        static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs buff)
        {
            if (sender.IsMe && buff.Buff.Name == "yasuoq3w")
                Q =  new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear);
        }
        static void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs buff)
        {
            if (sender.IsMe && buff.Buff.Name == "yasuoq3w")
                Q =  new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear);
        }

        static void OnHitChanceSliderChange(ValueBase sender, EventArgs args)
        {
            Slider slider = sender.Cast<Slider>();
            int value = slider.CurrentValue;

            if (value == 0)
            {
                slider.DisplayName = "Q Hit Chance: Any";
                QHitChance = HitChance.Unknown;
            }
            else if (value == 1)
            {
                slider.DisplayName = "Q Hit Chance: " + HitChance.Low.ToString();
                QHitChance = HitChance.Low;
            }
            else if (value == 2)
            {
                slider.DisplayName = "Q Hit Chance: " + HitChance.Medium.ToString();
                QHitChance = HitChance.Medium;
            }
            else if (value == 3)
            {
                slider.DisplayName = "Q Hit Chance: " + HitChance.High.ToString();
                QHitChance = HitChance.High;
            }
        }
    }
}
