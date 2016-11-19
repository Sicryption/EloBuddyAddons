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

namespace UnsignedGangplank
{
    internal class Program
    {
        public static Spell.Targeted Q, Ignite;
        public static Spell.Skillshot E, R;
        public static Spell.Active W;
        public static float barrelRadius = 345f,
            barrelDiameter = 690f;
        public static AIHeroClient Gangplank { get { return ObjectManager.Player; } }
        public static int currentPentaKills = 0;
        public static List<Barrel> barrels = new List<Barrel>();

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Gangplank")
                return;

            #region SpellSetup
            Q = new Spell.Targeted(SpellSlot.Q, 625, DamageType.Physical)
            {
                CastDelay = 250,
            };
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Circular, 250, null, 690);
            R = new Spell.Skillshot(SpellSlot.R, 20000, SkillShotType.Circular, 250, null, 1050, DamageType.Physical);
            Ignite = new Spell.Targeted(Gangplank.GetSpellSlotFromName("SummonerDot"), 600, DamageType.True)
            {
                CastDelay = 0,
            };
            #endregion

            #region Initializers
            MenuHandler.Initialize();
            #endregion

            #region Events
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Game.OnTick += Game_OnTick;
            GameObject.OnCreate += Obj_AI_Base_OnCreate;
            GameObject.OnDelete += Obj_AI_Base_OnDelete;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;
            #endregion

            #region Variable Setup
            currentPentaKills = Gangplank.PentaKills;

