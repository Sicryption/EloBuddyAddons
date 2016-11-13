using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedJarvanIV
{
    class ModeHandler
    {
        public static AIHeroClient JarvanIV;
        public static bool hasDoneActionThisTick = false;

        public static void Combo()
        {
            Menu menu = MenuHandler.Combo;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            //eq
            Spell.Skillshot.BestPosition bestQPos = Program.Q.GetBestLinearCastPosition(enemies);
            Spell.Skillshot.BestPosition bestEPos = Program.E.GetBestCircularCastPosition(enemies);

            if (menu.GetCheckboxValue("Use EQ") && Program.Q.IsReady() && Program.E.IsReady() && bestQPos.HitNumber >= 1)
            {
                CastE(bestQPos.CastPosition);
                Core.DelayAction(new Action(delegate { CastQ(bestQPos.CastPosition); }), 100);
            }

            if (menu.GetCheckboxValue("Use Q") && JarvanIV.IsAutoCanceling(enemies) && bestQPos.HitNumber >= 1)
                CastQ(bestQPos.CastPosition);
            
            if (menu.GetCheckboxValue("Use W") && enemies.Where(a=>a.IsInRange(JarvanIV, Program.W.Range)).Count() >= 1)
                CastW();

            if (menu.GetCheckboxValue("Use E") && bestEPos.HitNumber >= 1)
                CastE(bestEPos.CastPosition);

            if (menu.GetCheckboxValue("Use R") && bestEPos.HitNumber >= 1)
                CastR(enemies, false);

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

            //eq
            Spell.Skillshot.BestPosition bestQPos = Program.Q.GetBestLinearCastPosition(enemies);
            Spell.Skillshot.BestPosition bestEPos = Program.E.GetBestCircularCastPosition(enemies);

            if (menu.GetCheckboxValue("Use EQ") && Program.Q.IsReady() && Program.E.IsReady() && bestQPos.HitNumber >= 1)
            {
                CastE(bestQPos.CastPosition);
                Core.DelayAction(new Action(delegate { CastQ(bestQPos.CastPosition); }), 100);
            }

            if (menu.GetCheckboxValue("Use Q") && JarvanIV.IsAutoCanceling(enemies) && bestQPos.HitNumber >= 1)
                CastQ(bestQPos.CastPosition);

            if (menu.GetCheckboxValue("Use W") && enemies.Where(a => a.IsInRange(JarvanIV, Program.W.Range)).Count() >= 1)
                CastW();

            if (menu.GetCheckboxValue("Use E") && bestEPos.HitNumber >= 1)
                CastE(bestEPos.CastPosition);
            
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, true);
        }
        
        public static void Killsteal()
        {
            Menu menu = MenuHandler.Killsteal;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            //eq
            Spell.Skillshot.BestPosition bestQPos = Program.Q.GetBestLinearCastPosition(enemies.Where(a=>a.Health <= Calculations.Q(a)));
            Spell.Skillshot.BestPosition bestEPos = Program.E.GetBestCircularCastPosition(enemies.Where(a=>a.Health <= Calculations.E(a)));

            if (menu.GetCheckboxValue("Use EQ") && Program.Q.IsReady() && Program.E.IsReady() && bestQPos.HitNumber >= 1)
            {
                CastE(bestQPos.CastPosition);
                Core.DelayAction(new Action(delegate { CastQ(bestQPos.CastPosition); }), 100);
            }

            if (menu.GetCheckboxValue("Use Q") && JarvanIV.IsAutoCanceling(enemies) && bestQPos.HitNumber >= 1)
                CastQ(bestQPos.CastPosition);
            
            if (menu.GetCheckboxValue("Use E") && bestEPos.HitNumber >= 1)
                CastE(bestEPos.CastPosition);

            if (menu.GetCheckboxValue("Use R") && bestEPos.HitNumber >= 1)
                CastR(enemies, true);

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

            //eq
            if (menu.GetCheckboxValue("Use EQ") && Program.Q.IsReady() && Program.E.IsReady())
            {
                Vector3 pos = JarvanIV.Position.Extend(Game.CursorPos, Program.E.Range).To3D((int)JarvanIV.Position.Z);

                CastE(pos);
                Core.DelayAction(new Action(delegate { CastQ(pos); }), 100);
            }
        }

        public static void Harass()
        {
            Menu menu = MenuHandler.Harass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            //eq
            Spell.Skillshot.BestPosition bestQPos = Program.Q.GetBestLinearCastPosition(enemies);
            Spell.Skillshot.BestPosition bestEPos = Program.E.GetBestCircularCastPosition(enemies);

            if (menu.GetCheckboxValue("Use EQ") && Program.Q.IsReady() && Program.E.IsReady() && bestQPos.HitNumber >= 1)
            {
                CastE(bestQPos.CastPosition);
                Core.DelayAction(new Action(delegate { CastQ(bestQPos.CastPosition); }), 100);
            }

            if (menu.GetCheckboxValue("Use Q") && JarvanIV.IsAutoCanceling(enemies) && bestQPos.HitNumber >= 1)
                CastQ(bestQPos.CastPosition);

            if (menu.GetCheckboxValue("Use W") && enemies.Where(a => a.IsInRange(JarvanIV, Program.W.Range)).Count() >= 1)
                CastW();

            if (menu.GetCheckboxValue("Use E") && bestEPos.HitNumber >= 1)
                CastE(bestEPos.CastPosition);

            if (menu.GetCheckboxValue("Use R") && bestEPos.HitNumber >= 1)
                CastR(enemies, false);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, false);
        }
        
        public static void LaneClear()
        {
            Menu menu = MenuHandler.LaneClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.Where(a => a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            //eq
            Spell.Skillshot.BestPosition bestQPos = Program.Q.GetBestLinearCastPosition(enemies);
            Spell.Skillshot.BestPosition bestEPos = Program.E.GetBestCircularCastPosition(enemies);

            if (menu.GetCheckboxValue("Use EQ") && Program.Q.IsReady() && Program.E.IsReady() && bestQPos.HitNumber >= 1)
            {
                CastE(bestQPos.CastPosition);
                Core.DelayAction(new Action(delegate { CastQ(bestQPos.CastPosition); }), 100);
            }

            if (menu.GetCheckboxValue("Use Q") && JarvanIV.IsAutoCanceling(enemies) && bestQPos.HitNumber >= 1)
                CastQ(bestQPos.CastPosition);
            
            if (menu.GetCheckboxValue("Use E") && bestEPos.HitNumber >= 2)
                CastE(bestEPos.CastPosition);
            
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }
        
        public static void LastHit()
        {
            Menu menu = MenuHandler.LastHit;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.Where(a => a.MeetsCriteria()).ToList().ToObj_AI_BaseList();

            //eq
            Spell.Skillshot.BestPosition bestQPos = Program.Q.GetBestLinearCastPosition(enemies.Where(a=>a.Health <= Calculations.Q(a)));
            Spell.Skillshot.BestPosition bestEPos = Program.E.GetBestCircularCastPosition(enemies.Where(a=>a.Health <= Calculations.E(a)));

            if (menu.GetCheckboxValue("Use EQ") && Program.Q.IsReady() && Program.E.IsReady() && bestQPos.HitNumber >= 1)
            {
                CastE(bestQPos.CastPosition);
                Core.DelayAction(new Action(delegate { CastQ(bestQPos.CastPosition); }), 100);
            }

            if (menu.GetCheckboxValue("Use Q") && JarvanIV.IsAutoCanceling(enemies) && bestQPos.HitNumber >= 1)
                CastQ(bestQPos.CastPosition);
            
            if (menu.GetCheckboxValue("Use E") && bestEPos.HitNumber >= 1)
                CastE(bestEPos.CastPosition);
            
            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);
        }

        public static void UseItems(List<Obj_AI_Base> enemies, bool ks)
        {
            #region Item Initialization
            InventorySlot QSS = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Quick Silver Sash")) ? JarvanIV.GetItem(ItemId.Quicksilver_Sash) : null,
                MercurialsScimitar = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Mercurials Scimitar")) ? JarvanIV.GetItem(ItemId.Mercurial_Scimitar) : null,
                RavenousHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Ravenous Hydra")) ? JarvanIV.GetItem(ItemId.Ravenous_Hydra) : null,
                TitanicHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Titanic Hydra")) ? JarvanIV.GetItem(ItemId.Titanic_Hydra) : null,
                Tiamat = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Tiamat")) ? JarvanIV.GetItem(ItemId.Tiamat) : null,
                Youmuus = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Youmuus")) ? JarvanIV.GetItem(ItemId.Youmuus_Ghostblade) : null,
                BOTRK = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Blade of the Ruined King")) ? JarvanIV.GetItem(ItemId.Blade_of_the_Ruined_King) : null,
                BilgewaterCutlass = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Bilgewater Cutlass")) ? JarvanIV.GetItem(ItemId.Bilgewater_Cutlass) : null,
                HextechGunblade = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Hextech Gunblade")) ? JarvanIV.GetItem(ItemId.Hextech_Gunblade) : null;
            #endregion

            #region QSS
            if (!hasDoneActionThisTick &&
                QSS.MeetsCriteria() &&
                JarvanIV.CanCancleCC())
                hasDoneActionThisTick = QSS.Cast();
            #endregion

            #region Mercurials Scimitar
            if (!hasDoneActionThisTick &&
                MercurialsScimitar.MeetsCriteria() &&
                JarvanIV.CanCancleCC())
                hasDoneActionThisTick = MercurialsScimitar.Cast();
            #endregion

            #region Ravenous Hydra
            if (!hasDoneActionThisTick &&
                RavenousHydra.MeetsCriteria()
                && JarvanIV.IsAutoCanceling(enemies)
                && enemies.Where(a => a.IsInRange(JarvanIV, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(JarvanIV, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null))
                hasDoneActionThisTick = RavenousHydra.Cast();
            #endregion

            #region Titanic Hydra
            if (!hasDoneActionThisTick &&
                TitanicHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(JarvanIV, JarvanIV.GetAutoAttackRange())).FirstOrDefault() != null
                && JarvanIV.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(JarvanIV, a, ItemId.Titanic_Hydra)).FirstOrDefault() != null))
                hasDoneActionThisTick = TitanicHydra.Cast();
            #endregion

            #region Tiamat
            if (!hasDoneActionThisTick &&
                Tiamat.MeetsCriteria()
                && JarvanIV.IsAutoCanceling(enemies)
                && enemies.Where(a => a.IsInRange(JarvanIV, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(JarvanIV, a, ItemId.Tiamat)).FirstOrDefault() != null))
                hasDoneActionThisTick = Tiamat.Cast();
            #endregion

            #region Youmuus
            if (!hasDoneActionThisTick &&
                Youmuus.MeetsCriteria()
                && JarvanIV.CountEnemyHeroesInRangeWithPrediction((int)JarvanIV.GetAutoAttackRange(), 0) >= 1)
                hasDoneActionThisTick = Youmuus.Cast();
            #endregion

            //all targeted spells that must be used on champions must be called after this
            enemies = enemies.Where(a => a.Type == GameObjectType.AIHeroClient).ToList();
            var target = enemies.OrderBy(a => a.Health).FirstOrDefault();

            #region Hextech Gunblade
            if (!hasDoneActionThisTick &&
                target != null
                && HextechGunblade.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(JarvanIV, 700)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(JarvanIV, a, ItemId.Hextech_Gunblade)).FirstOrDefault() != null))
                hasDoneActionThisTick = HextechGunblade.Cast(target);
            #endregion

            #region BOTRK
            if (!hasDoneActionThisTick &&
                target != null
                && BOTRK.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(JarvanIV, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(JarvanIV, a, ItemId.Blade_of_the_Ruined_King)).FirstOrDefault() != null))
                hasDoneActionThisTick = BOTRK.Cast(target);
            #endregion

            #region Bilgewater Cutlass
            if (!hasDoneActionThisTick &&
                target != null
                && BilgewaterCutlass.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(JarvanIV, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(JarvanIV, a, ItemId.Bilgewater_Cutlass)).FirstOrDefault() != null))
                hasDoneActionThisTick = BilgewaterCutlass.Cast(target);
            #endregion
        }

        public static void UseSmite(List<Obj_AI_Base> enemies, bool ks)
        {
            Spell.Targeted blueSmite = new Spell.Targeted(JarvanIV.GetSpellSlotFromName("S5_SummonerSmitePlayerGanker"), 500, DamageType.True);
            Spell.Targeted redSmite = new Spell.Targeted(JarvanIV.GetSpellSlotFromName("S5_SummonerSmiteDuel"), 500, DamageType.True);

            if (blueSmite.Slot != SpellSlot.Unknown && blueSmite.IsReady())
            {
                var target = enemies.Where(a => a.MeetsCriteria() && a.IsTargetable && a.IsInRange(JarvanIV, blueSmite.Range) && (!ks || (a.Type == GameObjectType.AIHeroClient && a.Health <= 54 + 6 * JarvanIV.Level) || (a.Type != GameObjectType.AIHeroClient && a.Health <= Calculations.Smite()))).FirstOrDefault();

                if(target != null)
                    blueSmite.Cast(target);
            }

            if (redSmite.Slot != SpellSlot.Unknown && redSmite.IsReady())
            {
                var target = enemies.Where(a => a.MeetsCriteria() && a.IsTargetable && a.IsInRange(JarvanIV, redSmite.Range) && a.IsInRange(JarvanIV, JarvanIV.GetAutoAttackRange()) && (!ks || (a.Type == GameObjectType.AIHeroClient && a.Health <= 54 + 6 * JarvanIV.Level) || (a.Type != GameObjectType.AIHeroClient && a.Health <= Calculations.Smite()))).FirstOrDefault();

                if (target != null)
                    redSmite.Cast(target);
            }
        }

        public static void UseIgnite(List<Obj_AI_Base> enemies, bool ks)
        {
            Spell.Targeted ignite = new Spell.Targeted(JarvanIV.GetSpellSlotFromName("SummonerDot"), 600, DamageType.True);

            if (ignite.Slot == SpellSlot.Unknown || !ignite.IsReady())
                return;

            Obj_AI_Base unit = enemies.Where(a =>
                a.IsInRange(JarvanIV, ignite.Range)
                && (!ks || Calculations.Ignite(a) >= a.Health)
                && a.MeetsCriteria()).FirstOrDefault();

            if (unit != null)
                hasDoneActionThisTick = ignite.Cast(unit);
        }
        
        public static void CastQ(Vector3 pos)
        {
            if (Program.Q.IsReady() && !hasDoneActionThisTick && pos != Vector3.Zero)
                hasDoneActionThisTick = Program.Q.Cast(pos);
        }
        public static void CastW()
        {
            if (Program.W.IsReady() && !hasDoneActionThisTick)
                hasDoneActionThisTick = Program.W.Cast();
        }
        public static void CastE(List<Obj_AI_Base> enemies)
        {
            if (!Program.E.IsReady() || hasDoneActionThisTick)
                return;

            Spell.Skillshot.BestPosition bestPos = Program.E.GetBestCircularCastPosition(enemies);

            if (bestPos.HitNumber >= 1)
                hasDoneActionThisTick = Program.E.Cast(bestPos.CastPosition);
        }
        public static void CastE(Vector3 pos)
        {
            if (!Program.E.IsReady() || hasDoneActionThisTick || !JarvanIV.IsInRange(pos, Program.E.Range))
                return;

            hasDoneActionThisTick = Program.E.Cast(pos);
        }
        public static void CastR(List<Obj_AI_Base> enemies, bool ks)
        {
            if (!Program.R.IsReady() || hasDoneActionThisTick)
                return;

            Obj_AI_Base bestRTarget = enemies.OrderBy(a => a.CountEnemyHeroesInRangeWithPrediction(325, 250) >= 1 && (!ks || a.Health <= Calculations.R(a))).FirstOrDefault();

            if (bestRTarget.CountEnemyHeroesInRangeWithPrediction(325) >= 1)
                hasDoneActionThisTick = Program.R.Cast(bestRTarget);
        }
    }
}
