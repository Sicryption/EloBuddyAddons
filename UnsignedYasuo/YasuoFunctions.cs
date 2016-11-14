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
            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.LastHit, "Use E"))
                didActionThisTick = CastE(EntityManager.Enemies, true, MenuHandler.GetCheckboxValue(MenuHandler.LastHit, "Use E Under Tower"));

            if (!didActionThisTick && 
                (MenuHandler.GetCheckboxValue(MenuHandler.LastHit, "Use Q") && !Yasuo.HasBuff("YasuoQ3W")) 
                || (MenuHandler.GetCheckboxValue(MenuHandler.LastHit, "Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                didActionThisTick = CastQ(EntityManager.Enemies, true);

            if (MenuHandler.GetCheckboxValue(MenuHandler.LastHit, "Use EQ"))
                didActionThisTick = CastEQ(EntityManager.Enemies, true, MenuHandler.GetCheckboxValue(MenuHandler.LastHit, "Use E Under Tower"));
            
            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.LastHit, "Use Items"))
                didActionThisTick = CastItems(EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList(), true);
        }

        //complete
        public static void LaneClear()
        {
            if (!didActionThisTick && 
                (MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                didActionThisTick = CastQ(EntityManager.Enemies, false);

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use E"))
                didActionThisTick = CastE(EntityManager.Enemies, MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use E only for Last Hit"), MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use E Under Tower"));

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use EQ"))
                didActionThisTick = CastEQ(EntityManager.Enemies, false, MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use E Under Tower"));

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.LaneClear, "Use Items"))
                didActionThisTick = CastItems(EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList(), false);
        }

        public static void JungleClear()
        {
            if (!didActionThisTick &&
                (MenuHandler.GetCheckboxValue(MenuHandler.JungleClear, "Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (MenuHandler.GetCheckboxValue(MenuHandler.JungleClear, "Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                didActionThisTick = CastQ(EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList(), false);

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.JungleClear, "Use E"))
                didActionThisTick = CastE(EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList(), MenuHandler.GetCheckboxValue(MenuHandler.JungleClear, "Use E only for Last Hit"), true);

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.JungleClear, "Use EQ"))
                didActionThisTick = CastEQ(EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList(), false, true);

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.JungleClear, "Use Items"))
                didActionThisTick = CastItems(EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList(), false);
        }

        //complete
        public static void KS()
        {
            if (!didActionThisTick && 
                (MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                 || (MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                didActionThisTick = CastQ(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), true);

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use E"))
                didActionThisTick = CastE(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), true, MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use E Under Tower"));

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use EQ"))
                didActionThisTick = CastEQ(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), true, MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use E Under Tower"));

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use Ignite"))
                didActionThisTick = CastIgnite(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), true);

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Use Items"))
                didActionThisTick = CastItems(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), true);
        }

        //complete
        public static void Harrass()
        {
            if (!didActionThisTick && 
                (MenuHandler.GetCheckboxValue(MenuHandler.Harass, "Use Q") && !Yasuo.HasBuff("YasuoQ3W")) 
                || (MenuHandler.GetCheckboxValue(MenuHandler.Harass, "Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                didActionThisTick = CastQ(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false);

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.Harass, "Use E"))
                didActionThisTick = CastE(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false, MenuHandler.GetCheckboxValue(MenuHandler.Harass, "Use E Under Tower"));
            
            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.Harass, "Use EQ"))
                didActionThisTick = CastEQ(EntityManager.Enemies.ToList(), false, MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use E Under Tower"), EntityManager.Heroes.Enemies.ToObj_AI_BaseList());

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Harass, "Use Items"))
                didActionThisTick = CastItems(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false);

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Harass, "Use R"))
                didActionThisTick = UltHandler();
        }

        //complete
        public static void Combo()
        {
            if (!didActionThisTick && 
                (MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                didActionThisTick = CastQ(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false);

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use E"))
                didActionThisTick = CastE(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false, MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use E Under Tower"));
            
            if (!didActionThisTick &&
                MenuHandler.GetComboBoxText(MenuHandler.Combo, "Dash Mode: ") == "Gapclose")
                didActionThisTick = EGapClose(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use E Under Tower"));

            if (!didActionThisTick &&
                MenuHandler.GetComboBoxText(MenuHandler.Combo, "Dash Mode: ") == "To Mouse")
                didActionThisTick = EToMouse(MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use E Under Tower"), 
                    false, MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use EQ"), EntityManager.Heroes.Enemies.ToObj_AI_BaseList());

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use EQ"))
                didActionThisTick = CastEQ(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false, MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use E Under Tower"));

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use Items"))
                didActionThisTick = CastItems(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false);

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Use R"))
                didActionThisTick = UltHandler();

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Combo, "Beyblade"))
                didActionThisTick = TryBeyBlade();
        }

        //complete
        public static void Flee()
        {
            WallDash activeDash = null;

            if (MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Wall Dash") && Program.E.IsReady() && !YasuoCalcs.IsDashing())
            {
                //walldash
                foreach (WallDash wd in YasuoWallDashDatabase.wallDashDatabase.Where(a=>a.startPosition.Distance(Yasuo) <= 1300))
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

            if (MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Use E") || activeDash != null)
            {
                if (activeDash == null)
                {
                    Orbwalker.MoveTo(Game.CursorPos);
                    didActionThisTick = EToMouse(MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Use E Under Tower"), MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Stack Q"), false);
                }
                else
                {
                    //first check if the positions are exact
                    if (Yasuo.Position.To2D() == activeDash.startPosition.To2D())
                        didActionThisTick = CastE(EntityManager.MinionsAndMonsters.Combined.Where(a => a.Name == activeDash.unitName).ToList().ToObj_AI_BaseList(), false, MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Use E Under Tower"));
                    else
                        Orbwalker.MoveTo(activeDash.startPosition);

                    //if the positions aren't exact
                    //if (Yasuo.Position.Distance(activeDash.startPosition) > 50)
                    //    return;

                    Vector3 startPos = Yasuo.Position,
                        dashEndPos = YasuoCalcs.GetDashingEnd(EntityManager.MinionsAndMonsters.Combined.Where(a=>a.MeetsCriteria() && YasuoCalcs.ERequirements(a, MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Use E Under Tower")) && a.Name == activeDash.unitName).FirstOrDefault()),
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
                    if (actualDashPosition.Distance(activeDash.endPosition) <= MenuHandler.GetSliderValue(MenuHandler.Flee, "Wall Dash Extra Space"))
                    {
                        Chat.Print("did the dash");
                        didActionThisTick = Program.E.Cast(EntityManager.MinionsAndMonsters.Combined.Where(a => a.MeetsCriteria() && YasuoCalcs.ERequirements(a, MenuHandler.GetCheckboxValue(MenuHandler.Flee, "Use E Under Tower")) && a.Name == activeDash.unitName).FirstOrDefault());
                    }
                    else
                        Chat.Print("did not do the dash");
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

            if (!didActionThisTick && 
                (MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use Q") && !Yasuo.HasBuff("YasuoQ3W"))
                || (MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use Q3") && Yasuo.HasBuff("YasuoQ3W")))
                didActionThisTick = CastQ(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false);

            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use E"))
                didActionThisTick = CastE(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false, MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use E Under Tower"));

            //dashes to minions and EQ's if it hits the enemies
            if (!didActionThisTick && 
                MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use EQ"))
                didActionThisTick = CastEQ(EntityManager.Enemies.ToList(), false, MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use E Under Tower"), EntityManager.Heroes.Enemies.ToObj_AI_BaseList());

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.AutoHarass, "Use Items"))
                didActionThisTick = CastItems(EntityManager.Heroes.Enemies.ToObj_AI_BaseList(), false);

        }

        //Items need testing. Should work.
        public static bool CastItems(List<Obj_AI_Base> enemies, bool ks)
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
                && (!ks || enemies.Where(a=> a.MeetsCriteria() && a.Health <= DamageLibrary.GetItemDamage(Yasuo, a, ItemId.Ravenous_Hydra)).FirstOrDefault() != null))
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
                didActionThisTick = HextechGunblade.Cast(enemies.OrderBy(a=>a.Health).FirstOrDefault());
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

            return false;
        }

        public static bool CastE(List<Obj_AI_Base> enemies, bool ks, bool goUnderEnemyTower)
        {
            if (!Program.E.IsReady() || YasuoCalcs.IsDashing() || !enemies.Any(a=>a.IsInRange(Yasuo, Program.E.Range)))
                return false;

            Obj_AI_Base unit = enemies.Where(a =>
            a.IsInRange(Yasuo, Program.E.Range) 
            && (!ks || YasuoCalcs.E(a) >= a.Health)
            && a.MeetsCriteria()
            && YasuoCalcs.ERequirements(a, goUnderEnemyTower)
            ).FirstOrDefault();

            if (unit != null)
                return CastE(unit);
            return false;
        }
        public static bool CastIgnite(List<Obj_AI_Base> enemies, bool ks)
        {
            if (!Program.Ignite.IsReady() || !enemies.Any(a => a.IsInRange(Yasuo, Program.Ignite.Range)))
                return false;

            Obj_AI_Base unit = enemies.Where(a =>
                a.IsInRange(Yasuo, Program.Ignite.Range)
                && (!ks || YasuoCalcs.Ignite(a) >= a.Health)
                && a.MeetsCriteria()).FirstOrDefault();

            if (unit != null)
                return Program.Ignite.Cast(unit);
            return false;
        }
        
        public static bool CastE(Obj_AI_Base unit)
        {
            if (!Program.E.IsReady() || YasuoCalcs.IsDashing() || unit.IsInRange(Yasuo, Program.E.Range))
                return false;

            if (unit != null)
                return Program.E.Cast(unit);
            return false;
        }

        public static bool EGapClose(List<Obj_AI_Base> targets, bool goUnderEnemyTower)
        {
            if (!Program.E.IsReady() || YasuoCalcs.IsDashing())
                return false;
            //if none of the targets are in auto attack range
            if (targets.Where(a => a.IsInRange(Yasuo, Yasuo.GetAutoAttackRange())).FirstOrDefault() == null)
            {
                //get the closest target
                Obj_AI_Base closestEnemy = targets.Where(a=> a.MeetsCriteria() && a.IsInRange(Yasuo, 5000)).OrderBy(b => b.Distance(Yasuo)).FirstOrDefault();
                
                if (closestEnemy != null)
                {
                    //get all enemies in my E range
                    List<Obj_AI_Base> enemiesInERange = EntityManager.Enemies.Where(a => a.MeetsCriteria() && YasuoCalcs.ERequirements(a, goUnderEnemyTower) && a.IsInRange(Yasuo, Program.E.Range)).ToList();

                    Obj_AI_Base enemyToDashTo = enemiesInERange.OrderBy(a => YasuoCalcs.GetDashingEnd(a).Distance(closestEnemy)).FirstOrDefault();

                    if (enemyToDashTo != null && YasuoCalcs.GetDashingEnd(enemyToDashTo).Distance(closestEnemy) < Yasuo.Distance(closestEnemy))
                        return CastE(enemyToDashTo);
                }
            }
            return false;
        }

        public static bool EToMouse(bool goUnderEnemyTower, bool stackQ, bool EQ, List<Obj_AI_Base> EQTargets = null)
        {
            if (Program.E.IsReady() && !YasuoCalcs.IsDashing())
            {
                Geometry.Polygon.Sector sector = new Geometry.Polygon.Sector(Yasuo.Position, Game.CursorPos, (float)(30 * Math.PI / 180), Program.E.Range);

                List<Obj_AI_Base> dashableEnemies = EntityManager.Enemies.Where(a => !a.IsDead && a.MeetsCriteria() && YasuoCalcs.ERequirements(a, goUnderEnemyTower) && a.IsInRange(Yasuo.Position, Program.E.Range) && sector.IsInside(a)).OrderBy(a => YasuoCalcs.GetDashingEnd(a).Distance(Game.CursorPos)).ToList();
                List<Obj_AI_Base> dashableEnemiesWithTargets = dashableEnemies.Where(a => YasuoCalcs.GetDashingEnd(a).CountEnemyHeroesInRangeWithPrediction((int)Program.EQ.Range, 250) >= 1)
                    .OrderBy(a => YasuoCalcs.GetDashingEnd(a).CountEnemyHeroesInRangeWithPrediction((int)Program.EQ.Range, 250)).ToList();

                if (YasuoCalcs.WillQBeReady() && stackQ && !Yasuo.HasBuff("yasuoq3w") && dashableEnemies.Count != 0)
                    return CastEQ(dashableEnemies, false, goUnderEnemyTower);
                else if (YasuoCalcs.WillQBeReady() && EQ && dashableEnemiesWithTargets.Count != 0)
                    return CastEQ(dashableEnemies, false, goUnderEnemyTower, EQTargets);
                else
                    return CastE(dashableEnemies.FirstOrDefault());
            }
            return false;
        }

        public static bool CastEQ(List<Obj_AI_Base> dashEnemies, bool ks, bool goUnderEnemyTower, List<Obj_AI_Base> EQEnemies = null)
        {
            if (!Program.E.IsReady() || !YasuoCalcs.WillQBeReady() || 
                !dashEnemies.Any(a => a.IsInRange(Yasuo, Program.E.Range)) ||
                Yasuo.GetNearbyEnemies(Program.E.Range).Count() == 0 || YasuoCalcs.IsDashing())
                return false;

            if (EQEnemies == null)
                EQEnemies = dashEnemies;

            int numberOfEnemiesHitWithEQ = 0;
            Obj_AI_Base unitToDashTo = null;

            List<Obj_AI_Base> possibleDashUnits = dashEnemies.Where(a => a.MeetsCriteria() && a.IsInRange(Yasuo, Program.E.Range) && YasuoCalcs.ERequirements(a, goUnderEnemyTower) && a.Health > YasuoCalcs.E(a)).ToList();
            foreach(Obj_AI_Base possibleDashUnit in possibleDashUnits)
            {
                List<Obj_AI_Base> unitsHitWithEQ = EQEnemies.Where(a => a.MeetsCriteria() && a.IsInRange(YasuoCalcs.GetDashingEnd(possibleDashUnit), Program.EQ.Range)).ToList();
                if (ks)
                    unitsHitWithEQ = unitsHitWithEQ.Where(a => a.Health <= YasuoCalcs.Q(a) || (a.Name == possibleDashUnit.Name && a.Health <= YasuoCalcs.Q(a) + YasuoCalcs.E(a))).ToList();

                if(numberOfEnemiesHitWithEQ < unitsHitWithEQ.Count())
                {
                    numberOfEnemiesHitWithEQ = unitsHitWithEQ.Count();
                    unitToDashTo = possibleDashUnit;
                }
            }

            if(unitToDashTo != null && numberOfEnemiesHitWithEQ >= 1)
            {   
                CastE(unitToDashTo);
                if(YasuoCalcs.GetQReadyTimeInt() == 0)
                    CastEQsQ();
                else
                    Core.DelayAction(delegate { CastEQsQ(); }, YasuoCalcs.GetQReadyTimeInt());
                return true;
            }
            return false;
        }

        public static bool CastEQsQ()
        {
            return Program.Q.Cast(Yasuo.Position + new Vector3(50, 0, 0));
        }

        public static bool CastQ(List<Obj_AI_Base> enemies, bool ks)
        {
            if (!Program.Q.IsReady() || YasuoCalcs.IsDashing() || 
                !enemies.Any(a => a.IsInRange(Yasuo, Program.Q.Range)) ||
                (Orbwalker.CanAutoAttack && enemies.Where(a=>a.IsInRange(Yasuo, Yasuo.GetAutoAttackRange())).FirstOrDefault() != null)
                || Orbwalker.IsAutoAttacking)
                return false;

            Spell.Skillshot.BestPosition position;
            if (Yasuo.HasBuff("YasuoQ3W"))
                position = Program.Q3.GetBestLinearCastPosition(enemies.Where(a => a.MeetsCriteria() && (!ks || YasuoCalcs.Q(a) >= a.Health) && a.IsInRange(Yasuo, Program.Q3.Range)));
            else
                position = Program.Q.GetBestLinearCastPosition(enemies.Where(a => a.MeetsCriteria() && (!ks || YasuoCalcs.Q(a) >= a.Health) && a.IsInRange(Yasuo, Program.Q.Range)));

            if (position.CastPosition != null && position.CastPosition != Vector3.Zero && position.HitNumber != 0)
            {
                if (Yasuo.HasBuff("YasuoQ3W"))
                    return Program.Q3.Cast(position.CastPosition);
                else
                    return Program.Q.Cast(position.CastPosition);
            }
            return false;
        }
        
        public static bool UltHandler()
        {
            if (!Program.R.IsReady())
                return false;

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Ult, "Use R for Flow") &&
                Yasuo.Mana != 100)
                return CastR();

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Ult, "Use R on All Enemies in Range") &&
                EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria() && a.IsInRange(Yasuo, Program.R.Range) && a.IsKnockedUp()).Count() == Yasuo.CountEnemyHeroesInRangeWithPrediction(2000, 0))
                return CastR();

            if (!didActionThisTick &&
                MenuHandler.GetCheckboxValue(MenuHandler.Ult, "Use R at 10% HP") &&
                Yasuo.HealthPercent <= 0.10f &&
                Yasuo.Mana != 100)
                return CastR(true);

            if (!didActionThisTick &&
                EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria() && a.IsInRange(Yasuo, Program.R.Range) && a.IsKnockedUp()).Count() >= MenuHandler.GetSliderValue(MenuHandler.Ult, "Use R on x Enemies or more:"))
                return CastR(true);

            return false;
        }

        public static bool CastR(bool force = false)
        {
            List<AIHeroClient> enemiesKnockedUp = EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && a.IsInRange(Yasuo, Program.R.Range) && a.IsKnockedUp()).ToList();
            float timeTilLastEnemyIsPutDown = enemiesKnockedUp.OrderBy(a => a.TimeLeftOnKnockup()).FirstOrDefault().TimeLeftOnKnockup();
            
            if (!force && MenuHandler.GetCheckboxValue(MenuHandler.Ult, "Use R at Last Second") &&
                timeTilLastEnemyIsPutDown <= 0.1f)
                    return Program.R.Cast();
            else if (force || !MenuHandler.GetCheckboxValue(MenuHandler.Ult, "Use R at Last Second"))
                    return Program.R.Cast();

            return false;
        }

        private static bool TryBeyBlade()
        {
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
                            return CastE(dashUnitThatGetsMostChampionsInEQRange);
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
                    CastEQsQ();
                    if (!predictionResult.CastPosition.IsInRange(Yasuo, Program.Flash.Range) && predictionResult.CastPosition.IsInRange(Yasuo, flashEQRange))
                        return Program.Flash.Cast(Yasuo.Position.Extend(predictionResult.CastPosition, Program.Flash.Range).To3D());
                    //is in flash range, but is not in EQ range. Might delete this so it stops flashing during every EQ possible...
                    else if (predictionResult.CastPosition.IsInRange(Yasuo, Program.Flash.Range) && !predictionResult.CastPosition.IsInRange(Yasuo, Program.EQ.Range))
                        return Program.Flash.Cast(predictionResult.CastPosition);
                }
            }

            return false;
        }
    }
}