            foreach (Obj_AI_Base ob in ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "Barrel"))
                barrels.Add(new Barrel(ob, Game.Time - 500));
            #endregion
        }

        private static void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if(sender.IsMe && args.Slot == SpellSlot.E && MenuHandler.Settings.GetCheckboxValue("Barrel Position Auto-Correct"))
                if(args.End.IsInRangeOfBarrels(barrels))
                    GangplankFunctions.CastE(sender.Position.Extend(args.End, barrelRadius + 150f).To3D((int)args.End.Z));
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Gangplank.IsDead)
                return;

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Enemy Health after Combo"))
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria()))
                {
                    int hpBarWidth = 96;
                    float enemyHPPercentAfterCombo = Math.Max((100 * ((enemy.Health - Gangplank.ComboDamage(enemy)) / enemy.MaxHealth)), 0);
                    //Vector2 FriendlyHPBarOffset = new Vector2(26, 3);
                    Vector2 EnemyHPBarOffset = new Vector2(-10, -3f);
                    Vector2 CurrentHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(100 * enemy.HealthPercent / hpBarWidth, 0);
                    Vector2 EndHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(enemyHPPercentAfterCombo, 0);
                    if (enemyHPPercentAfterCombo == 0)
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Green);
                    else
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Yellow);
                }

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Health after W") && W.IsLearned)
            {
                int hpBarWidth = 96;
                float friendlyHPPercentAfterW = Math.Min(100 * (Gangplank.Health + Calculations.WHeal()) / Gangplank.MaxHealth, 100);
                Vector2 FriendlyHPBarOffset = new Vector2(26, 7f);
                Vector2 CurrentHP = Gangplank.HPBarPosition + FriendlyHPBarOffset + new Vector2(100 * Gangplank.HealthPercent / hpBarWidth, 0);
                Vector2 EndHP = Gangplank.HPBarPosition + FriendlyHPBarOffset + new Vector2(100 * friendlyHPPercentAfterW / hpBarWidth, 0);
                if (friendlyHPPercentAfterW == 100)
                    Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Green);
                else
                    Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Yellow);
            }
        }

        private static void Game_OnTick(EventArgs args) 
        {
            if (Gangplank.IsDead)
                return;
           
            GangplankFunctions.didActionThisTick = false;

            List<Barrel> removeBarrels = new List<Barrel>();
            foreach (Barrel b in barrels)
                if (!b.barrel.MeetsCriteria())
                    removeBarrels.Add(b);
            foreach (Barrel b in removeBarrels)
                barrels.Remove(b);

            GangplankFunctions.AutoHarrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                GangplankFunctions.Flee();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                GangplankFunctions.Combo();
            if (MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Activate Killsteal"))
                GangplankFunctions.KS();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                GangplankFunctions.LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                GangplankFunctions.Harrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                GangplankFunctions.JungleClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                GangplankFunctions.LaneClear();
            if (MenuHandler.Settings.GetCheckboxValue("Auto-Place Barrels with 3 stacks"))
                GangplankFunctions.AutoBarrel();
            if (MenuHandler.Items.GetCheckboxValue("Auto W"))
                GangplankFunctions.AutoW();

            if (Gangplank.PentaKills > currentPentaKills)
            {
                Chat.Print("Nice Penta! Make sure to screenshot it and post it on the UnsignedGangplank thread to show off!");
                
                currentPentaKills = Gangplank.PentaKills;
            }
        }

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
            if (sender.Name == "Barrel" && sender.Team == Gangplank.Team && args.Buff.Name == "gangplankebarreldecaysound")
                    GetBarrelAtPosition(sender.Position).timeSinceLastDecay = Game.Time;
        }

        //Draw E chains should not be drawing multiple lines between each barrel. It should also merge them into one polygon
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Gangplank.IsDead)
                return;
            Menu menu = MenuHandler.Drawing;

            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            if (menu.GetCheckboxValue("Draw Q") && Q.IsLearned)
                Q.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw E") && E.IsLearned)
                E.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw E Circle on Mouse") && E.IsLearned)
                Game.CursorPos.DrawCircle((int)barrelRadius, Color.Blue);
            if (menu.GetCheckboxValue("Draw Enemies Killable with E") && E.IsLearned)
                foreach (Obj_AI_Base ob in EntityManager.Enemies.Where(ob => ob.VisibleOnScreen))
                    if (ob.MeetsCriteria() && ob.Health <= Calculations.E(ob, false))
                        ob.DrawCircle(65, Color.Yellow);
            if (menu.GetCheckboxValue("Draw Enemies Killable with E + Q") && E.IsLearned && Q.IsLearned)
                foreach (Obj_AI_Base ob in EntityManager.Enemies.Where(ob=>ob.VisibleOnScreen))
                    if (ob.MeetsCriteria() && ob.Health <= Calculations.E(ob, true))
                        ob.DrawCircle(50, Color.Green);

            if (menu.GetCheckboxValue("Draw Silver Serpent Notifier") && Gangplank.GetBuffCount("gangplankbilgewatertoken") >= 500)
                Drawing.DrawText(Gangplank.Position.WorldToScreen(), drawColor, "You have enough silver serpents", 15);

            if (menu.GetCheckboxValue("Draw Shiny Barrels"))
                foreach (Barrel b in barrels.Where(ob => ob.barrel.VisibleOnScreen))
                    b.barrel.DrawCircle((int)barrelRadius, Color.Gold, 50);


            if (menu.GetCheckboxValue("Draw E Chains") && E.IsLearned)
            {
                List<Tuple<Geometry.Polygon.Circle, int>> polygons = new List<Tuple<Geometry.Polygon.Circle, int>>();
                foreach(Barrel b in barrels)
                    polygons.Add(Tuple.Create(new Geometry.Polygon.Circle(b.barrel.Position, barrelRadius), (int)b.barrel.Position.Z));

                foreach (Tuple<Geometry.Polygon.Circle, int> b in polygons)
                {
                    b.Item1.CenterOfPolygon().To3D(b.Item2).DrawCircle((int)barrelRadius, Color.Red, 1);
                    foreach (Tuple<Geometry.Polygon.Circle, int> linkedBarrel in polygons.Where(a=>a.Item1.CenterOfPolygon().Distance(b.Item1.CenterOfPolygon()) <= barrelDiameter))
                        Drawing.DrawLine(linkedBarrel.Item1.CenterOfPolygon().To3D(linkedBarrel.Item2).WorldToScreen(), b.Item1.CenterOfPolygon().To3D(b.Item2).WorldToScreen(), 5, drawColor);
                }
            }
            
            if (menu.GetCheckboxValue("Draw Killable Text"))
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a => a.VisibleOnScreen && a.MeetsCriteria() && a.Health < Gangplank.ComboDamage(a)))
                    Drawing.DrawText(enemy.Position.WorldToScreen(), System.Drawing.Color.GreenYellow, "Killable", 15);
        }
    }
}
