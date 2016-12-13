using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedCamille
{
    class ModeManager
    {
        public static AIHeroClient Camille => Player.Instance;
        public static bool hasDoneActionThisTick = false;
        public static float LastAutoTime = 0,
            LastECheckTime = 0;

        //camileqbuffname
        public static void Combo()
        {
            Menu menu = MenuHandler.Combo;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            
            if (menu.GetCheckboxValue("Use Q1") && Program.Q.Name == "CamilleQ")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use Q2") && Program.Q.Name == "CamilleQ2")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false);

            //if (menu.GetCheckboxValue("Force Follow in W Range"))
                //WFollow();
            
            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleE")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use E2") && Program.E.Name == "CamilleEDash2")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use R"))
                CastR(enemies);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);

            if (menu.GetCheckboxValue("Use Ignite"))
                UseIgnite(enemies, true);
        }

        public static void Harass()
        {
            Menu menu = MenuHandler.Harass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q1") && Program.Q.Name == "CamilleQ")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use Q2") && Program.Q.Name == "CamilleQ2")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false);

            //if (menu.GetCheckboxValue("Force Follow in W Range"))
                //WFollow();

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleE")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleEDash2")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use R"))
                CastR(enemies);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }

        public static void JungleClear()
        {
            Menu menu = MenuHandler.JungleClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q1") && Program.Q.Name == "CamilleQ")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use Q2") && Program.Q.Name == "CamilleQ2")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false);

            //if (menu.GetCheckboxValue("Force Follow in W Range"))
                //WFollow();

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleE")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleEDash2")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }

        public static void Killsteal()
        {
            Menu menu = MenuHandler.Killsteal;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q1") && Program.Q.Name == "CamilleQ")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use Q2") && Program.Q.Name == "CamilleQ2")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, true);

            //if (menu.GetCheckboxValue("Force Follow in W Range"))
                //WFollow();

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleE")
                CastE(enemies, true);

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleEDash2")
                CastE(enemies, true);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);

            if (menu.GetCheckboxValue("Use Ignite"))
                UseIgnite(enemies, true);
        }

        //add logic
        public static void Flee()
        {
            Menu menu = MenuHandler.Flee;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            
            if (Program.E.IsReady() && Program.E.Name == "CamilleE" && Game.Time - LastECheckTime > 0.25f)
            {
                List<Vector2> wallPositions = Program.GetWallPositions(Camille.Position);
                Vector2 closestToCursorPos = Vector2.Zero;
                //this is to reduce the amount of times .Distance is used
                float closestToCursorPosDistance = float.MaxValue;
                foreach (Vector2 wallPos in wallPositions)
                {
                    List<Vector2> dashPoses = Program.GetDashablePositions(wallPos.To3D());
                    foreach (Vector2 dashPos in dashPoses)
                    {
                        if (dashPos.Distance(Game.CursorPos) <= closestToCursorPosDistance)
                        {
                            closestToCursorPos = wallPos;
                            closestToCursorPosDistance = dashPos.Distance(Game.CursorPos);
                        }
                    }
                }

                LastECheckTime = Game.Time;
                if(closestToCursorPos != Vector2.Zero)
                    CastE(closestToCursorPos.To3D());
            }
            else if (Program.E.IsReady() && Program.E.Name == "CamilleEDash2" && Game.Time - LastECheckTime > 0.1)
            {
                Vector2 closestToCursorPos = Vector2.Zero;
                float closestToCursorPosDistance = float.MaxValue;
                List<Vector2> dashPoses = Program.GetDashablePositions(Camille.Position);
                foreach (Vector2 dashPos in dashPoses)
                {
                    if (dashPos.Distance(Game.CursorPos) <= closestToCursorPosDistance)
                    {
                        closestToCursorPos = dashPos;
                        closestToCursorPosDistance = dashPos.Distance(Game.CursorPos);
                    }
                }

                LastECheckTime = Game.Time;
                if (closestToCursorPos != Vector2.Zero)
                    Orbwalker.MoveTo(closestToCursorPos.To3D());
            }
        }

        public static void LaneClear()
        {
            Menu menu = MenuHandler.LaneClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q1") && Program.Q.Name == "CamilleQ")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use Q2") && Program.Q.Name == "CamilleQ2")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, false);

            //if (menu.GetCheckboxValue("Force Follow in W Range"))
                //WFollow();

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleE")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleEDash2")
                CastE(enemies, false);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, false);
        }

        public static void LastHit()
        {
            Menu menu = MenuHandler.LastHit;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

            if (menu.GetCheckboxValue("Use Q1") && Program.Q.Name == "CamilleQ")
                CastQ(enemies, true);

            if (menu.GetCheckboxValue("Use Q2") && Program.Q.Name == "CamilleQ2")
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use W"))
                CastW(enemies, true);

            //if (menu.GetCheckboxValue("Force Follow in W Range"))
                //WFollow();

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleE")
                CastE(enemies, true);

            if (menu.GetCheckboxValue("Use E1") && Program.E.Name == "CamilleEDash2")
                CastE(enemies, true);

            if (menu.GetCheckboxValue("Use Items"))
                UseItems(enemies, true);
        }

        public static void UseItems(List<Obj_AI_Base> enemies, bool ks)
        {
            #region Item Initialization
            InventorySlot QSS = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Quick Silver Sash")) ? Camille.GetItem(ItemId.Quicksilver_Sash) : null,
                MercurialsScimitar = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Mercurials Scimitar")) ? Camille.GetItem(ItemId.Mercurial_Scimitar) : null,
                RavenousHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Ravenous Hydra")) ? Camille.GetItem(ItemId.Ravenous_Hydra) : null,
                TitanicHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Titanic Hydra")) ? Camille.GetItem(ItemId.Titanic_Hydra) : null,
                Tiamat = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Tiamat")) ? Camille.GetItem(ItemId.Tiamat) : null,
                Youmuus = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Youmuus")) ? Camille.GetItem(ItemId.Youmuus_Ghostblade) : null,
                BOTRK = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Blade of the Ruined King")) ? Camille.GetItem(ItemId.Blade_of_the_Ruined_King) : null,
                BilgewaterCutlass = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Bilgewater Cutlass")) ? Camille.GetItem(ItemId.Bilgewater_Cutlass) : null,
                HextechGunblade = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Hextech Gunblade")) ? Camille.GetItem(ItemId.Hextech_Gunblade) : null;
            #endregion

            #region QSS
            if (!hasDoneActionThisTick &&
                QSS.MeetsCriteria() &&
                (Camille.HasBuffOfType(BuffType.Blind)
                || Camille.HasBuffOfType(BuffType.Charm)
                || Camille.HasBuffOfType(BuffType.Fear)
                || Camille.HasBuffOfType(BuffType.Knockback)
                || Camille.HasBuffOfType(BuffType.Silence)
                || Camille.HasBuffOfType(BuffType.Snare)
                || Camille.HasBuffOfType(BuffType.Stun)
                || Camille.HasBuffOfType(BuffType.Taunt))
                //not being knocked back by dragon
                && !Camille.HasBuff("moveawaycollision")
                //not standing on raka silence
                && !Camille.HasBuff("sorakaepacify"))
                hasDoneActionThisTick = QSS.Cast();
            #endregion

            #region Mercurials Scimitar
            if (!hasDoneActionThisTick &&
                MercurialsScimitar.MeetsCriteria() &&
                (Camille.HasBuffOfType(BuffType.Blind)
                || Camille.HasBuffOfType(BuffType.Charm)
                || Camille.HasBuffOfType(BuffType.Fear)
                || Camille.HasBuffOfType(BuffType.Knockback)
                || Camille.HasBuffOfType(BuffType.Silence)
                || Camille.HasBuffOfType(BuffType.Snare)
                || Camille.HasBuffOfType(BuffType.Stun)
                || Camille.HasBuffOfType(BuffType.Taunt))
                //not being knocked back by dragon
                && !Camille.HasBuff("moveawaycollision")
                //not standing on raka silence
                && !Camille.HasBuff("sorakaepacify"))
                hasDoneActionThisTick = MercurialsScimitar.Cast();
            #endregion

            #region Ravenous Hydra
            if (!hasDoneActionThisTick &&
                RavenousHydra.MeetsCriteria()
                && Camille.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Camille, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null)
                && (
                enemies.Where(a => a.Type == GameObjectType.AIHeroClient && a.IsInRange(Camille, 400)).Count() >= MenuHandler.Items.GetSliderValue("Champions to use Tiamat/Ravenous Hydra on")
                || enemies.Where(a => a.Type != GameObjectType.AIHeroClient && a.IsInRange(Camille, 400)).Count() >= MenuHandler.Items.GetSliderValue("Minions to use Tiamat/Ravenous Hydra on")))
                hasDoneActionThisTick = RavenousHydra.Cast();
            #endregion

            #region Titanic Hydra
            if (!hasDoneActionThisTick &&
                TitanicHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Camille, Camille.GetAutoAttackRange())).FirstOrDefault() != null
                && Camille.IsAutoCanceling(enemies)
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Camille, a, ItemId.Titanic_Hydra)).FirstOrDefault() != null))
                hasDoneActionThisTick = TitanicHydra.Cast();
            #endregion

            #region Tiamat
            if (!hasDoneActionThisTick &&
                Tiamat.MeetsCriteria()
                && Camille.IsAutoCanceling(enemies)
                && enemies.Where(a => a.IsInRange(Camille, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Camille, a, ItemId.Tiamat)).FirstOrDefault() != null)
                && (
                enemies.Where(a => a.Type == GameObjectType.AIHeroClient && a.IsInRange(Camille, 400)).Count() >= MenuHandler.Items.GetSliderValue("Champions to use Tiamat/Ravenous Hydra on")
                || enemies.Where(a => a.Type != GameObjectType.AIHeroClient && a.IsInRange(Camille, 400)).Count() >= MenuHandler.Items.GetSliderValue("Minions to use Tiamat/Ravenous Hydra on")))
                hasDoneActionThisTick = Tiamat.Cast();
            #endregion

            #region Youmuus
            if (!hasDoneActionThisTick &&
                Youmuus.MeetsCriteria()
                && Camille.CountEnemyHeroesInRangeWithPrediction((int)Camille.GetAutoAttackRange(), 0) >= 1)
                hasDoneActionThisTick = Youmuus.Cast();
            #endregion

            //all targeted spells that must be used on champions must be called after this
            enemies = enemies.Where(a => a.Type == GameObjectType.AIHeroClient).ToList();
            var target = enemies.OrderBy(a => a.Health).FirstOrDefault();

            #region Hextech Gunblade
            if (!hasDoneActionThisTick &&
                target != null
                && HextechGunblade.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Camille, 700)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Camille, a, ItemId.Hextech_Gunblade)).FirstOrDefault() != null))
                hasDoneActionThisTick = HextechGunblade.Cast(target);
            #endregion

            #region BOTRK
            if (!hasDoneActionThisTick &&
                target != null
                && BOTRK.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Camille, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Camille, a, ItemId.Blade_of_the_Ruined_King)).FirstOrDefault() != null))
                hasDoneActionThisTick = BOTRK.Cast(target);
            #endregion

            #region Bilgewater Cutlass
            if (!hasDoneActionThisTick &&
                target != null
                && BilgewaterCutlass.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Camille, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Camille, a, ItemId.Bilgewater_Cutlass)).FirstOrDefault() != null))
                hasDoneActionThisTick = BilgewaterCutlass.Cast(target);
            #endregion
        }
        
        public static void UseIgnite(List<Obj_AI_Base> enemies, bool ks)
        {
            Spell.Targeted ignite = new Spell.Targeted(Camille.GetSpellSlotFromName("SummonerDot"), 600, DamageType.True);

            if (ignite.Slot == SpellSlot.Unknown || !ignite.IsReady())
                return;

            Obj_AI_Base unit = enemies.Where(a =>
                a.IsInRange(Camille, ignite.Range)
                && (!ks || Calculations.Ignite(a) >= a.Health)
                && a.MeetsCriteria()).FirstOrDefault();

            if (unit != null)
                hasDoneActionThisTick = ignite.Cast(unit);
        }
        
        public static void CastQ(List<Obj_AI_Base> enemies, bool ks)
        {
            if (Camille.HasBuff("CamilleQ2") && ks)
                enemies = enemies.Where(a => a.Health <= Calculations.Q2(a, Camille.HasBuff("camilleqprimingcomplete"))).ToList();
            else if (!Camille.HasBuff("CamilleQ2") && ks)
                enemies = enemies.Where(a => a.Health <= Calculations.Q1(a)).ToList();
            else if (!ks && (Program.Q.Name != "CamilleQ" && !Camille.HasBuff("camilleqprimingcomplete")))
                return;

            if(enemies.Count > 0)
                CastQ(enemies.First());
        }
        public static void CastQ(Obj_AI_Base enemy)
        {
            if (Program.Q.IsReady() && !hasDoneActionThisTick && Game.Time - LastAutoTime <= 0.25f)
            {
                Orbwalker.ResetAutoAttack();
                hasDoneActionThisTick = Program.Q.Cast();

                if (hasDoneActionThisTick)
                    Player.IssueOrder(GameObjectOrder.AutoAttack, enemy);
            }
        }
        public static void CastW(List<Obj_AI_Base> enemies, bool ks)
        {
            if (Program.W.IsReady() && !hasDoneActionThisTick && enemies.Count > 0)
            {
                if (ks)
                    enemies = enemies.Where(a => a.Health <= Calculations.W(a)).ToList();

                int numHit = 0;
                Vector3 bestPos = Program.W.GetBestConeCastPosition(enemies, out numHit);

                Chat.Print(numHit);
                
                if (numHit > 0 && bestPos != Vector3.Zero)
                    hasDoneActionThisTick = Program.W.Cast(bestPos);
            }
        }
        public static void CastE(List<Obj_AI_Base> enemies, bool ks)
        {
            if (Program.E.IsReady() && !hasDoneActionThisTick && enemies.Count > 0)
            {
                Obj_AI_Base closestEnemy = enemies.OrderBy(a => a.Distance(Camille)).FirstOrDefault();
                if (closestEnemy != null)
                {
                    Vector3 closestPos = closestEnemy.Position;

                    if (Program.E.IsReady() && Program.E.Name == "CamilleE" && Game.Time - LastECheckTime > 0.25f)
                    {
                        List<Vector2> wallPositions = Program.GetWallPositions(Camille.Position);
                        Vector2 closestToEnemyPos = Vector2.Zero;
                        //this is to reduce the amount of times .Distance is used
                        float closestToEnemyPosDistance = float.MaxValue;
                        foreach (Vector2 wallPos in wallPositions)
                        {
                            List<Vector2> dashPoses = Program.GetDashablePositions(wallPos.To3D());
                            foreach (Vector2 dashPos in dashPoses)
                            {
                                if (dashPos.Distance(Game.CursorPos) <= closestToEnemyPosDistance)
                                {
                                    closestToEnemyPos = wallPos;
                                    closestToEnemyPosDistance = dashPos.Distance(closestPos);
                                }
                            }
                        }

                        LastECheckTime = Game.Time;
                        if (closestToEnemyPos != Vector2.Zero)
                            CastE(closestToEnemyPos.To3D());
                    }
                    else if (Program.E.IsReady() && Program.E.Name == "CamilleEDash2" && Game.Time - LastECheckTime > 0.1)
                    {
                        Vector2 closestToEnemyPos = Vector2.Zero;
                        float closestToEnemyPosDistance = float.MaxValue;
                        List<Vector2> dashPoses = Program.GetDashablePositions(Camille.Position);
                        foreach (Vector2 dashPos in dashPoses)
                        {
                            if (dashPos.Distance(closestPos) <= closestToEnemyPosDistance)
                            {
                                closestToEnemyPos = dashPos;
                                closestToEnemyPosDistance = dashPos.Distance(closestPos);
                            }
                        }

                        LastECheckTime = Game.Time;
                        if (closestToEnemyPos != Vector2.Zero)
                        {
                            if (closestEnemy.IsInRange(closestEnemy, 300))
                                Player.IssueOrder(GameObjectOrder.AutoAttack, closestEnemy);
                            else
                                Orbwalker.MoveTo(closestToEnemyPos.To3D());
                        }
                    }
                }   
            }
        }
        public static void CastE(Vector3 pos)
        {
            if (Program.E.IsReady() && !hasDoneActionThisTick)
                hasDoneActionThisTick = Program.E.Cast(pos);
        }
        //most enemies trapped in box?
        public static void CastR(List<Obj_AI_Base> enemies)
        {
            if (Program.R.IsReady() && enemies.Any(a => a.IsInRange(Camille, Program.R.Range)))
                hasDoneActionThisTick = Program.R.Cast(enemies.First(a => a.IsInRange(Camille, Program.R.Range)));
        }

        public static void WFollow()
        {

        }
    }
}
