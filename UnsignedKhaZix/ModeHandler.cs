using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace KhaZix
{
    class ModeHandler
    {
        public static AIHeroClient Champion => Player.Instance;
        public static bool hasDoneActionThisTick = false;
        public static float LastAutoTime = 0;

        public static void Combo()
        {
            Menu menu = MenuHandler.Combo;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use Q"))
                CastQ(enemies, false, menu);
            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false, menu);
            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, menu, -1, true);
            if (menu.GetCheckboxValue("Use R") && Champion.Position.CountEnemyHeroesInRangeWithPrediction((int)Champion.GetAutoAttackRange(), 250) >= 1)
                CastR();
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, false);
        }

        public static void Harass()
        {
            Menu menu = MenuHandler.Harass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use Q"))
                CastQ(enemies, false, menu);
            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false, menu);
            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, menu);
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }

        public static void AutoHarass()
        {
            if (Champion.IsUnderEnemyturret())
                return;

            Menu menu = MenuHandler.AutoHarass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            
            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use Q"))
                CastQ(enemies, false, menu);
            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false, menu);
            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, menu);
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }

        public static void JungleClear()
        {
            Menu menu = MenuHandler.JungleClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList();

            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use Q"))
                CastQ(enemies, false, menu);
            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false, menu, menu.GetCheckboxValue("Use W only for heal"));
            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, menu);
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, true, menu.GetCheckboxValue("Use Smite for HP"));
        }

        public static void Killsteal()
        {
            Menu menu = MenuHandler.Killsteal;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use Q"))
                CastQ(enemies, true, null);
            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, true, null);
            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, true, null);
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);
            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, true);
        }
        
        public static void Flee()
        {
            Menu menu = MenuHandler.Flee;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false, null);
            if (menu.GetCheckboxValue("E to Cursor"))
                CastE(Game.CursorPos);
            if (menu.GetCheckboxValue("Use R") && Champion.CountEnemyHeroesInRangeWithPrediction(1200, 250) > 1)
                CastR();
        }

        public static void LaneClear()
        {
            Menu menu = MenuHandler.LaneClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use Q") || menu.GetCheckboxValue("Use Q for Last Hit"))
                CastQ(enemies, menu.GetCheckboxValue("Use Q for Last Hit"), menu);
            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false, menu);
            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, menu, menu.GetCheckboxValue("Use E on X enemies")?menu.GetSliderValue("Units to E on"):-1);
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }

        public static void LastHit()
        {
            Menu menu = MenuHandler.LastHit;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

            if (enemies.Count <= 0)
                return;

            if (menu.GetCheckboxValue("Use Q"))
                CastQ(enemies, true, menu);
            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, true, menu);
            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, true, menu);
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);
        }

        public static void CastQ(List<Obj_AI_Base> enemies, bool ks, Menu menu)
        {
            if (hasDoneActionThisTick || !Program.Q.IsReady() || (LastAutoTime.IsWithin(0.5f) || !enemies.Any(a => Champion.IsInAutoAttackRange(a)))
                || (menu != null && Champion.ManaPercent < menu.GetSliderValue("Q Mana %")))
                return;

            enemies = enemies.Where(a => a.IsInRange(Champion, Program.Q.Range)).ToList();

            if (ks)
                enemies = enemies.Where(a => a.Health <= Calculations.Q(a)).ToList();

            if (enemies.Count > 0)
            {
                var bestEnemy = enemies.OrderByDescending(a => a.FlatGoldRewardMod).FirstOrDefault();
                if (menu == MenuHandler.JungleClear && Orbwalker.LastTarget != null && Orbwalker.LastTarget.IsInRange(Champion, Program.Q.Range) && (!ks || Orbwalker.LastTarget.Health < Calculations.Q(Orbwalker.LastTarget as Obj_AI_Base)))
                    bestEnemy = Orbwalker.LastTarget as Obj_AI_Base;
                if (bestEnemy != null)
                    hasDoneActionThisTick = Program.Q.Cast(bestEnemy);
            }
        }

        public static void CastW(List<Obj_AI_Base> enemies, bool ks, Menu menu, bool onlyForHeal = false)
        {
            if (hasDoneActionThisTick || !Program.W.IsReady() || Orbwalker.IsAutoAttacking
                || (menu != null && Champion.ManaPercent < menu.GetSliderValue("W Mana %")))
                return;

            var rangeCheck = Program.W.Range;
            if (onlyForHeal)
                rangeCheck = 275;

            enemies = enemies.Where(a => a.IsInRange(Champion, rangeCheck)).ToList();

            if (ks)
                enemies = enemies.Where(a => a.Health <= Calculations.W(a)).ToList();

            if (enemies.Count > 0)
            {
                if(Program.W.Name.Contains("Long"))
                {
                    //TODO
                    //Include prediction for Left/Right missiles
                    PredictionResult bestPrediction = null;
                    foreach (Obj_AI_Base enemy in enemies)
                    {
                        var prediction = Program.W.GetPrediction(enemy);
                        if (bestPrediction == null || prediction.HitChance > bestPrediction.HitChance)
                            bestPrediction = prediction;
                    }

                    if (bestPrediction != null)
                        hasDoneActionThisTick = Program.W.Cast(bestPrediction.CastPosition);
                }
                else
                {
                    PredictionResult bestPrediction = null;
                    foreach(Obj_AI_Base enemy in enemies)
                    {
                        var prediction = Program.W.GetPrediction(enemy);
                        if (bestPrediction == null || prediction.HitChance > bestPrediction.HitChance)
                            bestPrediction = prediction;
                    }

                    if (bestPrediction != null)
                        hasDoneActionThisTick = Program.W.Cast(bestPrediction.CastPosition);
                }
            }
        }

        public static void CastE(List<Obj_AI_Base> enemies, bool ks, Menu menu, int enemiesHit = -1, bool eIntoAARange = false)
        {
            if (hasDoneActionThisTick || !Program.E.IsReady() || Orbwalker.IsAutoAttacking || (menu != null && Champion.ManaPercent < menu.GetSliderValue("E Mana %")))
                return;

            if (eIntoAARange)
                Program.E.Range += (uint)Champion.GetAutoAttackRange();

            enemies = enemies.Where(a => a.IsInRange(Champion, Program.E.Range)).ToList();

            if (ks)
                enemies = enemies.Where(a => a.Health <= Calculations.E(a)).ToList();

            if (enemies.Count > 0)
            {
                PredictionResult bestPrediction = null;
                foreach (Obj_AI_Base enemy in enemies)
                {
                    var prediction = Program.E.GetPrediction(enemy);
                    if (enemiesHit == -1)
                    {
                        if (bestPrediction == null || prediction.HitChance > bestPrediction.HitChance)
                            bestPrediction = prediction;
                    }
                    else
                        if ((bestPrediction == null && prediction.CastPosition.CountEnemyMinionsInRangeWithPrediction(Program.E.Radius, 250) >= enemiesHit)
                        || prediction.CastPosition.CountEnemyMinionsInRangeWithPrediction(Program.E.Radius, 250) >= bestPrediction.CastPosition.CountEnemyMinionsInRangeWithPrediction(Program.E.Radius, 250))
                            bestPrediction = prediction;
                }

                if (bestPrediction != null)
                    CastE(bestPrediction.CastPosition);
            }

            if (eIntoAARange)
                Program.E.Range -= (uint)Champion.GetAutoAttackRange();
        }
        public static void CastE(Vector3 pos)
        {
            if (hasDoneActionThisTick || pos == null || pos.IsZero || !Program.E.IsReady())
                return;

            hasDoneActionThisTick = Program.E.Cast((pos.IsInRange(Champion, Program.E.Range))?pos:Champion.Position.Extend(pos, Program.E.Range - 1).To3D());
        }

        public static void CastR()
        {
            if (hasDoneActionThisTick || !Program.E.IsReady())
                return;

            hasDoneActionThisTick = Program.R.Cast();
        }

        public static void UseSmite(List<Obj_AI_Base> enemies, bool ks, bool forHP = false)
        {
            enemies = enemies.Where(a => a.MeetsCriteria() && a.IsTargetable && a.IsInRange(Champion, 500)
            && !a.BaseSkinName.ContainsAny(false, "mini", "sru_crab")).ToList();

            enemies = enemies.Where(a =>
                //if it is for hp then the entity must not be dragon/baron/herald/vilemaw
                (forHP && !a.BaseSkinName.ContainsAny(false, "Baron", "Dragon", "Herald", "Spider") && Champion.MissingHealth() >= Calculations.SmiteHeal())
                //OR if it is for ks
                || (ks &&
                //ks on champion
                (a.Type == GameObjectType.AIHeroClient
                //or ks on minion
                || a.Health <= Calculations.Smite(a, "regular")
                ))).ToList();

            if (enemies.Count < 0)
                return;

            Spell.Targeted blueSmite = new Spell.Targeted(Champion.GetSpellSlotFromName("S5_SummonerSmitePlayerGanker"), 500, DamageType.True);
            Spell.Targeted redSmite = new Spell.Targeted(Champion.GetSpellSlotFromName("S5_SummonerSmiteDuel"), 500, DamageType.True);
            Spell.Targeted Smite = new Spell.Targeted(Champion.GetSpellSlotFromName("SummonerSmite"), 500, DamageType.True);

            if (blueSmite.Slot != SpellSlot.Unknown && blueSmite.IsReady())
            {
                var target = enemies.Where(a =>
                //champions
                ((ks && a.Type == GameObjectType.AIHeroClient && a.Health <= Calculations.Smite(a, "blue")) ||
                //minions
                a.Type != GameObjectType.AIHeroClient)).FirstOrDefault();

                if (target != null)
                    hasDoneActionThisTick = blueSmite.Cast(target);
            }

            if (redSmite.Slot != SpellSlot.Unknown && redSmite.IsReady())
            {
                var target = enemies.Where(a => a.IsInRange(Champion, Champion.GetAutoAttackRange())
                //champions
                && ((ks && a.Type == GameObjectType.AIHeroClient && a.Health <= Calculations.Smite(a, "red")) ||
                //minions
                a.Type != GameObjectType.AIHeroClient)).FirstOrDefault();

                if (target != null)
                {
                    hasDoneActionThisTick = redSmite.Cast(target);

                    //if it did smite them, attack them
                    if (hasDoneActionThisTick)
                        Player.IssueOrder(GameObjectOrder.AutoAttack, target);
                }
            }

            if (Smite.Slot != SpellSlot.Unknown && Smite.IsReady())
            {
                var target = enemies.Where(a => a.Type != GameObjectType.AIHeroClient).FirstOrDefault();

                if (target != null)
                    hasDoneActionThisTick = Smite.Cast(target);
            }
        }

        public static void UseItems(List<Obj_AI_Base> enemies, bool ks)
        {
            #region Item Initialization
            InventorySlot QSS = (MenuHandler.Items.GetCheckboxValue("Use Quick Silver Sash")) ? Champion.GetItem(ItemId.Quicksilver_Sash) : null,
                MercurialsScimitar = (MenuHandler.Items.GetCheckboxValue("Use Mercurials Scimitar")) ? Champion.GetItem(ItemId.Mercurial_Scimitar) : null,
                RavenousHydra = (MenuHandler.Items.GetCheckboxValue("Use Ravenous Hydra")) ? Champion.GetItem(ItemId.Ravenous_Hydra) : null,
                TitanicHydra = (MenuHandler.Items.GetCheckboxValue("Use Titanic Hydra")) ? Champion.GetItem(ItemId.Titanic_Hydra) : null,
                Tiamat = (MenuHandler.Items.GetCheckboxValue("Use Tiamat")) ? Champion.GetItem(ItemId.Tiamat) : null,
                Youmuus = (MenuHandler.Items.GetCheckboxValue("Use Youmuus")) ? Champion.GetItem(ItemId.Youmuus_Ghostblade) : null,
                BOTRK = (MenuHandler.Items.GetCheckboxValue("Use Blade of the Ruined King")) ? Champion.GetItem(ItemId.Blade_of_the_Ruined_King) : null,
                BilgewaterCutlass = (MenuHandler.Items.GetCheckboxValue("Use Bilgewater Cutlass")) ? Champion.GetItem(ItemId.Bilgewater_Cutlass) : null,
                HextechGunblade = (MenuHandler.Items.GetCheckboxValue("Use Hextech Gunblade")) ? Champion.GetItem(ItemId.Hextech_Gunblade) : null;
            #endregion

            #region QSS
            if (!hasDoneActionThisTick &&
                QSS.MeetsCriteria() &&
                Champion.CanCancleCC())
                hasDoneActionThisTick = QSS.Cast();
            #endregion

            #region Mercurials Scimitar
            if (!hasDoneActionThisTick &&
                MercurialsScimitar.MeetsCriteria() &&
                Champion.CanCancleCC())
                hasDoneActionThisTick = MercurialsScimitar.Cast();
            #endregion

            #region Ravenous Hydra
            if (!hasDoneActionThisTick &&
                RavenousHydra.MeetsCriteria()
                && (LastAutoTime.IsWithin(0.5f) && enemies.Any(a => a.IsInRange(Champion, Champion.GetAutoAttackRange())) || enemies.Any(a => a.IsInRange(Champion, 400)))
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Champion, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null)
                && (
                enemies.Where(a => a.Type == GameObjectType.AIHeroClient && a.IsInRange(Champion, 400)).Count() >= MenuHandler.Items.GetSliderValue("Champions to use Tiamat/Ravenous Hydra on")
                || enemies.Where(a => a.Type != GameObjectType.AIHeroClient && a.IsInRange(Champion, 400)).Count() >= MenuHandler.Items.GetSliderValue("Minions to use Tiamat/Ravenous Hydra on")))
                hasDoneActionThisTick = RavenousHydra.Cast();
            #endregion

            #region Titanic Hydra
            if (!hasDoneActionThisTick &&
                TitanicHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Champion, Champion.GetAutoAttackRange())).FirstOrDefault() != null
                && LastAutoTime.IsWithin(0.5f)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Champion, a, ItemId.Titanic_Hydra)).FirstOrDefault() != null))
                hasDoneActionThisTick = TitanicHydra.Cast();
            #endregion

            #region Tiamat
            if (!hasDoneActionThisTick &&
                Tiamat.MeetsCriteria()
                && (LastAutoTime.IsWithin(0.5f) && enemies.Any(a=>a.IsInRange(Champion, Champion.GetAutoAttackRange())) || enemies.Any(a => a.IsInRange(Champion, 400)))
                && enemies.Where(a => a.IsInRange(Champion, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Champion, a, ItemId.Tiamat)).FirstOrDefault() != null)
                && (
                enemies.Where(a => a.Type == GameObjectType.AIHeroClient && a.IsInRange(Champion, 400)).Count() >= MenuHandler.Items.GetSliderValue("Champions to use Tiamat/Ravenous Hydra on")
                || enemies.Where(a => a.Type != GameObjectType.AIHeroClient && a.IsInRange(Champion, 400)).Count() >= MenuHandler.Items.GetSliderValue("Minions to use Tiamat/Ravenous Hydra on")))
                hasDoneActionThisTick = Tiamat.Cast();
            #endregion

            #region Youmuus
            if (!hasDoneActionThisTick &&
                Youmuus.MeetsCriteria()
                && Champion.CountEnemyHeroesInRangeWithPrediction((int)Champion.GetAutoAttackRange(), 0) >= 1)
                hasDoneActionThisTick = Youmuus.Cast();
            #endregion

            //all targeted spells that must be used on champions must be called after this
            enemies = enemies.Where(a => a.Type == GameObjectType.AIHeroClient).ToList();
            var target = enemies.OrderBy(a => a.Health).FirstOrDefault();

            #region Hextech Gunblade
            if (!hasDoneActionThisTick &&
                target != null
                && HextechGunblade.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Champion, 700)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Champion, a, ItemId.Hextech_Gunblade)).FirstOrDefault() != null))
                hasDoneActionThisTick = HextechGunblade.Cast(target);
            #endregion

            #region BOTRK
            if (!hasDoneActionThisTick &&
                target != null
                && BOTRK.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Champion, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Champion, a, ItemId.Blade_of_the_Ruined_King)).FirstOrDefault() != null))
                hasDoneActionThisTick = BOTRK.Cast(target);
            #endregion

            #region Bilgewater Cutlass
            if (!hasDoneActionThisTick &&
                target != null
                && BilgewaterCutlass.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Champion, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Champion, a, ItemId.Bilgewater_Cutlass)).FirstOrDefault() != null))
                hasDoneActionThisTick = BilgewaterCutlass.Cast(target);
            #endregion
        }
    }
}