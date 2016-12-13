using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedRengar
{
    class ModeHandler
    {
        public static AIHeroClient Rengar;
        public static bool hasDoneActionThisTick = false;

        public static void Combo()
        {
            Menu menu = MenuHandler.Combo;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (TargetSelector.SelectedTarget != null && TargetSelector.SelectedTarget.MeetsCriteria() && TargetSelector.SelectedTarget.IsInRange(Rengar, 1200))
                enemies = new List<Obj_AI_Base>() { TargetSelector.SelectedTarget };


            int QHitNumber = 0;
            Vector3 QPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies, Rengar.Position, out QHitNumber);
            int EHitNumber = 0;
            Vector3 EPos = Program.E.GetBestLinearPredictionPos(enemies, Rengar.Position, out EHitNumber);

            if (QPos != Vector3.Zero && QHitNumber >= 1 && Rengar.IsAutoCanceling(enemies))
            {
                if (menu.GetCheckboxValue("Use Q") && Rengar.Ferocity() < 4)
                    CastQ(QPos);
                if (menu.GetCheckboxValue("Use Empowered Q") && Rengar.Ferocity() == 4)
                    CastQ(QPos);
            }

            if (menu.GetCheckboxValue("Use W") && Rengar.Ferocity() < 4)
            {
                if (menu.GetCheckboxValue("Use W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();
                
                if (menu.GetCheckboxValue("Use W for fourth ferocity stack") && Rengar.Ferocity() == 3 && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use Empowered W") && Rengar.Ferocity() == 4)
            {
                if (menu.GetCheckboxValue("Use Empowered W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (menu.GetCheckboxValue("Use Empowered W to Stop CC")
                    && Rengar.CanCancleCC())
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use Empowered W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use E") && Rengar.Ferocity() < 4 && Rengar.IsAutoCanceling(enemies) && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered E") && Rengar.IsAutoCanceling(enemies) && Rengar.Ferocity() == 4 && EHitNumber >= 1)
                CastE(EPos);

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
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList();

            int QHitNumber = 0;
            Vector3 QPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies, Rengar.Position, out QHitNumber);
            int EHitNumber = 0;
            Vector3 EPos = Program.E.GetBestLinearPredictionPos(enemies, Rengar.Position, out EHitNumber);

            if (QPos != Vector3.Zero && QHitNumber >= 1 && Rengar.IsAutoCanceling(enemies))
            {
                if (menu.GetCheckboxValue("Use Q") && Rengar.Ferocity() < 4)
                    CastQ(QPos);
                if (menu.GetCheckboxValue("Use Empowered Q") && Rengar.Ferocity() == 4)
                    CastQ(QPos);
            }

            if (menu.GetCheckboxValue("Use W") && Rengar.Ferocity() < 4)
            {
                if (menu.GetCheckboxValue("Use W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (menu.GetCheckboxValue("Use W for fourth ferocity stack") && Rengar.Ferocity() == 3 && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use Empowered W") && Rengar.Ferocity() == 4)
            {
                if (menu.GetCheckboxValue("Use Empowered W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use Empowered W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use E") && Rengar.Ferocity() < 4 && Rengar.IsAutoCanceling(enemies) && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered E") && Rengar.Ferocity() == 4 && Rengar.IsAutoCanceling(enemies) && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, true, menu.GetCheckboxValue("Use Smite for HP"));
        }
        
        public static void Killsteal()
        {
            Menu menu = MenuHandler.Killsteal;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            int QHitNumber = 0;
            Vector3 QPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies.Where(a => a.Health <= Calculations.Q(a)).ToList(), Rengar.Position, out QHitNumber);
            int EmpQHitNumber = 0;
            Vector3 EmpQPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies.Where(a => a.Health <= Calculations.EmpQ(a)).ToList(), Rengar.Position, out QHitNumber);
            int EHitNumber = 0;
            Vector3 EPos = Program.E.GetBestLinearPredictionPos(enemies.Where(a => a.Health <= Calculations.E(a)).ToList(), Rengar.Position, out EHitNumber);
            int EmpEHitNumber = 0;
            Vector3 EmpEPos = Program.E.GetBestLinearPredictionPos(enemies.Where(a => a.Health <= Calculations.EmpE(a)).ToList(), Rengar.Position, out EmpEHitNumber);

            if (menu.GetCheckboxValue("Use Q") && Rengar.Ferocity() < 4 && QHitNumber >= 1)
                CastQ(QPos);
            if (menu.GetCheckboxValue("Use Empowered Q") && Rengar.Ferocity() == 4 && EmpQHitNumber >= 1)
                CastQ(EmpQPos);

            if (menu.GetCheckboxValue("Use W") && Rengar.Ferocity() < 4)
                if (enemies.Where(a => a.IsInRange(Rengar, Program.W.Range) && a.Health <= Calculations.EmpW(a)).Count() >= 1)
                    CastW();

            if (menu.GetCheckboxValue("Use Empowered W") && Rengar.Ferocity() == 4)
                if (enemies.Where(a => a.IsInRange(Rengar, Program.W.Range) && a.Health <= Calculations.EmpW(a)).Count() >= 1)
                    CastW();

            if (menu.GetCheckboxValue("Use E") && Rengar.Ferocity() < 4 && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered E") && Rengar.Ferocity() == 4 && EmpEHitNumber >= 1)
                CastE(EmpEPos);

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
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            int EHitNumber = 0;
            Vector3 EPos = Program.E.GetBestLinearPredictionPos(enemies.Where(a => a.Health <= Calculations.E(a)).ToList(), Rengar.Position, out EHitNumber);
            int EmpEHitNumber = 0;
            Vector3 EmpEPos = Program.E.GetBestLinearPredictionPos(enemies.Where(a => a.Health <= Calculations.EmpE(a)).ToList(), Rengar.Position, out EmpEHitNumber);
            
            if (menu.GetCheckboxValue("Use E") && Rengar.Ferocity() < 4 && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered E") && Rengar.Ferocity() == 4 && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered W") && Rengar.Ferocity() == 4)
                CastW();

            if (menu.GetCheckboxValue("Use Empowered W to Stop CC") && Rengar.Ferocity() == 4 && Rengar.CanCancleCC())
                CastW();

            if (menu.GetCheckboxValue("Jump from Brush") && Rengar.IsAbleToJump() && Orbwalker.CanAutoAttack)
            {
                Obj_AI_Base closestEnemy =
                    ObjectManager.Get<Obj_AI_Base>().Where(a => a.MeetsCriteria() && a.IsEnemy && a.IsTargetable && a.IsInRange(Rengar, 825))
                    .OrderBy(a => a.Position.WorldToScreen().Distance(Game.CursorPos2D)).FirstOrDefault();

                if (closestEnemy != null && closestEnemy.Distance(Rengar) > 225)
                    Player.IssueOrder(GameObjectOrder.AutoAttack, closestEnemy);
            }
        }

        public static void Harass()
        {
            Menu menu = MenuHandler.Harass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            int QHitNumber = 0;
            Vector3 QPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies, Rengar.Position, out QHitNumber);
            int EHitNumber = 0;
            Vector3 EPos = Program.E.GetBestLinearPredictionPos(enemies, Rengar.Position, out EHitNumber);

            if (QPos != Vector3.Zero && QHitNumber >= 1 && Rengar.IsAutoCanceling(enemies))
            {
                if (menu.GetCheckboxValue("Use Q") && Rengar.Ferocity() < 4)
                    CastQ(QPos);
                if (menu.GetCheckboxValue("Use Empowered Q") && Rengar.Ferocity() == 4)
                    CastQ(QPos);
            }

            if (menu.GetCheckboxValue("Use W") && Rengar.Ferocity() < 4)
            {
                if (menu.GetCheckboxValue("Use W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (menu.GetCheckboxValue("Use W for fourth ferocity stack") && Rengar.Ferocity() == 3 && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use Empowered W") && Rengar.Ferocity() == 4)
            {
                if (menu.GetCheckboxValue("Use Empowered W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (menu.GetCheckboxValue("Use Empowered W to Stop CC")
                    && Rengar.CanCancleCC())
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use Empowered W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use E") && Rengar.Ferocity() < 4 && Rengar.IsAutoCanceling(enemies) && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered E") && Rengar.Ferocity() == 4 && Rengar.IsAutoCanceling(enemies) && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Smite"))
                UseSmite(enemies, false);
        }
        
        public static void LaneClear()
        {
            Menu menu = MenuHandler.LaneClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

            int QHitNumber = 0;
            Vector3 QPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies, Rengar.Position, out QHitNumber);
            int EHitNumber = 0;
            Vector3 EPos = Program.E.GetBestLinearPredictionPos(enemies, Rengar.Position, out EHitNumber);

            if (QPos != Vector3.Zero && QHitNumber >= 1)
            {
                if (menu.GetCheckboxValue("Use Q") && Rengar.Ferocity() < 4)
                    CastQ(QPos);
                if (menu.GetCheckboxValue("Use Empowered Q") && Rengar.Ferocity() == 4 && !menu.GetCheckboxValue("Save Ferocity"))
                    CastQ(QPos);
            }

            if (menu.GetCheckboxValue("Use W") && Rengar.Ferocity() < 4)
            {
                if (menu.GetCheckboxValue("Use W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (menu.GetCheckboxValue("Use W for fourth ferocity stack") && Rengar.Ferocity() == 3 && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use Empowered W") && Rengar.Ferocity() == 4 && !menu.GetCheckboxValue("Save Ferocity"))
            {
                if (menu.GetCheckboxValue("Use Empowered W for damage") && enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)).Count() >= 1)
                    CastW();

                if (Rengar.GreyShieldPercent() >= 1 && menu.GetSliderValue("Use Empowered W at % black health") <= Rengar.GreyShieldPercent())
                    CastW();
            }

            if (menu.GetCheckboxValue("Use E") && Rengar.Ferocity() < 4 && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered E") && Rengar.Ferocity() == 4 && EHitNumber >= 1 && Rengar.IsAutoCanceling(enemies) && !menu.GetCheckboxValue("Save Ferocity"))
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }
        
        public static void LastHit()
        {
            Menu menu = MenuHandler.LastHit;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

            int QHitNumber = 0;
            Vector3 QPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies.Where(a => a.Health <= Calculations.Q(a)).ToList(), Rengar.Position, out QHitNumber);
            int EmpQHitNumber = 0;
            Vector3 EmpQPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, enemies.Where(a=>a.Health <= Calculations.EmpQ(a)).ToList(), Rengar.Position, out QHitNumber);
            int EHitNumber = 0;
            Vector3 EPos = Program.E.GetBestLinearPredictionPos(enemies.Where(a => a.Health <= Calculations.E(a)).ToList(), Rengar.Position, out EHitNumber);
            int EmpEHitNumber = 0;
            Vector3 EmpEPos = Program.E.GetBestLinearPredictionPos(enemies.Where(a => a.Health <= Calculations.EmpE(a)).ToList(), Rengar.Position, out EmpEHitNumber);
            
            if (menu.GetCheckboxValue("Use Q") && Rengar.Ferocity() < 4 && QHitNumber >= 1 && Rengar.IsAutoCanceling(enemies))
                CastQ(QPos);
            if (menu.GetCheckboxValue("Use Empowered Q") && Rengar.Ferocity() == 4 && Rengar.IsAutoCanceling(enemies) && EmpQHitNumber >= 1 && !menu.GetCheckboxValue("Save Ferocity"))
                CastQ(EmpQPos);

            if (menu.GetCheckboxValue("Use W") && Rengar.Ferocity() < 4)
                if (enemies.Where(a => a.IsInRange(Rengar, Program.W.Range) 
                && menu.GetSliderValue("Minions to use W") != 0
                && a.Health <= Calculations.EmpW(a)).Count() >= menu.GetSliderValue("Minions to use W"))
                    CastW();

            if (menu.GetCheckboxValue("Use Empowered W") && Rengar.Ferocity() == 4 && !menu.GetCheckboxValue("Save Ferocity"))
                if (enemies.Where(a => a.IsInRange(Rengar, Program.W.Range)
                && menu.GetSliderValue("Minions to use Empowered W") != 0
                && a.Health <= Calculations.EmpW(a)).Count() >= menu.GetSliderValue("Minions to use Empowered W"))
                    CastW();

            if (menu.GetCheckboxValue("Use E") && Rengar.Ferocity() < 4 && EHitNumber >= 1)
                CastE(EPos);

            if (menu.GetCheckboxValue("Use Empowered E") && Rengar.Ferocity() == 4 && EmpEHitNumber >= 1 && !menu.GetCheckboxValue("Save Ferocity"))
                CastE(EmpEPos);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);
        }

        public static void UseItems(List<Obj_AI_Base> enemies, bool ks)
        {
            #region Item Initialization
            InventorySlot QSS = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Quick Silver Sash")) ? Rengar.GetItem(ItemId.Quicksilver_Sash) : null,
                MercurialsScimitar = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Mercurials Scimitar")) ? Rengar.GetItem(ItemId.Mercurial_Scimitar) : null,
                RavenousHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Ravenous Hydra")) ? Rengar.GetItem(ItemId.Ravenous_Hydra) : null,
                TitanicHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Titanic Hydra")) ? Rengar.GetItem(ItemId.Titanic_Hydra) : null,
                Tiamat = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Tiamat")) ? Rengar.GetItem(ItemId.Tiamat) : null,
                Youmuus = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Youmuus")) ? Rengar.GetItem(ItemId.Youmuus_Ghostblade) : null,
                BOTRK = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Blade of the Ruined King")) ? Rengar.GetItem(ItemId.Blade_of_the_Ruined_King) : null,
                BilgewaterCutlass = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Bilgewater Cutlass")) ? Rengar.GetItem(ItemId.Bilgewater_Cutlass) : null,
                HextechGunblade = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Hextech Gunblade")) ? Rengar.GetItem(ItemId.Hextech_Gunblade) : null;
            #endregion

            #region QSS
            if (!hasDoneActionThisTick &&
                QSS.MeetsCriteria() &&
                (Rengar.HasBuffOfType(BuffType.Blind)
                || Rengar.HasBuffOfType(BuffType.Charm)
                || Rengar.HasBuffOfType(BuffType.Fear)
                || Rengar.HasBuffOfType(BuffType.Knockback)
                || Rengar.HasBuffOfType(BuffType.Silence)
                || Rengar.HasBuffOfType(BuffType.Snare)
                || Rengar.HasBuffOfType(BuffType.Stun)
                || Rengar.HasBuffOfType(BuffType.Taunt))
                //not being knocked back by dragon
                && !Rengar.HasBuff("moveawaycollision")
                //not standing on raka silence
                && !Rengar.HasBuff("sorakaepacify"))
                hasDoneActionThisTick = QSS.Cast();
            #endregion

            #region Mercurials Scimitar
            if (!hasDoneActionThisTick &&
                MercurialsScimitar.MeetsCriteria() &&
                (Rengar.HasBuffOfType(BuffType.Blind)
                || Rengar.HasBuffOfType(BuffType.Charm)
                || Rengar.HasBuffOfType(BuffType.Fear)
                || Rengar.HasBuffOfType(BuffType.Knockback)
                || Rengar.HasBuffOfType(BuffType.Silence)
                || Rengar.HasBuffOfType(BuffType.Snare)
                || Rengar.HasBuffOfType(BuffType.Stun)
                || Rengar.HasBuffOfType(BuffType.Taunt))
                //not being knocked back by dragon
                && !Rengar.HasBuff("moveawaycollision")
                //not standing on raka silence
                && !Rengar.HasBuff("sorakaepacify"))
                hasDoneActionThisTick = MercurialsScimitar.Cast();
            #endregion

            #region Ravenous Hydra
            if (!hasDoneActionThisTick &&
                RavenousHydra.MeetsCriteria()
                && Rengar.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Rengar, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null)
                && (
                enemies.Where(a => a.Type == GameObjectType.AIHeroClient && a.IsInRange(Rengar, 400)).Count() >= MenuHandler.Items.GetSliderValue("Champions to use Tiamat/Ravenous Hydra on")
                || enemies.Where(a => a.Type != GameObjectType.AIHeroClient && a.IsInRange(Rengar, 400)).Count() >= MenuHandler.Items.GetSliderValue("Minions to use Tiamat/Ravenous Hydra on")))
                hasDoneActionThisTick = RavenousHydra.Cast();
            #endregion

            #region Titanic Hydra
            if (!hasDoneActionThisTick &&
                TitanicHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Rengar, Rengar.GetAutoAttackRange())).FirstOrDefault() != null
                && Rengar.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Rengar, a, ItemId.Titanic_Hydra)).FirstOrDefault() != null))
                hasDoneActionThisTick = TitanicHydra.Cast();
            #endregion

            #region Tiamat
            if (!hasDoneActionThisTick &&
                Tiamat.MeetsCriteria()
                && Rengar.IsAutoCanceling(enemies)
                && enemies.Where(a => a.IsInRange(Rengar, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Rengar, a, ItemId.Tiamat)).FirstOrDefault() != null)
                && (
                enemies.Where(a => a.Type == GameObjectType.AIHeroClient && a.IsInRange(Rengar, 400)).Count() >= MenuHandler.Items.GetSliderValue("Champions to use Tiamat/Ravenous Hydra on")
                || enemies.Where(a => a.Type != GameObjectType.AIHeroClient && a.IsInRange(Rengar, 400)).Count() >= MenuHandler.Items.GetSliderValue("Minions to use Tiamat/Ravenous Hydra on")))
                hasDoneActionThisTick = Tiamat.Cast();
            #endregion

            #region Youmuus
            if (!hasDoneActionThisTick &&
                Youmuus.MeetsCriteria()
                && Rengar.CountEnemyHeroesInRangeWithPrediction((int)Rengar.GetAutoAttackRange(), 0) >= 1)
                hasDoneActionThisTick = Youmuus.Cast();
            #endregion

            //all targeted spells that must be used on champions must be called after this
            enemies = enemies.Where(a => a.Type == GameObjectType.AIHeroClient).ToList();
            var target = enemies.OrderBy(a => a.Health).FirstOrDefault();

            #region Hextech Gunblade
            if (!hasDoneActionThisTick &&
                target != null
                && HextechGunblade.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Rengar, 700)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Rengar, a, ItemId.Hextech_Gunblade)).FirstOrDefault() != null))
                hasDoneActionThisTick = HextechGunblade.Cast(target);
            #endregion

            #region BOTRK
            if (!hasDoneActionThisTick &&
                target != null
                && BOTRK.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Rengar, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Rengar, a, ItemId.Blade_of_the_Ruined_King)).FirstOrDefault() != null))
                hasDoneActionThisTick = BOTRK.Cast(target);
            #endregion

            #region Bilgewater Cutlass
            if (!hasDoneActionThisTick &&
                target != null
                && BilgewaterCutlass.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Rengar, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Rengar, a, ItemId.Bilgewater_Cutlass)).FirstOrDefault() != null))
                hasDoneActionThisTick = BilgewaterCutlass.Cast(target);
            #endregion
        }

        public static void UseSmite(List<Obj_AI_Base> enemies, bool ks, bool forHP = false)
        {
            enemies = enemies.Where(a => a.MeetsCriteria() && a.IsTargetable && a.IsInRange(Rengar, 500) 
            && !a.BaseSkinName.ContainsAny(false, "mini", "sru_crab")).ToList();
            
            enemies = enemies.Where(a => 
                //if it is for hp then the entity must not be dragon/baron/herald/vilemaw
                (forHP && !a.BaseSkinName.ContainsAny(false, "Baron", "Dragon", "Herald", "Spider") && Rengar.MissingHealth() >= Calculations.SmiteHeal())
                //OR if it is for ks
                || (ks && 
                //ks on champion
                (a.Type == GameObjectType.AIHeroClient
                //or ks on minion
                || a.Health <= Calculations.Smite(a, "regular")
                ))).ToList();
            
            Spell.Targeted blueSmite = new Spell.Targeted(Rengar.GetSpellSlotFromName("S5_SummonerSmitePlayerGanker"), 500, DamageType.True);
            Spell.Targeted redSmite = new Spell.Targeted(Rengar.GetSpellSlotFromName("S5_SummonerSmiteDuel"), 500, DamageType.True);
            Spell.Targeted Smite = new Spell.Targeted(Rengar.GetSpellSlotFromName("SummonerSmite"), 500, DamageType.True);
            
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
                var target = enemies.Where(a => a.IsInRange(Rengar, Rengar.GetAutoAttackRange()) 
                //champions
                && ((ks && a.Type == GameObjectType.AIHeroClient && a.Health <= Calculations.Smite(a, "red")) || 
                //minions
                a.Type != GameObjectType.AIHeroClient)).FirstOrDefault();

                if (target != null)
                {
                    hasDoneActionThisTick = redSmite.Cast(target);
                    
                    //if it did smite them, attack them
                    if(hasDoneActionThisTick)
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

        public static void UseIgnite(List<Obj_AI_Base> enemies, bool ks)
        {
            Spell.Targeted ignite = new Spell.Targeted(Rengar.GetSpellSlotFromName("SummonerDot"), 600, DamageType.True);

            if (ignite.Slot == SpellSlot.Unknown || !ignite.IsReady())
                return;

            Obj_AI_Base unit = enemies.Where(a =>
                a.IsInRange(Rengar, ignite.Range)
                && (!ks || Calculations.Ignite(a) >= a.Health)
                && a.MeetsCriteria()).FirstOrDefault();

            if (unit != null)
                hasDoneActionThisTick = ignite.Cast(unit);
        }

        public static void CastQ(Vector3 pos)
        {
            if (Program.Q.IsReady() && !hasDoneActionThisTick && pos != Vector3.Zero && !Rengar.HasBuff("RengarR"))
                hasDoneActionThisTick = Program.Q.Cast(pos);
        }
        public static void CastW()
        {
            if (Program.W.IsReady() && !hasDoneActionThisTick && !Rengar.HasBuff("RengarR"))
                hasDoneActionThisTick = Program.W.Cast();
        }
        public static void CastE(Vector3 pos)
        {
            if (Program.E.IsReady() && !hasDoneActionThisTick && pos != Vector3.Zero && !Rengar.HasBuff("RengarR"))
                hasDoneActionThisTick = Program.E.Cast(pos);
        }
    }
}
