using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedVi
{
    class ModeHandler
    {
        public static AIHeroClient Vi;
        public static bool hasDoneActionThisTick = false;

        public static void Combo()
        {
            Menu menu = MenuHandler.Combo;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria()).ToList().ToObj_AI_BaseList();
            Obj_AI_Base Qtarget = enemies.Where(a => a.IsInRange(Vi, Program.Q.MaximumRange) && a.MeetsCriteria()).OrderBy(a=>a.Health).FirstOrDefault();

            if (menu.GetCheckboxValue("Use Q") && Qtarget != null && Program.Q.IsReady())
            {
                List<Obj_AI_Base> QEnemies = enemies.Where(a => a.IsInRange(Vi, Program.Q.Range())).ToList();

                if (QEnemies.Count() >= 1 && !Program.Q.IsCharging)
                    ChargeQ();
                else if (Qtarget.IsInRange(Vi, Program.Q.Range()))
                    CastQ(Program.Q.GetBestLinearCastPosition(new List<Obj_AI_Base>() { Qtarget }).CastPosition);
            }

            if(menu.GetCheckboxValue("Use E"))
                CastE(enemies);

            if (menu.GetCheckboxValue("Use R"))
                CastR(enemies);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, false);
            
            if (menu.GetCheckboxValue("Use Ignite"))
                UseIgnite(enemies, true);
        }
        
        public static void JungleClear()
        {
            Menu menu = MenuHandler.JungleClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.Monsters.Where(a => a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q") && Program.Q.IsReady())
            {
                List<Obj_AI_Base> QEnemies = enemies.Where(a => a.IsInRange(Vi, Program.Q.Range())).ToList();

                if (QEnemies.Count() >= 1 && !Program.Q.IsCharging)
                    ChargeQ();
                else
                {
                    Spell.Skillshot.BestPosition bestPos = Program.Q.GetBestLinearCastPosition(QEnemies);

                    if (bestPos.CastPosition != null && bestPos.CastPosition != Vector3.Zero && bestPos.CastPosition.IsInRange(Vi, Program.Q.Range()) && bestPos.HitNumber > 0)
                        CastQ(bestPos.CastPosition);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies);
            
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, true);
        }
        
        public static void Killsteal()
        {
            Menu menu = MenuHandler.Killsteal;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q") && Program.Q.IsReady())
            {
                List<Obj_AI_Base> QEnemies = enemies.Where(a => a.Health <= Calculations.Q(a, 1.25f) && a.IsInRange(Vi, Program.Q.Range())).ToList();
                if (QEnemies.Count >= 1 && !Program.Q.IsCharging)
                    ChargeQ();
                else
                {
                    QEnemies = enemies.Where(a => a.Health <= Calculations.Q(a, Program.Q.TimeSinceCharge()) && a.IsInRange(Vi, Program.Q.Range())).ToList();
                    Spell.Skillshot.BestPosition bestPos = Program.Q.GetBestLinearCastPosition(QEnemies, 0, Vi.Position.To2D());

                    if (bestPos.CastPosition != null && bestPos.CastPosition != Vector3.Zero && bestPos.CastPosition.IsInRange(Vi, Program.Q.Range()) && bestPos.HitNumber > 0)
                        CastQ(bestPos.CastPosition);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies.Where(a=>a.Health <= Calculations.E(a) + Vi.GetAutoAttackDamage(a)).ToList());

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);
            
            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, true);
            
            if (menu.GetCheckboxValue("Use Ignite"))
                UseIgnite(enemies, true);
        }
        
        public static void Flee()
        {
            Menu menu = MenuHandler.Flee;

            if (menu.GetCheckboxValue("Use Q") && Program.Q.IsReady() && !Program.Q.IsCharging)
                ChargeQ();

            if (Program.Q.IsCharging && Program.Q.TimeSinceCharge() >= 1.25f)
                CastQ(Vi.Position.Extend(Game.CursorPos, Program.Q.MaximumRange).To3D((int)Vi.Position.Z));
        }

        public static void Harass()
        {
            Menu menu = MenuHandler.Harass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria()).ToList().ToObj_AI_BaseList();
            Obj_AI_Base Qtarget = enemies.Where(a => a.IsInRange(Vi, Program.Q.MaximumRange) && a.MeetsCriteria()).OrderBy(a => a.Health).FirstOrDefault();

            if (menu.GetCheckboxValue("Use Q") && Qtarget != null && Program.Q.IsReady())
            {
                List<Obj_AI_Base> QEnemies = enemies.Where(a => a.IsInRange(Vi, Program.Q.MaximumRange)).ToList();
                if (QEnemies.Count >= 1 && !Program.Q.IsCharging)
                    ChargeQ();
                else if (Qtarget.IsInRange(Vi, Program.Q.Range()))
                    CastQ(Program.Q.GetBestLinearCastPosition(new List<Obj_AI_Base>() { Qtarget }).CastPosition);
            }

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies);

            if (menu.GetCheckboxValue("Use R"))
                CastR(enemies);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, false);
        }
        
        public static void LaneClear()
        {
            Menu menu = MenuHandler.LaneClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.Where(a => a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q") && Program.Q.IsReady())
            {
                List<Obj_AI_Base> QEnemies = enemies.Where(a => a.IsInRange(Vi, Program.Q.Range())).ToList();
                if (QEnemies.Count >= 1 && !Program.Q.IsCharging)
                    ChargeQ();
                else
                {
                    Spell.Skillshot.BestPosition bestPos = Program.Q.GetBestLinearCastPosition(QEnemies, 0, Vi.Position.To2D());

                    if (bestPos.CastPosition != null && bestPos.CastPosition != Vector3.Zero && bestPos.CastPosition.IsInRange(Vi, Program.Q.Range()) && bestPos.HitNumber > 0)
                        CastQ(bestPos.CastPosition);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }
        
        public static void LastHit()
        {
            Menu menu = MenuHandler.LastHit;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.Where(a => a.MeetsCriteria()).ToList().ToObj_AI_BaseList();
            
            if (menu.GetCheckboxValue("Use Q") && Program.Q.IsReady())
            {
                List<Obj_AI_Base> QEnemies = enemies.Where(a => a.Health <= Calculations.Q(a, 1.25f) && a.IsInRange(Vi, Program.Q.Range())).ToList();
                if (QEnemies.Count >= 1 && !Program.Q.IsCharging)
                    ChargeQ();
                else
                {
                    QEnemies = enemies.Where(a => a.Health <= Calculations.Q(a, Program.Q.TimeSinceCharge()) && a.IsInRange(Vi, Program.Q.Range())).ToList();
                    Spell.Skillshot.BestPosition bestPos = Program.Q.GetBestLinearCastPosition(QEnemies, 0, Vi.Position.To2D());

                    if (bestPos.CastPosition != null && bestPos.CastPosition != Vector3.Zero && bestPos.CastPosition.IsInRange(Vi, Program.Q.Range()) && bestPos.HitNumber > 0)
                        CastQ(bestPos.CastPosition);
                }
            }

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies.Where(a => a.Health <= Calculations.E(a) + Vi.GetAutoAttackDamage(a)).ToList());
            
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);
        }

        public static void UseItems(List<Obj_AI_Base> enemies, bool ks)
        {
            #region Item Initialization
            InventorySlot QSS = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Quick Silver Sash")) ? Vi.GetItem(ItemId.Quicksilver_Sash) : null,
                MercurialsScimitar = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Mercurials Scimitar")) ? Vi.GetItem(ItemId.Mercurial_Scimitar) : null,
                RavenousHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Ravenous Hydra")) ? Vi.GetItem(ItemId.Ravenous_Hydra) : null,
                TitanicHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Titanic Hydra")) ? Vi.GetItem(ItemId.Titanic_Hydra) : null,
                Tiamat = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Tiamat")) ? Vi.GetItem(ItemId.Tiamat) : null,
                Youmuus = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Youmuus")) ? Vi.GetItem(ItemId.Youmuus_Ghostblade) : null,
                BOTRK = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Blade of the Ruined King")) ? Vi.GetItem(ItemId.Blade_of_the_Ruined_King) : null,
                BilgewaterCutlass = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Bilgewater Cutlass")) ? Vi.GetItem(ItemId.Bilgewater_Cutlass) : null,
                HextechGunblade = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Hextech Gunblade")) ? Vi.GetItem(ItemId.Hextech_Gunblade) : null;
            #endregion

            #region QSS
            if (!hasDoneActionThisTick &&
                QSS.MeetsCriteria() &&
                Vi.CanCancleCC())
                hasDoneActionThisTick = QSS.Cast();
            #endregion

            #region Mercurials Scimitar
            if (!hasDoneActionThisTick &&
                MercurialsScimitar.MeetsCriteria() &&
                Vi.CanCancleCC())
                hasDoneActionThisTick = MercurialsScimitar.Cast();
            #endregion

            #region Ravenous Hydra
            if (!hasDoneActionThisTick &&
                RavenousHydra.MeetsCriteria()
                && Vi.IsAutoCanceling(enemies)
                && enemies.Where(a => a.IsInRange(Vi, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Vi, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null))
                hasDoneActionThisTick = RavenousHydra.Cast();
            #endregion

            #region Titanic Hydra
            if (!hasDoneActionThisTick &&
                TitanicHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Vi, Vi.GetAutoAttackRange())).FirstOrDefault() != null
                && Vi.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Vi, a, ItemId.Titanic_Hydra)).FirstOrDefault() != null))
                hasDoneActionThisTick = TitanicHydra.Cast();
            #endregion

            #region Tiamat
            if (!hasDoneActionThisTick &&
                Tiamat.MeetsCriteria()
                && Vi.IsAutoCanceling(enemies)
                && enemies.Where(a => a.IsInRange(Vi, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Vi, a, ItemId.Tiamat)).FirstOrDefault() != null))
                hasDoneActionThisTick = Tiamat.Cast();
            #endregion

            #region Youmuus
            if (!hasDoneActionThisTick &&
                Youmuus.MeetsCriteria()
                && Vi.CountEnemyHeroesInRangeWithPrediction((int)Vi.GetAutoAttackRange(), 0) >= 1)
                hasDoneActionThisTick = Youmuus.Cast();
            #endregion

            //all targeted spells that must be used on champions must be called after this
            enemies = enemies.Where(a => a.Type == GameObjectType.AIHeroClient).ToList();
            var target = enemies.OrderBy(a => a.Health).FirstOrDefault();

            #region Hextech Gunblade
            if (!hasDoneActionThisTick &&
                target != null
                && HextechGunblade.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Vi, 700)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Vi, a, ItemId.Hextech_Gunblade)).FirstOrDefault() != null))
                hasDoneActionThisTick = HextechGunblade.Cast(target);
            #endregion

            #region BOTRK
            if (!hasDoneActionThisTick &&
                target != null
                && BOTRK.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Vi, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Vi, a, ItemId.Blade_of_the_Ruined_King)).FirstOrDefault() != null))
                hasDoneActionThisTick = BOTRK.Cast(target);
            #endregion

            #region Bilgewater Cutlass
            if (!hasDoneActionThisTick &&
                target != null
                && BilgewaterCutlass.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Vi, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Vi, a, ItemId.Bilgewater_Cutlass)).FirstOrDefault() != null))
                hasDoneActionThisTick = BilgewaterCutlass.Cast(target);
            #endregion
        }

        public static void UseSmite(List<Obj_AI_Base> enemies, bool ks)
        {
            Spell.Targeted blueSmite = new Spell.Targeted(Vi.GetSpellSlotFromName("S5_SummonerSmitePlayerGanker"), 500, DamageType.True);
            Spell.Targeted redSmite = new Spell.Targeted(Vi.GetSpellSlotFromName("S5_SummonerSmiteDuel"), 500, DamageType.True);

            if (blueSmite.Slot != SpellSlot.Unknown && blueSmite.IsReady())
            {
                var target = enemies.Where(a => a.MeetsCriteria() && a.IsTargetable && a.IsInRange(Vi, blueSmite.Range) && (!ks || (a.Type == GameObjectType.AIHeroClient && a.Health <= 54 + 6 * Vi.Level) || (a.Type != GameObjectType.AIHeroClient && a.Health <= Calculations.Smite()))).FirstOrDefault();

                if(target != null)
                    blueSmite.Cast(target);
            }

            if (redSmite.Slot != SpellSlot.Unknown && redSmite.IsReady())
            {
                var target = enemies.Where(a => a.MeetsCriteria() && a.IsTargetable && a.IsInRange(Vi, redSmite.Range) && a.IsInRange(Vi, Vi.GetAutoAttackRange()) && (!ks || (a.Type == GameObjectType.AIHeroClient && a.Health <= 54 + 6 * Vi.Level) || (a.Type != GameObjectType.AIHeroClient && a.Health <= Calculations.Smite()))).FirstOrDefault();

                if (target != null)
                    redSmite.Cast(target);
            }
        }

        public static void UseIgnite(List<Obj_AI_Base> enemies, bool ks)
        {
            Spell.Targeted ignite = new Spell.Targeted(Vi.GetSpellSlotFromName("SummonerDot"), 600, DamageType.True);

            if (ignite.Slot == SpellSlot.Unknown || !ignite.IsReady())
                return;

            Obj_AI_Base unit = enemies.Where(a =>
                a.IsInRange(Vi, ignite.Range)
                && (!ks || Calculations.Ignite(a) >= a.Health)
                && a.MeetsCriteria()).FirstOrDefault();

            if (unit != null)
                hasDoneActionThisTick = ignite.Cast(unit);
        }

        public static void ChargeQ()
        {
            if (Program.Q.IsReady() && !hasDoneActionThisTick)
                hasDoneActionThisTick = Program.Q.StartCharging();
        }
        public static void CastQ(Vector3 pos)
        {
            if (Program.Q.IsReady() && !hasDoneActionThisTick && pos != Vector3.Zero)
                hasDoneActionThisTick = Program.Q.Cast(pos);
        }
        public static void CastE(List<Obj_AI_Base> enemies)
        {
            if (!Program.E.IsReady() || hasDoneActionThisTick || !Vi.IsAutoCanceling(enemies))
                return;

            int bestCount = 0;
            Obj_AI_Base bestEnemy = null;
            foreach (Obj_AI_Base enemy in EntityManager.Enemies.Where(a => a.MeetsCriteria() && a.IsInRange(Vi, Program.E.Range)).ToList())
            {
                Geometry.Polygon.Sector cone = new Geometry.Polygon.Sector(Vi.Position, enemy.Position, (float)(45 * Math.PI / 180), 600);

                List<Obj_AI_Base> enemiesHitByE = enemies.Where(a => cone.IsInside(a) && a != enemy).ToList();
                if(!enemiesHitByE.Contains(enemy))
                    enemiesHitByE.Add(enemy);
                if(bestCount < enemiesHitByE.Count())
                {
                    bestCount = enemiesHitByE.Count();
                    bestEnemy = enemy;
                }
            }   
            if (bestEnemy != null && bestCount > 0 && bestEnemy.IsInRange(Vi, Vi.GetAutoAttackRange()))
            {
                Orbwalker.ResetAutoAttack();
                hasDoneActionThisTick = Program.E.Cast();
                Orbwalker.ForcedTarget = bestEnemy;
            }
        }
        public static void CastR(List<Obj_AI_Base> enemies)
        {
            if (!Program.R.IsReady() || hasDoneActionThisTick)
                return;

            if (TargetSelector.SelectedTarget != null && TargetSelector.SelectedTarget.IsInRange(Vi, Program.R.Range))
                hasDoneActionThisTick = Program.R.Cast(TargetSelector.SelectedTarget);
            else
            {
                Obj_AI_Base enemy = enemies.Where(a => a.IsInRange(Vi, Program.R.Range)).OrderBy(a => a.Health).FirstOrDefault();
                if (enemy != null)
                    hasDoneActionThisTick = Program.R.Cast(enemy);
            }
        }
    }
}
