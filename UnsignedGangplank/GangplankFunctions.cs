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
    class GangplankFunctions
    {
        public static bool didActionThisTick = false;

        public static AIHeroClient Gangplank { get { return ObjectManager.Player; } }
        
        //working and tested
        public static void LaneClear()
        {
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();
            Menu menu = MenuHandler.LaneClear;
            
            if (menu.GetCheckboxValue("Use Q"))
            {
                if (menu.GetCheckboxValue("Use Q to kill barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                    CastQOnBestBarrel(enemies, false, menu.GetSliderValue("Minions to use Q on Barrel"));

                if (menu.GetComboBoxText("Q On Enemy Logic:") != "Never")
                {
                    if (menu.GetComboBoxText("Q On Enemy Logic:") == "No Barrels Around" && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, false);
                    else if (menu.GetComboBoxText("Q On Enemy Logic:") == "Last Hit")
                        CastQ(enemies, true);
                    else if (menu.GetComboBoxText("Q On Enemy Logic:") == "No Barrels Around and Last Hit" && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, true);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Chain Barrels") && Gangplank.NearbyBarrelCount(Program.E.Range) != 0)
                    CastEOnBestBarrelChainPosition(enemies, menu.GetSliderValue("Minions to use Barrel"), false);

                if (menu.GetCheckboxValue("Create First Barrel") && Gangplank.NearbyBarrelCount(Program.E.Range) == 0 && Gangplank.NearbyBarrelCount(Program.E.Range) == 0)
                    CastEOnBestPosition(enemies, menu.GetSliderValue("Minions to use Barrel"), false);
            }

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);

            if (menu.GetCheckboxValue("Auto-Attack Barrels if Q on cooldown") && (!Program.Q.IsReady() || !Program.Q.IsLearned)
                && Orbwalker.CanAutoAttack)
            {
                Barrel b = Calculations.getBestBarrel(enemies, false, menu.GetSliderValue("Minions to Auto-Attack Barrel"), false);

                if (b != null && b.EHitNumber(enemies) >= menu.GetSliderValue("Minions to Auto-Attack Barrel"))
                {
                    Orbwalker.ForcedTarget = b.barrel;
                    didActionThisTick = true;
                }
            }
        }

        //working and tested
        public static void LastHit()
        {
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();
            Menu menu = MenuHandler.LastHit;

            if (menu.GetCheckboxValue("Use Q"))
            {
                if (menu.GetCheckboxValue("Use Q to kill barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                    CastQOnBestBarrel(enemies, true, menu.GetSliderValue("Minions to use Q on Barrel"));

                if (menu.GetCheckboxValue("Use Q on enemies"))
                {
                    if (menu.GetCheckboxValue("Use Q on enemies only if no barrels around") && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, true);
                    else if (!menu.GetCheckboxValue("Use Q on enemies only if no barrels around") || Gangplank.NearbyBarrelCount(Program.Q.Range) != 0)
                        CastQ(enemies, true);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Chain Barrels"))
                    CastEOnBestBarrelChainPosition(enemies, menu.GetSliderValue("Minions to use Barrel"), false);

                if (menu.GetCheckboxValue("Create First Barrel") && Gangplank.NearbyBarrelCount(Program.E.Range) == 0)
                    CastEOnBestPosition(enemies, menu.GetSliderValue("Minions to use Barrel"), false);
            }
            
            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, true);

            if (menu.GetCheckboxValue("Auto-Attack Barrels if Q on cooldown") && (!Program.Q.IsReady() || !Program.Q.IsLearned)
                && Orbwalker.CanAutoAttack)
            {
                Barrel b = Calculations.getBestBarrel(enemies, false, menu.GetSliderValue("Minions to Auto-Attack Barrel"), true);

                if (b != null && b.EHitNumber(enemies.Where(a=>a.Health <= Calculations.E(a, false)).ToList()) >= menu.GetSliderValue("Minions to Auto-Attack Barrel"))
                {
                    Orbwalker.ForcedTarget = b.barrel;
                    didActionThisTick = true;
                }
            }
        }

        //working and tested
        public static void JungleClear()
        {
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList();
            Menu menu = MenuHandler.JungleClear;

            if (menu.GetCheckboxValue("Use Q"))
            {
                if (menu.GetCheckboxValue("Use Q to kill barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                    CastQOnBestBarrel(enemies, false, menu.GetSliderValue("Minions to use Q on Barrel"));

                if (menu.GetCheckboxValue("Use Q on enemies"))
                {
                    if (menu.GetCheckboxValue("Use Q on enemies only if no barrels around") && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, false);
                    else if (!menu.GetCheckboxValue("Use Q on enemies only if no barrels around") || Gangplank.NearbyBarrelCount(Program.Q.Range) != 0)
                        CastQ(enemies, false);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Chain Barrels"))
                    CastEOnBestBarrelChainPosition(enemies, menu.GetSliderValue("Minions to use Barrel"), false);

                if (menu.GetCheckboxValue("Create First Barrel") && Gangplank.NearbyBarrelCount(Program.E.Range) == 0)
                    CastEOnBestPosition(enemies, menu.GetSliderValue("Minions to use Barrel"), false);
            }

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);

            if (menu.GetCheckboxValue("Auto-Attack Barrels if Q on cooldown") && (!Program.Q.IsReady() || !Program.Q.IsLearned)
                && Orbwalker.CanAutoAttack)
            {
                Barrel b = Calculations.getBestBarrel(enemies, false, menu.GetSliderValue("Minions to Auto-Attack Barrel"), false);

                if (b != null && b.EHitNumber(enemies) >= menu.GetSliderValue("Minions to Auto-Attack Barrel"))
                {
                    Orbwalker.ForcedTarget = b.barrel;
                    didActionThisTick = true;
                }
            }
        }

        //working and tested
        public static void KS()
        {
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            Menu menu = MenuHandler.Killsteal;

            if (menu.GetCheckboxValue("Use Q"))
            {
                if (menu.GetCheckboxValue("Use Q to kill barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                    CastQOnBestBarrel(enemies, true, 1);

                if (menu.GetCheckboxValue("Use Q on enemies"))
                {
                    if (menu.GetCheckboxValue("Use Q on enemies only if no barrels around") && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, true);
                    else if (!menu.GetCheckboxValue("Use Q on enemies only if no barrels around") || Gangplank.NearbyBarrelCount(Program.Q.Range) != 0)
                        CastQ(enemies, true);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Chain Barrels"))
                    CastEOnBestBarrelChainPosition(enemies, 1, true);

                if (menu.GetCheckboxValue("Create First Barrel") && Gangplank.NearbyBarrelCount(Program.E.Range) == 0)
                    CastEOnBestPosition(enemies, 1, true);
            }

            if (menu.GetCheckboxValue("Use R"))
                CastR(enemies, 1, true);

            if (menu.GetCheckboxValue("Use Ignite"))
                CastIgnite(enemies, true);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, true);

            if (menu.GetCheckboxValue("Auto-Attack Barrels if Q on cooldown") && (!Program.Q.IsReady() || !Program.Q.IsLearned)
                && Orbwalker.CanAutoAttack)
            {
                Barrel b = Calculations.getBestBarrel(enemies, false, 1, true);

                if (b != null && b.EHitNumber(enemies.Where(a => a.Health <= Calculations.E(a, false)).ToList()) >= 1)
                {
                    Orbwalker.ForcedTarget = b.barrel;
                    didActionThisTick = true;
                }
            }
        }

        //working and tested
        public static void Harrass()
        {
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            Menu menu = MenuHandler.Harass;

            if (menu.GetCheckboxValue("Use Q"))
            {
                if (menu.GetCheckboxValue("Use Q to kill barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                    CastQOnBestBarrel(enemies, false, menu.GetSliderValue("Enemies to use Q on Barrel"));

                if (menu.GetCheckboxValue("Use Q on enemies"))
                {
                    if (menu.GetCheckboxValue("Use Q on enemies only if no barrels around") && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, false);
                    else if (!menu.GetCheckboxValue("Use Q on enemies only if no barrels around") || Gangplank.NearbyBarrelCount(Program.Q.Range) != 0)
                        CastQ(enemies, false);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Chain Barrels"))
                    CastEOnBestBarrelChainPosition(enemies, menu.GetSliderValue("Enemies to use Barrel"), false);

                if (menu.GetCheckboxValue("Create First Barrel") && Gangplank.NearbyBarrelCount(Program.E.Range) == 0)
                    CastEOnBestPosition(enemies, menu.GetSliderValue("Enemies to use Barrel"), false);
            }

            if (menu.GetCheckboxValue("Use R"))
                CastR(enemies, menu.GetSliderValue("Enemies to use R"), false);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);

            if (menu.GetCheckboxValue("Auto-Attack Barrels if Q on cooldown") && (!Program.Q.IsReady() || !Program.Q.IsLearned)
                && Orbwalker.CanAutoAttack)
            {
                Barrel b = Calculations.getBestBarrel(enemies, false, menu.GetSliderValue("Enemies to Auto-Attack Barrel"), false);

                if (b != null && b.EHitNumber(enemies) >= menu.GetSliderValue("Enemies to Auto-Attack Barrel"))
                {
                    Orbwalker.ForcedTarget = b.barrel;
                    didActionThisTick = true;
                }
            }
        }

        //working and tested
        public static void Combo()
        {
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            Menu menu = MenuHandler.Combo;

            if (menu.GetCheckboxValue("Use Q"))
            {
                if (menu.GetCheckboxValue("Use Q to kill barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                    CastQOnBestBarrel(enemies, false, menu.GetSliderValue("Enemies to use Q on Barrel"));

                if (menu.GetCheckboxValue("Use Q on enemies"))
                {
                    if (menu.GetCheckboxValue("Use Q on enemies only if no barrels around") && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, false);
                    else if (!menu.GetCheckboxValue("Use Q on enemies only if no barrels around") || Gangplank.NearbyBarrelCount(Program.Q.Range) != 0)
                        CastQ(enemies, false);
                }
            }

            if (menu.GetCheckboxValue("Use W"))
            {
                if (menu.GetCheckboxValue("Use W to remove CC") && Gangplank.CanCancleCC(false))
                    CastW();

                if (menu.GetCheckboxValue("Use W to remove slows") && Gangplank.CanCancleCC(true))
                    CastW();
            }

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Chain Barrels"))
                    CastEOnBestBarrelChainPosition(enemies, menu.GetSliderValue("Enemies to use Barrel"), false);

                if (menu.GetCheckboxValue("Create First Barrel") && Gangplank.NearbyBarrelCount(Program.E.Range) == 0)
                    CastEOnBestPosition(enemies, menu.GetSliderValue("Enemies to use Barrel"), false);
            }

            if (menu.GetCheckboxValue("Use R"))
                CastR(enemies, menu.GetSliderValue("Enemies to use R"), false);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);

            if (menu.GetCheckboxValue("Auto-Attack Barrels if Q on cooldown") && (!Program.Q.IsReady() || !Program.Q.IsLearned)
                && Orbwalker.CanAutoAttack)
            {
                Barrel b = Calculations.getBestBarrel(enemies, false, menu.GetSliderValue("Enemies to Auto-Attack Barrel"), false);

                if (b != null && b.EHitNumber(enemies) >= menu.GetSliderValue("Enemies to Auto-Attack Barrel"))
                {
                    Orbwalker.ForcedTarget = b.barrel;
                    didActionThisTick = true;
                }
            }
        }

        //needs testing
        public static void Flee()
        {
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            Menu menu = MenuHandler.Flee;
            
            if (menu.GetCheckboxValue("Slow Enemies with Barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                CastQOnBestBarrel(enemies, false, 1);

            if (menu.GetCheckboxValue("Use Passive") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                CastQ(Gangplank.Nearby1HPBarrel(Program.Q.Range));

            if (menu.GetCheckboxValue("Ult for slow"))
                CastR(enemies.Where(a=>a.IsInRange(Gangplank, 1200f)).ToList(), 1, false);
        }

        //working and tested
        public static void AutoHarrass()
        {
            if (Gangplank.IsRecalling() || Gangplank.IsUnderEnemyturret() || 
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                return;

            Menu menu = MenuHandler.AutoHarass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q"))
            {
                if (menu.GetCheckboxValue("Use Q to kill barrels") && Gangplank.NearbyBarrelCount(Program.Q.Range) >= 1)
                    CastQOnBestBarrel(enemies, false, 1);

                if (menu.GetCheckboxValue("Use Q on enemies"))
                {
                    if (menu.GetCheckboxValue("Use Q on enemies only if no barrels around") && Gangplank.NearbyBarrelCount(Program.Q.Range) == 0)
                        CastQ(enemies, false);
                    else if (!menu.GetCheckboxValue("Use Q on enemies only if no barrels around") || Gangplank.NearbyBarrelCount(Program.Q.Range) != 0)
                        CastQ(enemies, false);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Chain Barrels"))
                    CastEOnBestBarrelChainPosition(enemies, 1, false);

                if (menu.GetCheckboxValue("Create First Barrel") && Gangplank.NearbyBarrelCount(Program.E.Range) == 0)
                    CastEOnBestPosition(enemies, 1, false);
            }

            if (menu.GetCheckboxValue("Auto-Attack Barrels if Q on cooldown") && (!Program.Q.IsReady() || !Program.Q.IsLearned)
                && Orbwalker.CanAutoAttack)
            {
                Barrel b = Calculations.getBestBarrel(enemies, false, 1, false);

                if (b != null && b.EHitNumber(enemies) >= 1)
                {
                    Orbwalker.ForcedTarget = b.barrel;
                    didActionThisTick = true;
                }
            }
        }

        //working and tested
        public static void AutoW()
        {
            Menu menu = MenuHandler.Items;

            if (menu.GetCheckboxValue("Use W to remove CC") && Gangplank.CanCancleCC(false))
                CastW();

            if (menu.GetCheckboxValue("Use W to remove slows") && Gangplank.CanCancleCC(true))
                CastW();

            if (menu.GetSliderValue("HP to use W") >= Gangplank.HealthPercent && menu.GetSliderValue("Mana to use W") >= Gangplank.ManaPercent)
                CastW();
        }

        //working and tested
        public static void AutoBarrel()
        {
            if (didActionThisTick || !Program.E.IsReady() || Program.E.AmmoQuantity != 3)
                return;

            GrassObject bush = ObjectManager.Get<GrassObject>().Where(
                a =>
                a.Distance(Gangplank) <= Program.E.Range
                && a.NearbyBarrelCount(Program.barrelDiameter) == 0).OrderBy(a => a.Distance(Gangplank)).FirstOrDefault();

            if (bush != null)
                CastE(bush.Position);
        }

        //working and tested
        public static bool CastItems(List<Obj_AI_Base> enemies, bool ks)
        {
            Menu menu = MenuHandler.Items;
            #region Item Initialization
            InventorySlot QSS = (menu.GetCheckboxValue("Use Quick Silver Sash")) ? Gangplank.GetItem(ItemId.Quicksilver_Sash) : null,
                MercurialsScimitar = (menu.GetCheckboxValue("Use Mercurials Scimitar")) ? Gangplank.GetItem(ItemId.Mercurial_Scimitar) : null,
                RavenousHydra = (menu.GetCheckboxValue("Use Ravenous Hydra")) ? Gangplank.GetItem(ItemId.Ravenous_Hydra) : null,
                TitanicHydra = (menu.GetCheckboxValue("Use Titanic Hydra")) ? Gangplank.GetItem(ItemId.Titanic_Hydra) : null,
                Tiamat = (menu.GetCheckboxValue("Use Tiamat")) ? Gangplank.GetItem(ItemId.Tiamat) : null,
                Youmuus = (menu.GetCheckboxValue("Use Youmuus")) ? Gangplank.GetItem(ItemId.Youmuus_Ghostblade) : null,
                BOTRK = (menu.GetCheckboxValue("Use Blade of the Ruined King")) ? Gangplank.GetItem(ItemId.Blade_of_the_Ruined_King) : null,
                BilgewaterCutlass = (menu.GetCheckboxValue("Use Bilgewater Cutlass")) ? Gangplank.GetItem(ItemId.Bilgewater_Cutlass) : null,
                HextechGunblade = (menu.GetCheckboxValue("Use Hextech Gunblade")) ? Gangplank.GetItem(ItemId.Hextech_Gunblade) : null,
                HealthPotion = Gangplank.GetItem(ItemId.Health_Potion),
                RefillablePotion = Gangplank.GetItem(ItemId.Refillable_Potion),
                CorruptingPotion = Gangplank.GetItem(ItemId.Corrupting_Potion);
            #endregion

            #region QSS
            if (!didActionThisTick &&
                QSS.MeetsCriteria() &&
                Gangplank.CanCancleCC(menu.GetCheckboxValue("Use QSS to remove slows")))
                didActionThisTick = QSS.Cast();
            #endregion

            #region Mercurials Scimitar
            if (!didActionThisTick &&
                MercurialsScimitar.MeetsCriteria() &&
                Gangplank.CanCancleCC(menu.GetCheckboxValue("Use Mercurials Scimitar to remove slows")))
                didActionThisTick = MercurialsScimitar.Cast();
            #endregion

            #region Ravenous Hydra
            if (!didActionThisTick &&
                RavenousHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Gangplank, 400)).FirstOrDefault() != null
                && Gangplank.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a=> a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Gangplank, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null))
                didActionThisTick = RavenousHydra.Cast();
            #endregion

            #region Titanic Hydra
            if (!didActionThisTick &&
                TitanicHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Gangplank, Gangplank.GetAutoAttackRange())).FirstOrDefault() != null
                && Gangplank.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Gangplank, a, ItemId.Titanic_Hydra)).FirstOrDefault() != null))
                didActionThisTick = TitanicHydra.Cast();
            #endregion

            #region Tiamat
            if (!didActionThisTick &&
                Tiamat.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Gangplank, 400)).FirstOrDefault() != null
                && Gangplank.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Gangplank, a, ItemId.Tiamat)).FirstOrDefault() != null))
                didActionThisTick = Tiamat.Cast();
            #endregion

            #region Youmuus
            if (!didActionThisTick &&
                Youmuus.MeetsCriteria()
                && Gangplank.CountEnemyHeroesInRangeWithPrediction((int)Gangplank.GetAutoAttackRange(), 0) >= 1)
                didActionThisTick = Youmuus.Cast();
            #endregion

            #region Potions
            if (!didActionThisTick &&
                HealthPotion.MeetsCriteria() &&
                Gangplank.HealthPercent <= menu.GetSliderValue("HP to use Potions"))
                didActionThisTick = HealthPotion.Cast();

            if (!didActionThisTick &&
                RefillablePotion.MeetsCriteria() &&
                Gangplank.HealthPercent <= menu.GetSliderValue("HP to use Potions"))
                didActionThisTick = RefillablePotion.Cast();

            if (!didActionThisTick &&
                CorruptingPotion.MeetsCriteria() &&
                Gangplank.HealthPercent <= menu.GetSliderValue("HP to use Potions"))
                didActionThisTick = CorruptingPotion.Cast();
            #endregion

            //all targeted spells that must be used on champions must be called after this
            enemies = enemies.Where(a => a.Type == GameObjectType.AIHeroClient).ToList();

            #region Hextech Gunblade
            if (!didActionThisTick &&
                HextechGunblade.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Gangplank, 700)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Gangplank, a, ItemId.Hextech_Gunblade)).FirstOrDefault() != null))
                didActionThisTick = HextechGunblade.Cast(enemies.OrderBy(a=>a.Health).FirstOrDefault());
            #endregion

            #region BOTRK
            if (!didActionThisTick &&
                BOTRK.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Gangplank, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Gangplank, a, ItemId.Blade_of_the_Ruined_King)).FirstOrDefault() != null))
                didActionThisTick = BOTRK.Cast(enemies.OrderBy(a => a.Health).FirstOrDefault());
            #endregion

            #region Bilgewater Cutlass
            if (!didActionThisTick &&
                BilgewaterCutlass.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Gangplank, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Gangplank, a, ItemId.Bilgewater_Cutlass)).FirstOrDefault() != null))
                didActionThisTick = BilgewaterCutlass.Cast(enemies.OrderBy(a => a.Health).FirstOrDefault());
            #endregion

            return false;
        }
        
        //working and tested
        private static void CastEOnBestBarrelChainPosition(List<Obj_AI_Base> enemies, int enemiesToUseEOn, bool ks)
        {
            if(ks)
                enemies = enemies.Where(a => a.Health <= Calculations.E(a, false)).ToList();

            if (!Program.E.IsReady() || enemies.Count == 0)
                return;

            Vector3 bestChainPositition = GetBestBarrelChainPosition(enemies, enemiesToUseEOn);
            
            CastE(bestChainPositition);
        }

        //working and tested
        public static Vector3 GetBestBarrelChainPosition(List<Obj_AI_Base> enemies, int enemiesToUseEOn)
        {
            List<Barrel> barrelsInQRange = Program.barrels.Where(a => a.barrel.IsInRange(Gangplank, Program.Q.Range) && a.TimeAt1HP - Game.Time <= Calculations.CalculateQTimeToTarget(a.barrel)).ToList();

            int enemiesHit = 0;
            List<Obj_AI_Base> enemiesHitList = new List<Obj_AI_Base>();
            Vector3 bestPosition = new Vector3();

            int eWidthIterationsCap = 4;
            int predictionAngleCap = 15;
            int barrelConnectionExtendRange = 650;

            if (MenuHandler.Settings.GetComboBoxText("Barrel Mode:") == "Best Prediction")
            {
                eWidthIterationsCap = 10;
                predictionAngleCap = 5;
            }
            else if (MenuHandler.Settings.GetComboBoxText("Barrel Mode:") == "Best FPS")
            {
                eWidthIterationsCap = 2;
                predictionAngleCap = 50;
            }
            else if (MenuHandler.Settings.GetComboBoxText("Barrel Mode:") == "Middle Ground")
            {
                eWidthIterationsCap = 4;
                predictionAngleCap = 15;
            }

            if (MenuHandler.Settings.GetComboBoxText("Barrel Range Mode:") == "Max Range")
                barrelConnectionExtendRange = 650;
            else if (MenuHandler.Settings.GetComboBoxText("Barrel Range Mode:") == "Any Position")
                barrelConnectionExtendRange = 0;
            else if (MenuHandler.Settings.GetComboBoxText("Barrel Range Mode:") == "Middle Ground")
                barrelConnectionExtendRange = 350;

            foreach (Barrel b in barrelsInQRange)
            {
                List<Geometry.Polygon.Circle> secondBarrelPosition = new List<Geometry.Polygon.Circle>();

                //does 3 iterations of E width 
                for (int eWidthIteration = 1; eWidthIteration < eWidthIterationsCap; eWidthIteration++)
                {
                    Vector3 extendingPosition = b.barrel.Position + new Vector3(0, eWidthIteration * (Program.barrelDiameter - 20 / 3), 0);

                    //checks every 15 degrees
                    for (int i = 0; i < 360 / predictionAngleCap; i++)
                    {
                        Vector3 endPosition = extendingPosition.To2D().RotateAroundPoint(b.barrel.Position.To2D(), (float)((i * predictionAngleCap) * Math.PI / 180)).To3D((int)b.barrel.Position.Z);
                        Geometry.Polygon.Circle secondBarrel = new Geometry.Polygon.Circle(endPosition, Program.barrelRadius);

                        secondBarrelPosition.Add(secondBarrel);
                    }
                }

                secondBarrelPosition = secondBarrelPosition.Where(a => a.CenterOfPolygon().IsInRange(Gangplank, Program.E.Range)).ToList();
                secondBarrelPosition = secondBarrelPosition.Where(secondBarrel => secondBarrel.CenterOfPolygon().NearbyBarrelCount(barrelConnectionExtendRange) == 0).ToList();
                
                foreach (Geometry.Polygon.Circle secondBarrel in secondBarrelPosition)
                {
                    List<Obj_AI_Base> tempEnemiesHitList = enemies.Where(a => a.Position(250).Distance(secondBarrel.CenterOfPolygon()) <= Program.barrelRadius).ToList();
                    int tempEnemiesHit = tempEnemiesHitList.Count();
                    if (tempEnemiesHit > enemiesHit)
                    {
                        enemiesHit = tempEnemiesHit;
                        enemiesHitList = tempEnemiesHitList;
                        bestPosition = secondBarrel.CenterOfPolygon().To3D((int)b.barrel.Position.Z);
                    }
                }
            }

            if (enemiesHit >= enemiesToUseEOn)
                return bestPosition;
            else
                return Vector3.Zero;
        }

        //working and tested
        private static void CastEOnBestPosition(List<Obj_AI_Base> enemies, int enemiesToUseEOn, bool ks)
        {
            enemies = enemies.Where(a => a.Distance(Gangplank) <= Program.E.Range + (Program.barrelRadius / 2)).ToList();

            if(ks)
                enemies = enemies.Where(a => a.Health <= Calculations.E(a, false)).ToList();

            if (!Program.E.IsReady() || enemies.Count == 0)
                return;
            
            Spell.Skillshot.BestPosition bestPos = Program.E.GetBestCircularCastPosition(enemies);
            
            if (bestPos.CastPosition != Vector3.Zero && bestPos.CastPosition.IsInRange(Gangplank, Program.E.Range) && bestPos.EHitNumber(enemies) >= enemiesToUseEOn)
                CastE(bestPos.CastPosition);
        }

        //working and tested
        private static void CastQOnBestBarrel(List<Obj_AI_Base> enemies, bool ks, int enemiesHit)
        {
            if (ks)
                enemies = enemies.Where(a => a.Health <= Calculations.E(a, true)).ToList();

            if ((!Program.Q.IsReady() || !Program.Q.IsLearned) || didActionThisTick || !Program.barrels.Any(a => a.barrel.IsInRange(Gangplank, Program.Q.Range)) || enemies.Count == 0)
                return;

            Barrel b = Calculations.getBestBarrel(enemies, true, 1, ks);
            
            if (enemies.Count >= 1 && b != null && Calculations.EnemiesHitByBarrel(b, enemies, true) >= enemiesHit)
                didActionThisTick = Program.Q.Cast(b.barrel);
        }

        //working
        public static void CastW()
        {
            if (!Program.W.IsReady())
                return;

            didActionThisTick = Program.W.Cast();
        }

        public static void CastE(Vector3 pos)
        {
            if (!Program.E.IsReady() || pos == Vector3.Zero || pos == default(Vector3) || !pos.IsInRange(Gangplank, Program.E.Range))
                return;

            didActionThisTick = Program.E.Cast(pos);
        }

        public static void CastIgnite(List<Obj_AI_Base> enemies, bool ks)
        {
            if (!Program.Ignite.IsReady() || !enemies.Any(a => a.IsInRange(Gangplank, Program.Ignite.Range)))
                return;

            Obj_AI_Base unit = enemies.Where(a =>
                a.IsInRange(Gangplank, Program.Ignite.Range)
                && (!ks || Calculations.Ignite(a) >= a.Health)
                && a.MeetsCriteria()).FirstOrDefault();

            if (unit != null)
                didActionThisTick =  Program.Ignite.Cast(unit);
        }
        
        public static void CastQ(List<Obj_AI_Base> enemies, bool ks)
        {
            if ((!Program.Q.IsReady() || !Program.Q.IsLearned) || didActionThisTick || !enemies.Any(a => a.IsInRange(Gangplank, Program.Q.Range)))
                return;

            enemies = enemies.Where(a => a.IsInRange(Gangplank, Program.Q.Range)).ToList();

            if (ks)
                enemies = enemies.Where(a => a.Health <= Calculations.Q(a)).ToList();
            
            if (enemies.Count >= 1)
                CastQ(enemies.First());
        }
        public static void CastQ(Obj_AI_Base enemy)
        {
            //barrels must be attacked by CastQOnBestBarrel method
            if ((!Program.Q.IsReady() || !Program.Q.IsLearned) || enemy == null || enemy.Name == "Barrel" || didActionThisTick || !enemy.IsInRange(Gangplank, Program.Q.Range))
                return;

            didActionThisTick = Program.Q.Cast(enemy);
        }

        public static void CastR(List<Obj_AI_Base> enemies, int enemiesToUlt, bool ks)
        {
            if (!Program.R.IsReady() || didActionThisTick)
                return;

            if (ks)
                enemies = enemies.Where(a => a.Health <= (Calculations.RDamagePerWave(a) * 5)).ToList();

            if (enemies.Count() < enemiesToUlt)
                return;

            Spell.Skillshot.BestPosition bestPos = Program.R.GetBestCircularCastPosition(enemies);
            
            if (bestPos.CastPosition != Vector3.Zero && bestPos.RHitNumber(enemies) >= enemiesToUlt)
                didActionThisTick = Program.R.Cast(bestPos.CastPosition);
        }
    }
}
