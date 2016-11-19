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

namespace UnsignedYasuo
{
    class YasuoFunctions
    {
        public static bool didActionThisTick = false;

        public static AIHeroClient Yasuo { get { return ObjectManager.Player; } }

        //complete
        public static void LastHit()
        {
            Menu menu = MenuHandler.LastHit;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();
            bool EUnderTower = menu.GetCheckboxValue("Use E Under Tower");

            if ((menu.GetCheckboxValue("Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (menu.GetCheckboxValue("Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                CastQ(enemies, true);

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, true, EUnderTower);

            if (menu.GetCheckboxValue("Use EQ"))
                CastEQ(enemies, true, EUnderTower);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, true);
        }

        //complete
        public static void LaneClear()
        {
            Menu menu = MenuHandler.LaneClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();
            bool EUnderTower = menu.GetCheckboxValue("Use E Under Tower");

            if ((menu.GetCheckboxValue("Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (menu.GetCheckboxValue("Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Use E only for Last Hit"))
                    CastE(enemies, true, EUnderTower);
                else
                    CastE(enemies, false, EUnderTower);
            }

            if (menu.GetCheckboxValue("Use EQ"))
                CastEQ(enemies, false, EUnderTower);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);
        }

        //complete
        public static void JungleClear()
        {
            Menu menu = MenuHandler.JungleClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList();
            bool EUnderTower = false;

            if ((menu.GetCheckboxValue("Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (menu.GetCheckboxValue("Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use E"))
            {
                if (menu.GetCheckboxValue("Use E only for Last Hit"))
                    CastE(enemies, true, EUnderTower);
                else
                    CastE(enemies, false, EUnderTower);
            }

            if (menu.GetCheckboxValue("Use EQ"))
                CastEQ(enemies, false, EUnderTower);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);
        }

        //complete
        public static void KS()
        {
            Menu menu = MenuHandler.Killsteal;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToObj_AI_BaseList();
            bool EUnderTower = menu.GetCheckboxValue("Use E Under Tower");

            if ((menu.GetCheckboxValue("Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (menu.GetCheckboxValue("Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                CastQ(enemies, true);

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, true, EUnderTower);

            if (menu.GetCheckboxValue("Use EQ"))
                CastEQ(enemies, true, EUnderTower);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, true);

            if (menu.GetCheckboxValue("Use Ignite"))
                CastIgnite(enemies, true);
        }

        //complete
        public static void Harrass()
        {
            Menu menu = MenuHandler.Harass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToObj_AI_BaseList();
            bool EUnderTower = menu.GetCheckboxValue("Use E Under Tower");

            if ((menu.GetCheckboxValue("Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (menu.GetCheckboxValue("Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, EUnderTower);
            
            if (menu.GetCheckboxValue("Use EQ"))
                CastEQ(enemies, false, EUnderTower);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);

            if (menu.GetCheckboxValue("Use R"))
                UltHandler();
        }

        //complete
        public static void Combo()
        {
            Menu menu = MenuHandler.Combo;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToObj_AI_BaseList();
            bool EUnderTower = menu.GetCheckboxValue("Use E Under Tower");

            if ((menu.GetCheckboxValue("Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (menu.GetCheckboxValue("Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, EUnderTower);

            if (menu.GetComboBoxText("Dash Mode: ") == "Gapclose")
                EGapClose(enemies, EUnderTower);

            if (menu.GetComboBoxText("Dash Mode: ") == "To Mouse")
                EToMouse(EUnderTower,
                    false, menu.GetCheckboxValue("Use EQ"), enemies);

            if (menu.GetCheckboxValue("Use EQ"))
                CastEQ(enemies, false, EUnderTower);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);

            if (menu.GetCheckboxValue("Use R"))
                UltHandler();

            if (menu.GetCheckboxValue("Beyblade"))
                TryBeyBlade();
        }

        //complete
        public static void Flee()
        {
            WallDash activeDash = null;

            Menu menu = MenuHandler.Flee;

            if (menu.GetCheckboxValue("Wall Dash") && Program.E.IsReady() && !YasuoCalcs.IsDashing())
            {
                //walldash
                foreach (WallDash wd in YasuoWallDashDatabase.wallDashDatabase.Where(a => a.startPosition.Distance(Yasuo) <= 1300))
                    if (EntityManager.MinionsAndMonsters.Combined.Where(a => a.MeetsCriteria() && a.Name == wd.unitName && a.ServerPosition.Distance(wd.dashUnitPosition) <= 2).FirstOrDefault() != null)
                    {
                        Geometry.Polygon.Circle dashCircle = new Geometry.Polygon.Circle(wd.endPosition, 120);
                        if (dashCircle.IsInside(Game.CursorPos))
                        {
                            activeDash = wd;
                            break;
                        }
                    }
            }

            if (menu.GetCheckboxValue("Use E") || activeDash != null)
            {
                if (activeDash == null)
                {
                    Orbwalker.MoveTo(Game.CursorPos);
                    EToMouse(menu.GetCheckboxValue("Use E Under Tower"), menu.GetCheckboxValue("Stack Q"), false);
                }
                else
                {
                    //first check if the positions are exact
                    if (Yasuo.Position.To2D() == activeDash.startPosition.To2D())
                        CastE(EntityManager.MinionsAndMonsters.Combined.Where(a => a.Name == activeDash.unitName).ToList().ToObj_AI_BaseList(), false, menu.GetCheckboxValue("Use E Under Tower"));
                    else
                        Orbwalker.MoveTo(activeDash.startPosition);

                    //if the positions aren't exact
                    //if (Yasuo.Position.Distance(activeDash.startPosition) > 50)
                    //    return;

                    Vector3 startPos = Yasuo.Position,
                        dashEndPos = YasuoCalcs.GetDashingEnd(EntityManager.MinionsAndMonsters.Combined.Where(a => a.MeetsCriteria() && YasuoCalcs.ERequirements(a, MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Use E Under Tower")) && a.Name == activeDash.unitName).FirstOrDefault()),
                        fakeEndPos = startPos.To2D().Extend(dashEndPos.To2D(), 1000).To3D() + new Vector3(0, 0, startPos.Z),
                        slope = new Vector3(dashEndPos.X - startPos.X, dashEndPos.Y - startPos.Y, 0),
                        fakeSlope = new Vector3(fakeEndPos.X - startPos.X, fakeEndPos.Y - startPos.Y, 0),
                        actualDashPosition = Vector3.Zero;

                    List<Vector3> pointsAlongPath = new List<Vector3>();
                    List<Vector3> straightLinePath = new List<Vector3>();

                    int points = 100;

                    pointsAlongPath.Add(startPos);

                    //get all points in a line from start to fake end
                    for (int i = 0; i < points; i++)
                        straightLinePath.Add(startPos + (i * (fakeSlope / points)));

                    bool isWall = false;

                    //get all wall start and end positions
                    for (int i = 0; i < points; i++)
                    {
                        //wall start
                        if (!isWall && straightLinePath[i].IsWall())
                        {
                            pointsAlongPath.Add(straightLinePath[i]);
                            isWall = true;
                        }
                        //wall end
                        if (isWall && !straightLinePath[i].IsWall())
                        {
                            pointsAlongPath.Add(straightLinePath[i]);
                            isWall = false;
                        }
                    }

                    pointsAlongPath.Add(fakeEndPos);

                    Vector3 closestWall = pointsAlongPath.Where(a => a.IsWall()).OrderBy(a => a.Distance(dashEndPos)).FirstOrDefault(),
                        closestWallsEndPosition = (pointsAlongPath.IndexOf(closestWall) + 1 == pointsAlongPath.Count) ? Vector3.Zero : pointsAlongPath[pointsAlongPath.IndexOf(closestWall) + 1];

                    //none of the points are a wall so the end point is the dash position
                    if (!pointsAlongPath.Any(a => a.IsWall()))
                        actualDashPosition = dashEndPos;
                    // OR none of the walls are in the E range
                    else if (pointsAlongPath.Where(a => a.IsWall()).OrderBy(a => a.Distance(startPos)).FirstOrDefault() != null &&
                        pointsAlongPath.Where(a => a.IsWall()).OrderBy(a => a.Distance(startPos)).FirstOrDefault().Distance(startPos) > Program.E.Range)
                        actualDashPosition = dashEndPos;
                    //or the dashing end is not a wall
                    else if (!dashEndPos.IsWall())
                        actualDashPosition = dashEndPos;
                    //find the nearest wall to the dash position
                    else if (closestWall != Vector3.Zero && closestWallsEndPosition != Vector3.Zero &&
                        closestWall != null && closestWallsEndPosition != null &&
                        closestWallsEndPosition.Distance(dashEndPos) < closestWall.Distance(dashEndPos) &&
                        startPos.Distance(closestWallsEndPosition) <= 630)
                        actualDashPosition = closestWallsEndPosition;
                    //the end position is the first wall
                    else
                        actualDashPosition = pointsAlongPath.First(a => a.IsWall());

                    //if the end position is close enough to the walldash position, dash
                    if (actualDashPosition.Distance(activeDash.endPosition) <= menu.GetSliderValue("Wall Dash Extra Space"))
                        CastE(EntityManager.MinionsAndMonsters.Combined.Where(a => a.MeetsCriteria() && YasuoCalcs.ERequirements(a, menu.GetCheckboxValue("Use E Under Tower")) && a.Name == activeDash.unitName).FirstOrDefault());
                }
            }
        }

        //complete
        public static void AutoHarrass()
        {
            if (Yasuo.IsRecalling() || Yasuo.IsUnderEnemyturret() ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                return;

            Menu menu = MenuHandler.AutoHarass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToObj_AI_BaseList();
            bool EUnderTower = menu.GetCheckboxValue("Use E Under Tower");

            if ((menu.GetCheckboxValue("Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (menu.GetCheckboxValue("Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                CastQ(enemies, false);

            if (menu.GetCheckboxValue("Use E"))
                CastE(enemies, false, EUnderTower);

            if (menu.GetCheckboxValue("Use EQ"))
                CastEQ(enemies, false, EUnderTower);

            if (menu.GetCheckboxValue("Use Items"))
                CastItems(enemies, false);
        }

        //Items need testing. Should work.
        public static void CastItems(List<Obj_AI_Base> enemies, bool ks)
        {
            #region Item Initialization
            InventorySlot QSS = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Quick Silver Sash")) ? Yasuo.GetItem(ItemId.Quicksilver_Sash) : null,
                MercurialsScimitar = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Mercurials Scimitar")) ? Yasuo.GetItem(ItemId.Mercurial_Scimitar) : null,
                RavenousHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Ravenous Hydra")) ? Yasuo.GetItem(ItemId.Ravenous_Hydra) : null,
                TitanicHydra = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Titanic Hydra")) ? Yasuo.GetItem(ItemId.Titanic_Hydra) : null,
                Tiamat = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Tiamat")) ? Yasuo.GetItem(ItemId.Tiamat) : null,
                Youmuus = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Youmuus")) ? Yasuo.GetItem(ItemId.Youmuus_Ghostblade) : null,
                BOTRK = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Blade of the Ruined King")) ? Yasuo.GetItem(ItemId.Blade_of_the_Ruined_King) : null,
                BilgewaterCutlass = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Bilgewater Cutlass")) ? Yasuo.GetItem(ItemId.Bilgewater_Cutlass) : null,
                HextechGunblade = (MenuHandler.GetCheckboxValue(MenuHandler.Items, "Use Hextech Gunblade")) ? Yasuo.GetItem(ItemId.Hextech_Gunblade) : null;
            #endregion

            #region QSS
            if (!didActionThisTick &&
                QSS.MeetsCriteria() &&
                (Yasuo.HasBuffOfType(BuffType.Blind)
                || Yasuo.HasBuffOfType(BuffType.Charm)
                || Yasuo.HasBuffOfType(BuffType.Fear)
                || Yasuo.HasBuffOfType(BuffType.Knockback)
                || Yasuo.HasBuffOfType(BuffType.Silence)
                || Yasuo.HasBuffOfType(BuffType.Snare)
                || Yasuo.HasBuffOfType(BuffType.Stun)
                || Yasuo.HasBuffOfType(BuffType.Taunt))
                //not being knocked back by dragon
                && !Yasuo.HasBuff("moveawaycollision")
                //not standing on raka silence
                && !Yasuo.HasBuff("sorakaepacify"))
                didActionThisTick = QSS.Cast();
            #endregion

            #region Mercurials Scimitar
            if (!didActionThisTick &&
                MercurialsScimitar.MeetsCriteria() &&
                (Yasuo.HasBuffOfType(BuffType.Blind)
                || Yasuo.HasBuffOfType(BuffType.Charm)
                || Yasuo.HasBuffOfType(BuffType.Fear)
                || Yasuo.HasBuffOfType(BuffType.Knockback)
                || Yasuo.HasBuffOfType(BuffType.Silence)
                || Yasuo.HasBuffOfType(BuffType.Snare)
                || Yasuo.HasBuffOfType(BuffType.Stun)
                || Yasuo.HasBuffOfType(BuffType.Taunt))
                //not being knocked back by dragon
                && !Yasuo.HasBuff("moveawaycollision")
                //not standing on raka silence
                && !Yasuo.HasBuff("sorakaepacify"))
                didActionThisTick = MercurialsScimitar.Cast();
            #endregion

            #region Ravenous Hydra
            if (!didActionThisTick &&
                RavenousHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Yasuo, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Yasuo, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null))
                didActionThisTick = RavenousHydra.Cast();
            #endregion

            #region Titanic Hydra
            if (!didActionThisTick &&
                TitanicHydra.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Yasuo, Yasuo.GetAutoAttackRange())).FirstOrDefault() != null
                && !Orbwalker.CanAutoAttack
                && !Orbwalker.IsAutoAttacking
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Yasuo, a, ItemId.Titanic_Hydra)).FirstOrDefault() != null))
                didActionThisTick = TitanicHydra.Cast();
            #endregion

            #region Tiamat
            if (!didActionThisTick &&
                Tiamat.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Yasuo, 400)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Yasuo, a, ItemId.Tiamat)).FirstOrDefault() != null))
                didActionThisTick = Tiamat.Cast();
            #endregion

            #region Youmuus
            if (!didActionThisTick &&
                Youmuus.MeetsCriteria()
                && Yasuo.CountEnemyHeroesInRangeWithPrediction((int)Yasuo.GetAutoAttackRange(), 0) >= 1)
                didActionThisTick = Youmuus.Cast();
            #endregion

            //all targeted spells that must be used on champions must be called after this
            enemies = enemies.Where(a => a.Type == GameObjectType.AIHeroClient).ToList();

            #region Hextech Gunblade
            if (!didActionThisTick &&
                HextechGunblade.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Yasuo, 700)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Yasuo, a, ItemId.Hextech_Gunblade)).FirstOrDefault() != null))
                didActionThisTick = HextechGunblade.Cast(enemies.OrderBy(a => a.Health).FirstOrDefault());
            #endregion

            #region BOTRK
            if (!didActionThisTick &&
                BOTRK.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Yasuo, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Yasuo, a, ItemId.Blade_of_the_Ruined_King)).FirstOrDefault() != null))
                didActionThisTick = BOTRK.Cast(enemies.OrderBy(a => a.Health).FirstOrDefault());
            #endregion

            #region Bilgewater Cutlass
            if (!didActionThisTick &&
                BilgewaterCutlass.MeetsCriteria()
                && enemies.Where(a => a.IsInRange(Yasuo, 550)).FirstOrDefault() != null
                && (!ks || enemies.Where(a => a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Yasuo, a, ItemId.Bilgewater_Cutlass)).FirstOrDefault() != null))
                didActionThisTick = BilgewaterCutlass.Cast(enemies.OrderBy(a => a.Health).FirstOrDefault());
            #endregion
        }

        public static void CastE(List<Obj_AI_Base> enemies, bool ks, bool goUnderEnemyTower)
        {
            if (!Program.E.IsReady() || YasuoCalcs.IsDashing() || didActionThisTick || !enemies.Any(a => a.MeetsCriteria() && a.IsInRange(Yasuo, Program.E.Range)))
                return;

            Obj_AI_Base unit = enemies.Where(a =>
            a.MeetsCriteria()
            && a.IsInRange(Yasuo, Program.E.Range)
            && (!ks || YasuoCalcs.E(a) >= a.Health)
            && YasuoCalcs.ERequirements(a, goUnderEnemyTower)
            ).FirstOrDefault();

            if (unit != null)
                CastE(unit);
        }
        public static void CastIgnite(List<Obj_AI_Base> enemies, bool ks)
        {
            if (!Program.Ignite.IsReady() || !enemies.Any(a => a.IsInRange(Yasuo, Program.Ignite.Range)) || didActionThisTick)
                return;

            Obj_AI_Base unit = enemies.Where(a =>
                a.IsInRange(Yasuo, Program.Ignite.Range)
                && (!ks || YasuoCalcs.Ignite(a) >= a.Health)
                && a.MeetsCriteria()).FirstOrDefault();

            if (unit != null)
                didActionThisTick =  Program.Ignite.Cast(unit);
        }

        public static void CastE(Obj_AI_Base unit)
        {
            if (!Program.E.IsReady() || YasuoCalcs.IsDashing() || !unit.IsInRange(Yasuo, Program.E.Range) || didActionThisTick)
                return;

            if (unit != null)
                didActionThisTick = Program.E.Cast(unit);
        }

        public static void EGapClose(List<Obj_AI_Base> targets, bool goUnderEnemyTower)
        {
            if (!Program.E.IsReady() || YasuoCalcs.IsDashing() || didActionThisTick) 
                return;
            //if none of the targets are in auto attack range
            if (targets.Where(a => a.IsInRange(Yasuo, Yasuo.GetAutoAttackRange())).FirstOrDefault() == null)
            {
                //get the closest target
                Obj_AI_Base closestEnemy = targets.Where(a => a.MeetsCriteria() && a.IsInRange(Yasuo, 5000)).OrderBy(b => b.Distance(Yasuo)).FirstOrDefault();

                if (closestEnemy != null)
                {
                    //get all enemies in my E range
                    List<Obj_AI_Base> enemiesInERange = EntityManager.Enemies.Where(a => a.MeetsCriteria() && a != closestEnemy && YasuoCalcs.ERequirements(a, goUnderEnemyTower) && a.IsInRange(Yasuo, Program.E.Range)).ToList();

                    Obj_AI_Base enemyToDashTo = enemiesInERange.OrderBy(a => YasuoCalcs.GetDashingEnd(a).Distance(closestEnemy)).FirstOrDefault();

                    if (enemyToDashTo != null && YasuoCalcs.GetDashingEnd(enemyToDashTo).Distance(closestEnemy) < Yasuo.Distance(closestEnemy))
                        CastE(enemyToDashTo);
                }
            }
        }

        public static void EToMouse(bool goUnderEnemyTower, bool stackQ, bool EQ, List<Obj_AI_Base> EQTargets = null)
        {
            if (Program.E.IsReady() && !YasuoCalcs.IsDashing() && !didActionThisTick)
            {
                Geometry.Polygon.Sector sector = new Geometry.Polygon.Sector(Yasuo.Position, Game.CursorPos, (float)(30 * Math.PI / 180), Program.E.Range);

                List<Obj_AI_Base> dashableEnemies = EntityManager.Enemies.Where(a => !a.IsDead && a.MeetsCriteria() && YasuoCalcs.ERequirements(a, goUnderEnemyTower) && a.IsInRange(Yasuo.Position, Program.E.Range) && sector.IsInside(a)).OrderBy(a => YasuoCalcs.GetDashingEnd(a).Distance(Game.CursorPos)).ToList();
                List<Obj_AI_Base> dashableEnemiesWithTargets = dashableEnemies.Where(a => YasuoCalcs.GetDashingEnd(a).CountEnemyHeroesInRangeWithPrediction((int)Program.EQ.Range, 250) >= 1)
                    .OrderBy(a => YasuoCalcs.GetDashingEnd(a).CountEnemyHeroesInRangeWithPrediction((int)Program.EQ.Range, 250)).ToList();

                if (YasuoCalcs.WillQBeReady() && stackQ && !Yasuo.HasBuff("yasuoq3w") && dashableEnemies.Count != 0)
                    CastEQ(dashableEnemies, false, goUnderEnemyTower);
                else if (YasuoCalcs.WillQBeReady() && EQ && dashableEnemiesWithTargets.Count != 0)
                    CastEQ(dashableEnemies, false, goUnderEnemyTower, EQTargets);
                else if(dashableEnemies.FirstOrDefault() != null)
                    CastE(dashableEnemies.First());
            }
        }

        public static void CastEQ(List<Obj_AI_Base> dashEnemies, bool ks, bool goUnderEnemyTower, List<Obj_AI_Base> EQEnemies = null)
        {
            if (!Program.E.IsReady() || !YasuoCalcs.WillQBeReady() || didActionThisTick ||
                !dashEnemies.Any(a => a.IsInRange(Yasuo, Program.E.Range)) ||
                Yasuo.GetNearbyEnemies(Program.E.Range).Count() == 0 || YasuoCalcs.IsDashing())
                return;

            if (EQEnemies == null)
                EQEnemies = dashEnemies;

            int numberOfEnemiesHitWithEQ = 0;
            Obj_AI_Base unitToDashTo = null;

            List<Obj_AI_Base> possibleDashUnits = dashEnemies.Where(a => a.MeetsCriteria() && a.IsInRange(Yasuo, Program.E.Range) && YasuoCalcs.ERequirements(a, goUnderEnemyTower) && a.Health > YasuoCalcs.E(a)).ToList();
            foreach (Obj_AI_Base possibleDashUnit in possibleDashUnits)
            {
                List<Obj_AI_Base> unitsHitWithEQ = EQEnemies.Where(a => a.MeetsCriteria() && a.Position(250).IsInRange(YasuoCalcs.GetDashingEnd(possibleDashUnit), Program.EQ.Range)).ToList();
                if (ks)
                    unitsHitWithEQ = unitsHitWithEQ.Where(a => a.Health <= YasuoCalcs.Q(a) || (a.Name == possibleDashUnit.Name && a.Health <= YasuoCalcs.Q(a) + YasuoCalcs.E(a))).ToList();

                if (numberOfEnemiesHitWithEQ < unitsHitWithEQ.Count())
                {
                    numberOfEnemiesHitWithEQ = unitsHitWithEQ.Count();
                    unitToDashTo = possibleDashUnit;
                }
            }

            if (unitToDashTo != null && numberOfEnemiesHitWithEQ >= 1)
            {
                CastE(unitToDashTo);
                if (YasuoCalcs.GetQReadyTimeInt() == 0)
                    CastEQsQ(EQEnemies);
                else
                    Core.DelayAction(delegate { CastEQsQ(EQEnemies); }, YasuoCalcs.GetQReadyTimeInt());
                didActionThisTick = true;
            }
        }

        public static void CastEQsQ(List<Obj_AI_Base> dashEnemies)
        {
            if (didActionThisTick || !Yasuo.IsDashing() || dashEnemies.Where(a=>a.Position(250).Distance(Yasuo) <= Program.EQ.Range).Count() == 0)
                return;

            didActionThisTick = Program.Q.Cast(Yasuo.Position + new Vector3(50, 0, 0));
        }

        public static void CastQ(List<Obj_AI_Base> enemies, bool ks)
        {
            if (didActionThisTick || !Program.Q.IsReady() || YasuoCalcs.IsDashing() ||
                !enemies.Any(a =>
                (Yasuo.HasBuff("YasuoQ3W") && a.IsInRange(Yasuo, Program.Q3.Range)) || (!Yasuo.HasBuff("YasuoQ3W") && a.IsInRange(Yasuo, Program.Q.Range))) ||
                !Yasuo.IsAutoCanceling(enemies))
                return;

            if (ks)
                enemies = enemies.Where(a => YasuoCalcs.Q(a) >= a.Health).ToList();

            int enemiesHit = 0;
            Vector3 bestPos = Vector3.Zero;
            if (Yasuo.HasBuff("YasuoQ3W"))
                bestPos = Program.Q3.GetBestLinearPredictionPos(enemies, Yasuo.Position, out enemiesHit);
            else
                bestPos = Program.Q.GetBestLinearPredictionPos(enemies, Yasuo.Position + new Vector3(0, 0, 150f), out enemiesHit);

            if (bestPos != Vector3.Zero && enemiesHit > 0)
            {
                if (Yasuo.HasBuff("YasuoQ3W"))
                    didActionThisTick = Program.Q3.Cast(bestPos);
                else
                    didActionThisTick = Program.Q.Cast(bestPos);
            }
        }

        public static void UltHandler()
        {
            if (!Program.R.IsReady() || didActionThisTick)
                return;

            if (MenuHandler.Ult.GetCheckboxValue("Use R for Flow") &&
                Yasuo.Mana != 100)
                CastR();

            if (MenuHandler.Ult.GetCheckboxValue("Use R on All Enemies in Range") &&
                EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria() && a.IsInRange(Yasuo, Program.R.Range) && a.IsKnockedUp()).Count() == Yasuo.CountEnemyHeroesInRangeWithPrediction(2000, 0))
                CastR();

            if (MenuHandler.Ult.GetCheckboxValue("Use R at 10% HP") &&
                Yasuo.HealthPercent <= 0.10f &&
                Yasuo.Mana != 100)
                CastR(true);

            if (EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria() && a.IsInRange(Yasuo, Program.R.Range) && a.IsKnockedUp()).Count() >= MenuHandler.Ult.GetSliderValue("Use R on x Enemies or more:"))
                CastR(true);
        }

        public static void CastR(bool force = false)
        {
            if (didActionThisTick)
                return;

            List<AIHeroClient> enemiesKnockedUp = EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && a.IsInRange(Yasuo, Program.R.Range) && a.IsKnockedUp()).ToList();
            float timeTilLastEnemyIsPutDown = enemiesKnockedUp.OrderBy(a => a.TimeLeftOnKnockup()).FirstOrDefault().TimeLeftOnKnockup();
            
            if (!force && MenuHandler.GetCheckboxValue(MenuHandler.Ult, "Use R at Last Second") &&
                timeTilLastEnemyIsPutDown <= 0.1f)
                    didActionThisTick = Program.R.Cast();
            else if (force || !MenuHandler.GetCheckboxValue(MenuHandler.Ult, "Use R at Last Second"))
                    didActionThisTick = Program.R.Cast();;
        }

        private static void TryBeyBlade()
        {
            if (didActionThisTick)
                return;

            int beyBladeRange = (int)(Program.E.Range + Program.Flash.Range + (Program.EQ.Range / 2)),
                flashEQRange = (int)(Program.Flash.Range + (Program.EQ.Range / 2));


            //if yasuo has 3rd q ready and everything needed to beyblade
            if (!YasuoCalcs.IsDashing()
                && Yasuo.HasBuff("yasuoq3w")
                && Program.Q.IsReady()
                && Program.E.IsReady()
                && Program.Flash != null
                && Program.Flash.IsReady()
                && Program.R.IsReady())
            {
                //not in EQ range, and is in beyblade range
                List<AIHeroClient> EnemiesInRange = EntityManager.Heroes.Enemies.Where(a =>
                    a.MeetsCriteria() && !a.IsInRange(Yasuo, Program.E.Range + (Program.EQ.Range / 2)) && a.IsInRange(Yasuo, beyBladeRange)).ToList();

                if (EnemiesInRange.Count() != 0)
                {
                    List<Obj_AI_Base> DashableUnits = EntityManager.MinionsAndMonsters.EnemyMinions.Where(a =>
                        //meets criteria
                        a.MeetsCriteria()
                        //if dashing this this unit, it will put is in position to flash range
                        && YasuoCalcs.GetDashingEnd(a).IsInRange(EnemiesInRange.OrderBy(b => YasuoCalcs.GetDashingEnd(a).Distance(b)).FirstOrDefault(), Program.Flash.Range + (Program.EQ.Range / 2))).ToList().ToObj_AI_BaseList();

                    if (DashableUnits.Count() != 0)
                    {
                        Obj_AI_Base dashUnitThatGetsMostChampionsInEQRange = DashableUnits.OrderBy(a =>
                             //get best cast position start
                             Prediction.Position.PredictCircularMissileAoe(EnemiesInRange.ToObj_AI_BaseList().ToArray(), flashEQRange, Program.EQ.Radius, 250, int.MaxValue, YasuoCalcs.GetDashingEnd(a)).OrderBy(prediction => prediction.CollisionObjects.Where(unit => EnemiesInRange.Contains(unit)).Count()).FirstOrDefault().CollisionObjects.Where(unit => EnemiesInRange.Contains(unit)).Count()).FirstOrDefault();

                        if (dashUnitThatGetsMostChampionsInEQRange != null)
                            CastE(dashUnitThatGetsMostChampionsInEQRange);
                    }
                }
            }

            if (Yasuo.HasBuff("yasuoq3w")
                && Program.R.IsReady()
                && YasuoCalcs.IsDashing()
                && Program.Flash.IsReady()
                && Program.Q.IsReady())
            {
                List<AIHeroClient> EnemiesInRange = EntityManager.Heroes.Enemies.Where(a =>
                    a.MeetsCriteria() && a.IsInRange(Yasuo, beyBladeRange)).ToList();

                PredictionResult predictionResult = Prediction.Position.PredictCircularMissileAoe(EnemiesInRange.ToObj_AI_BaseList().ToArray(), flashEQRange, Program.EQ.Radius, 250, int.MaxValue, Yasuo.Position)
                    .OrderBy(prediction => prediction.CollisionObjects.Where(unit => EnemiesInRange.Contains(unit)).Count()).FirstOrDefault();

                if (predictionResult.CastPosition != null && predictionResult.CastPosition != Vector3.Zero && predictionResult.CollisionObjects.Where(a=>EnemiesInRange.Contains(a)).Count() >= 1)
                {
                    CastEQsQ(EnemiesInRange.ToObj_AI_BaseList());
                    if (!predictionResult.CastPosition.IsInRange(Yasuo, Program.Flash.Range) && predictionResult.CastPosition.IsInRange(Yasuo, flashEQRange))
                        didActionThisTick = Program.Flash.Cast(Yasuo.Position.Extend(predictionResult.CastPosition, Program.Flash.Range).To3D());
                    //is in flash range, but is not in EQ range. Might delete this so it stops flashing during every EQ possible...
                    else if (predictionResult.CastPosition.IsInRange(Yasuo, Program.Flash.Range) && !predictionResult.CastPosition.IsInRange(Yasuo, Program.EQ.Range))
                        didActionThisTick = Program.Flash.Cast(predictionResult.CastPosition);
                }
            }
        }
    }
}
