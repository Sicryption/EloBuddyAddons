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
    internal class Program
    {
        public static Spell.Skillshot Q, Q3, EQ, Flash;
        public static Spell.Skillshot W;
        public static Spell.Targeted E, Ignite;
        public static Spell.Active R;
        public static AIHeroClient Yasuo { get { return ObjectManager.Player; } }
        public static int currentPentaKills = 0;
        public static string Animation = "";
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Yasuo")
                return;

            #region SpellSetup
            Q = new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear, 250, int.MaxValue, 55, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
                //SourcePosition = Yasuo.Position + new Vector3(0, 0, 150f),
            };

            Q3 = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 300, 1200, 90, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            W = new Spell.Skillshot(SpellSlot.W, 400, SkillShotType.Cone, 250);
            EQ = new Spell.Skillshot(SpellSlot.Q, 375, SkillShotType.Circular, 0, int.MaxValue, 375, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Targeted(SpellSlot.E, 475, DamageType.Magical)
            {
                CastDelay = 250,
            };
            R = new Spell.Active(SpellSlot.R, 1200, DamageType.Physical)
            {
                CastDelay = 0,
            };
            Ignite = new Spell.Targeted(Yasuo.GetSpellSlotFromName("SummonerDot"), 600, DamageType.True)
            {
                CastDelay = 0,
            };
            Flash = new Spell.Skillshot(Yasuo.GetSpellSlotFromName("SummonerFlash"), 425, SkillShotType.Linear);
            #endregion

            #region Initializers
            MenuHandler.Initialize();
            #endregion

            #region Events
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnTick += Game_OnTick;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Obj_AI_Base.OnPlayAnimation += Obj_AI_Base_OnPlayAnimation;
            #endregion

            #region Variable Setup
            //currentPentaKills = Yasuo.PentaKills;
            #endregion

            Orbwalker.DisableMovement = true;
        }

        private static void Obj_AI_Base_OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            if (sender.IsMe)
                Animation = args.Animation;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Yasuo.IsDead)
                return;
           
            YasuoFunctions.didActionThisTick = false;
            
            YasuoFunctions.AutoHarrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                YasuoFunctions.Flee();
            else if(Orbwalker.ActiveModesFlags != Orbwalker.ActiveModes.None)
            {
                Orbwalker.MoveTo(Game.CursorPos);

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                    YasuoFunctions.Combo();
                if (MenuHandler.GetCheckboxValue(MenuHandler.Killsteal, "Activate Killsteal"))
                    YasuoFunctions.KS();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                    YasuoFunctions.LastHit();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                    YasuoFunctions.Harrass();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                    YasuoFunctions.JungleClear();
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                    YasuoFunctions.LaneClear();
            }

            /*if (Yasuo.PentaKills > currentPentaKills)
            {
                Chat.Print("Nice Penta! Make sure to screenshot it and post it on the UnsignedYasuo thread to show off!");
                
                currentPentaKills = Yasuo.PentaKills;
            }*/
        }
        
        //complete
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Yasuo.IsDead)
                return;

            Menu menu = MenuHandler.Drawing;
            System.Drawing.Color drawColor = System.Drawing.Color.Blue;
            
            //Drawing.DrawText(Yasuo.Position.WorldToScreen(), drawColor, Animation, 15);
            
            //Vector3 yasuoQStartPos = Yasuo.Position + new Vector3(0, 0, 150f),
            //    yasuoQEndPos = yasuoQStartPos.To2D().Extend(Game.CursorPos, Q.Range).To3D((int)yasuoQStartPos.Z - 75);

            //yasuoQStartPos.DrawArrow(yasuoQEndPos, drawColor);
            //Drawing.DrawLine(yasuoQStartPos.WorldToScreen(), yasuoQEndPos.WorldToScreen(), 5, drawColor);

            if (menu.GetCheckboxValue("Draw Q") && !Yasuo.HasBuff("YasuoQ3W") && Q.IsLearned)
            {
                //Drawing.DrawCircle(yasuoQStartPos, Q.Range, drawColor);
                Q.DrawRange(drawColor);
            }
            if (menu.GetCheckboxValue("Draw Q") && Yasuo.HasBuff("YasuoQ3W") && Q.IsLearned)
                Q3.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw W") && W.IsLearned)
                W.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw E") && E.IsLearned)
                E.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw EQ") && E.IsLearned && Q.IsLearned)
                EQ.DrawRange(drawColor);
            if (menu.GetCheckboxValue("Draw R") && R.IsLearned)
                R.DrawRange(drawColor); 
            if (menu.GetCheckboxValue("Draw Beyblade") && R.IsLearned && Flash != null && E.IsLearned && Q.IsLearned)
                Drawing.DrawCircle(Yasuo.Position, E.Range + Flash.Range + (EQ.Range / 2), System.Drawing.Color.Red);
            if (menu.GetCheckboxValue("Draw Turret Range"))
                foreach (Obj_AI_Turret turret in EntityManager.Turrets.Enemies.Where(a => !a.IsDead && a.VisibleOnScreen))
                    turret.DrawCircle((int)turret.GetAutoAttackRange() + 35, drawColor);

            Obj_AI_Base hoverObject = EntityManager.Enemies.Where(a => !a.IsDead && a.IsTargetable && a.IsInRange(Yasuo, E.Range) && a.Distance(Game.CursorPos) <= 75).OrderBy(a => a.Distance(Game.CursorPos)).FirstOrDefault();
            if (hoverObject != null)
            {
                if (menu.GetCheckboxValue("Draw EQ on Target"))
                    Drawing.DrawCircle(YasuoCalcs.GetDashingEnd(hoverObject), EQ.Range, drawColor);
                if (menu.GetCheckboxValue("Draw E End Position on Target"))
                    Drawing.DrawLine(Yasuo.Position.WorldToScreen(), YasuoCalcs.GetDashingEnd(hoverObject).WorldToScreen(), 3, drawColor);
                if (menu.GetCheckboxValue("Draw E End Position on Target - Detailed"))
                {
                    Vector3 startPos = Yasuo.Position,
                        dashEndPos = YasuoCalcs.GetDashingEnd(hoverObject),
                        fakeEndPos = startPos.To2D().Extend(dashEndPos.To2D(), 1000).To3D() + new Vector3(0, 0, startPos.Z),
                        slope = new Vector3(dashEndPos.X - startPos.X, dashEndPos.Y - startPos.Y, 0),
                        fakeSlope = new Vector3(fakeEndPos.X - startPos.X, fakeEndPos.Y - startPos.Y, 0);

                    List<Vector3> pointsAlongPath = new List<Vector3>();
                    List<Vector3> straightLinePath = new List<Vector3>();

                    int points = 100;

                    pointsAlongPath.Add(startPos);
                    
                    //get all points in a line from start to fake end
                    for(int i = 0; i < points; i++)
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

                    for(int i = 0; i < pointsAlongPath.Count() - 1; i++)
                    {
                        System.Drawing.Color color = (pointsAlongPath[i].IsWall()) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                        Drawing.DrawLine(pointsAlongPath[i].WorldToScreen(), pointsAlongPath[i + 1].WorldToScreen(), 2, color);
                    }

                    Vector3 closestWall = pointsAlongPath.Where(a => a.IsWall()).OrderBy(a => a.Distance(dashEndPos)).FirstOrDefault(),
                        closestWallsEndPosition = (pointsAlongPath.IndexOf(closestWall) + 1 == pointsAlongPath.Count) ? Vector3.Zero : pointsAlongPath[pointsAlongPath.IndexOf(closestWall) + 1];

                    Drawing.DrawText(closestWall.WorldToScreen(), drawColor, "start", 15);
                    Drawing.DrawText(closestWallsEndPosition.WorldToScreen(), drawColor, "end", 15);
                    Drawing.DrawText(((closestWall + closestWallsEndPosition) / 2).WorldToScreen(), drawColor, closestWall.Distance(closestWallsEndPosition).ToString(), 15);
                    Drawing.DrawText(dashEndPos.WorldToScreen(), drawColor, startPos.Distance(closestWallsEndPosition).ToString(), 15);

                    //none of the points are a wall so the end point is the dash position
                    if (!pointsAlongPath.Any(a => a.IsWall()))
                        Drawing.DrawCircle(dashEndPos, 50, drawColor);
                    // OR none of the walls are in the E range
                    else if (pointsAlongPath.Where(a => a.IsWall()).OrderBy(a => a.Distance(startPos)).FirstOrDefault() != null &&
                        pointsAlongPath.Where(a => a.IsWall()).OrderBy(a => a.Distance(startPos)).FirstOrDefault().Distance(startPos) > E.Range)
                        Drawing.DrawCircle(dashEndPos, 50, drawColor);
                    //or the dashing end is not a wall
                    else if (!dashEndPos.IsWall())
                        Drawing.DrawCircle(dashEndPos, 50, drawColor);
                    //find the nearest wall to the dash position
                    else if (closestWall != Vector3.Zero && closestWallsEndPosition != Vector3.Zero &&
                        closestWall != null && closestWallsEndPosition != null &&
                        closestWallsEndPosition.Distance(dashEndPos) < closestWall.Distance(dashEndPos) &&
                        startPos.Distance(closestWallsEndPosition) <= 630)
                        Drawing.DrawCircle(closestWallsEndPosition, 50, drawColor);
                    //the end position is the first wall
                    else
                        Drawing.DrawCircle(pointsAlongPath.First(a => a.IsWall()), 50, drawColor);
                }
            }

            if (menu.GetCheckboxValue("Draw Wall Dashes") && E.IsLearned)
                foreach (WallDash wd in YasuoWallDashDatabase.wallDashDatabase.Where(a=>a.startPosition.Distance(Yasuo) <= 1300))
                    if (EntityManager.MinionsAndMonsters.Combined.Where(a => a.MeetsCriteria() && a.VisibleOnScreen && a.Name == wd.unitName && a.ServerPosition.Distance(wd.dashUnitPosition) <= 2).FirstOrDefault() != null)
                    {
                        wd.startPosition.DrawArrow(wd.endPosition, System.Drawing.Color.Red, 1);
                        Geometry.Polygon.Circle dashCircle = new Geometry.Polygon.Circle(wd.endPosition, 120);
                        dashCircle.Draw(System.Drawing.Color.Red, 1);
                    }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Yasuo.IsDead)
                return;

            if (MenuHandler.Drawing.GetCheckboxValue("Draw Combo Damage"))
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && a.VisibleOnScreen))
                {
                    int hpBarWidth = 96;
                    float enemyHPPercentAfterCombo = Math.Max((100 * ((enemy.Health - enemy.ComboDamage()) / enemy.MaxHealth)), 0);
                    //Vector2 FriendlyHPBarOffset = new Vector2(26, 3);
                    Vector2 EnemyHPBarOffset = new Vector2(2, 9.5f);
                    Vector2 CurrentHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(100 * enemy.HealthPercent / hpBarWidth, 0);
                    Vector2 EndHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(enemyHPPercentAfterCombo, 0);
                    if (enemyHPPercentAfterCombo == 0)
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Green);
                    else
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Yellow);
                }
        }

        private static void DrawLineIfWallBetween(Vector3 startPos, Obj_AI_Base target)
         {
             Vector3 endPos = YasuoCalcs.GetDashingEnd(target);
 
             List<Vector3> inbetweenPoints = new List<Vector3>();
             Vector2 wallStartPosition = Vector2.Zero;
             Vector2 wallEndPosition = Vector2.Zero;
 
             //get every point between yasuo's position and the end position of the dash extended to a range of 1000. 
             //1 point is every 1/100 of total length
             for (int i = 0; i <= 100; i++)
                 inbetweenPoints.Add(startPos.Extend(startPos.Extend(endPos, 1000), i* (startPos.Distance(startPos.Extend(endPos, 1000)) / 100)).To3D());
 
             //for every point in the list of points, find the beginning and the end of the wal
             foreach (Vector2 vec in inbetweenPoints)
              {
                 if (vec.IsWall())
                 {
                     if (wallStartPosition == Vector2.Zero)
                         wallStartPosition = vec;
                 }
                 else if (wallEndPosition == Vector2.Zero && wallStartPosition != Vector2.Zero)
                     wallEndPosition = vec;
             }

            //draw the wall in the color blue
            if (wallStartPosition != Vector2.Zero && wallEndPosition != Vector2.Zero)
            {
                 double wallWidth = Math.Round(wallStartPosition.Distance(wallEndPosition)),
                    distanceToWall = Math.Round(startPos.Distance(wallStartPosition)),
                    totalDistance = Math.Round(wallStartPosition.Distance(wallEndPosition) + startPos.Distance(wallStartPosition)),
                    monsterDist = Math.Round(target.Position.Distance(wallStartPosition));

                Drawing.DrawLine(wallStartPosition.To3D().WorldToScreen(), wallEndPosition.To3D().WorldToScreen(), 10, System.Drawing.Color.Black);

                //if the end point of yasuos dash brings him at least halfway between the two points (closer to the wall end than to the walls beginning)
                //and the wall has to be thinner than yasuo's total dash range. TESTED THIS TO CONFIRM IT WORKS
                //if (endPos.Distance(wallEndPosition) < endPos.Distance(wallStartPosition) && wallStartPosition.Distance(wallEndPosition) <= Program.E.Range)
                if (totalDistance <= 630)
                    Drawing.DrawLine(startPos.WorldToScreen(), startPos.Extend(endPos, 1000).To3D().WorldToScreen(), 3, System.Drawing.Color.Green);
                else
                    Drawing.DrawLine(startPos.WorldToScreen(), startPos.Extend(endPos, 1000).To3D().WorldToScreen(), 3, System.Drawing.Color.Red);
                Drawing.DrawText(wallStartPosition.To3D().WorldToScreen(), System.Drawing.Color.Purple, wallStartPosition.Distance(wallEndPosition).ToString(), 15);
                Drawing.DrawText(startPos.Extend(endPos, 1000).To3D().WorldToScreen(), System.Drawing.Color.Purple, (wallStartPosition.Distance(wallEndPosition) + startPos.Distance(wallStartPosition)).ToString(), 15);
                Drawing.DrawCircle(endPos, 50, System.Drawing.Color.White);
            }
         }
    }
}
