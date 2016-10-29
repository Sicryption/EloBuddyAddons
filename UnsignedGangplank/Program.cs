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

namespace UnsignedGP
{
    internal class Program
    {
        public static Menu ComboMenu, DrawingsMenu, SettingsMenu, LaneClear, LastHit, Items, Killsteal, Harass, menu;
        public static Spell.Targeted Q; // travels at 3380 units per second
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static Spell.Targeted Ignite;
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Gangplank")
                return;

            //Hacks.AntiAFK = true;
            Bootstrap.Init(null);

            Q = new Spell.Targeted(SpellSlot.Q, 625, DamageType.Physical)
            {
                CastDelay = 250
            };
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Circular, 250, null, 700, DamageType.Physical);
            R = new Spell.Skillshot(SpellSlot.R, 999999, SkillShotType.Circular);

            menu = MainMenu.AddMenu("Unsigned GP", "UnsignedGangplank");

            ComboMenu = menu.AddSubMenu("Combo", "combomenu");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("QU", new CheckBox("Use Q"));//complete
            ComboMenu.Add("EU", new CheckBox("Use E"));//complete
            ComboMenu.Add("RU", new CheckBox("Use R"));
            ComboMenu.Add("IU", new CheckBox("Use Items"));//complete
            ComboMenu.Add("IgU", new CheckBox("Use Ignite"));//complete
            ComboMenu.Add("BarrelSettings", new ComboBox("First Barrel Usage: ", 0, "None", "EloBuddy Prediction", "On Closest Enemy", "On Lowest HP Enemy", "On Lowest % HP Enemy", "Between Enemy and Me"));

            LaneClear = menu.AddSubMenu("Lane Clear", "laneclear");
            LaneClear.AddGroupLabel("Lane Clear Settings");
            LaneClear.Add("LCQ", new CheckBox("Use Q"));//complete
            LaneClear.Add("LCE", new CheckBox("Use E"));//complete

            Harass = menu.AddSubMenu("Harass", "harass");
            Harass.AddGroupLabel("Harass Settings");
            Harass.Add("HQ", new CheckBox("Use Q"));//complete
            Harass.Add("HE", new CheckBox("Use E"));//complete

            LastHit = menu.AddSubMenu("Last Hit", "lasthitmenu");
            LastHit.AddGroupLabel("Last Hit Settings");
            LastHit.Add("LHQ", new CheckBox("Use Q"));//complete
            LastHit.Add("LHE", new CheckBox("Use E"));//complete

            Killsteal = menu.AddSubMenu("Killsteal", "killstealmenu");
            Killsteal.AddGroupLabel("Killsteal Settings");
            Killsteal.Add("KSER", new CheckBox("Activate Killsteal"));//complete
            Killsteal.Add("KSQ", new CheckBox("Use Q"));//complete
            Killsteal.Add("KSE", new CheckBox("Use E"));//complete
            Killsteal.Add("KSR", new CheckBox("Use R"));
            Killsteal.Add("KSI", new CheckBox("Use Ignite"));//complete

            Items = menu.AddSubMenu("Items", "itemsmenu");
            Items.AddGroupLabel("Item Settings");
            Items.Add("ItemsT", new CheckBox("Use Tiamat"));//complete
            Items.Add("ItemsRH", new CheckBox("Use Ravenous Hydra"));//complete
            Items.Add("ItemsTH", new CheckBox("Use Titanic Hydra"));//complete
            Items.Add("ItemsBC", new CheckBox("Use Bilgewater Cutlass"));//complete
            Items.Add("ItemsBORK", new CheckBox("Use Blade of the Ruined King"));//complete
            Items.Add("ItemsY", new CheckBox("Use Youmuus"));//complete
            Items.Add("ItemsQSS", new CheckBox("Use Quick Silversash"));//complete
            Items.Add("ItemsMS", new CheckBox("Use Mercurial Scimitar"));//complete
            Items.Add("ItemsPotions", new CheckBox("Use Potions"));
            Items.AddGroupLabel("Auto - W/QSS/Merc Scimitar Settings");
            Items.Add("AW", new CheckBox("Auto-W"));//complete
            Items.Add("QSSBlind", new CheckBox("Blind"));//complete
            Items.Add("QSSCharm", new CheckBox("Charm"));//complete
            Items.Add("QSSFear", new CheckBox("Fear"));//complete
            Items.Add("QSSKB", new CheckBox("Knockback"));//complete
            Items.Add("QSSSilence", new CheckBox("Silence"));//complete
            Items.Add("QSSSlow", new CheckBox("Slow"));//complete
            Items.Add("QSSSnare", new CheckBox("Snare"));//complete
            Items.Add("QSSStun", new CheckBox("Stun"));//complete
            Items.Add("QSSTaunt", new CheckBox("Taunt"));//complete
            Items.AddGroupLabel("Potion Settings");
            Items.Add("PotSlider", new Slider("Use Potion at Health Percent", 65, 1, 100));//add extra pots

            DrawingsMenu = menu.AddSubMenu("Drawings", "drawingsmenu");
            DrawingsMenu.AddGroupLabel("Drawings Settings");
            DrawingsMenu.Add("DQ", new CheckBox("Draw Q Range"));//complete
            DrawingsMenu.Add("DE", new CheckBox("Draw E Range"));//complete
            DrawingsMenu.Add("DEER", new CheckBox("Draw E Explosion Range"));//complete
            DrawingsMenu.Add("DMHPQ", new CheckBox("Draw if nearby minion die by Q"));//complete
            DrawingsMenu.Add("DMHPQB", new CheckBox("Draw if nearby minion die by Q on Barrel"));//complete

            SettingsMenu = menu.AddSubMenu("Settings", "settingsmenu");
            SettingsMenu.AddGroupLabel("Settings");
            SettingsMenu.Add("SAB", new CheckBox("Auto-Place Barrels with 3 stacks"));//complete
            SettingsMenu.Add("SAWH", new Slider("Auto-W at % Health", 80, 1, 99));//complete
            SettingsMenu.Add("SAWM", new Slider("Auto-W only at % Mana", 60, 1, 100));//complete

            SpellDataInst Sum1 = _Player.Spellbook.GetSpell(SpellSlot.Summoner1);
            SpellDataInst Sum2 = _Player.Spellbook.GetSpell(SpellSlot.Summoner2);
            if (Sum1.Name == "SummonerDot")
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 600);
            else if (Sum2.Name == "SummonerDot")
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 600);

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnCreate += Obj_AI_Base_OnCreate;
            Obj_AI_Base.OnDelete += Obj_AI_Base_OnDelete;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
        }

        public static List<Barrel> barrels = new List<Barrel>();

        private static void Obj_AI_Base_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.Name == "Barrel")
                barrels.Remove(GetBarrelAtPosition(sender.Position));
        }

        private static void Obj_AI_Base_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name == "Barrel")
                barrels.Add(new Barrel(sender as Obj_AI_Base, Game.Time));
        }

        public static Barrel GetBarrelAtPosition(Vector3 pos)
        {
            return barrels.Where(a => a.barrel.Position == pos).FirstOrDefault();
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.Name == "Barrel")
            {
                if (args.Buff.Name == "gangplankebarreldecaysound")
                {
                    Barrel barrel = GetBarrelAtPosition(sender.Position);

                    barrel.timeSinceLastDecay = Game.Time;
                }
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            if (DrawingsMenu["DQ"].Cast<CheckBox>().CurrentValue && Q.IsLearned)
                Drawing.DrawCircle(_Player.Position, Q.Range, System.Drawing.Color.BlueViolet);
            if (DrawingsMenu["DE"].Cast<CheckBox>().CurrentValue && E.IsLearned)
                Drawing.DrawCircle(_Player.Position, E.Range, System.Drawing.Color.BlueViolet);
            if (DrawingsMenu["DEER"].Cast<CheckBox>().CurrentValue && E.IsLearned)
                Drawing.DrawCircle(_Player.Position, E.Range + 350, System.Drawing.Color.BlueViolet);
            if (DrawingsMenu["DMHPQ"].Cast<CheckBox>().CurrentValue && Q.IsLearned)
                foreach (Obj_AI_Base min in ObjectManager.Get<Obj_AI_Base>().Where(a => a.Type == GameObjectType.obj_AI_Minion && a.Distance(_Player) <= 1500 && a.Name != "MoveTester" && a.Name != "Barrel" && a.Name != "WardCorpse" && a.IsEnemy && !a.IsDead && GPCalcs.Q(a) >= a.Health))
                    Drawing.DrawCircle(min.Position, 150, System.Drawing.Color.Green);
            if (DrawingsMenu["DMHPQB"].Cast<CheckBox>().CurrentValue && Q.IsLearned && E.IsLearned)
                foreach (Obj_AI_Base min in ObjectManager.Get<Obj_AI_Base>().Where(a => a.Type == GameObjectType.obj_AI_Minion && a.Distance(_Player) <= 1500 && a.Name != "MoveTester" && a.Name != "Barrel" && a.Name != "WardCorpse" && a.IsEnemy && !a.IsDead && GPCalcs.E(a, true) >= a.Health))
                    Drawing.DrawCircle(min.Position, 150, System.Drawing.Color.DarkRed);
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                GPFunctions.Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                GPFunctions.LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                GPFunctions.Harrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                GPFunctions.LaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                GPFunctions.Flee();
            if (Killsteal["KSER"].Cast<CheckBox>().CurrentValue)
                GPFunctions.KillSteal();
            if (Items["PotSlider"].Cast<Slider>().CurrentValue != 100)
                GPFunctions.UseItems();
            if (SettingsMenu["SAB"].Cast<CheckBox>().CurrentValue)
                GPFunctions.AutoBarrel();
            GPFunctions.AutoW();
        }
    }
}
