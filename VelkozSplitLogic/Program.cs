using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using SharpDX;
using System.Linq;

namespace VelkozSplitLogic
{
    internal class Program
    {
        public static Menu menu;
        public static Spell.Skillshot Q;
        public static AIHeroClient vel;
        public static MissileClient activeQ;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Velkoz")
                return;
            vel = Player.Instance;

            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 1300, 50, DamageType.Magical)
            {
                AllowedCollisionCount = 1,
            };

            menu = MainMenu.AddMenu("Velkoz Split Logic", "VelkozSplitLogic");

            Game.OnTick += Game_OnTick;
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            //Drawing.on
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            Game_OnTick(args);
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender == null)
                return;

            MissileClient projectile = sender as MissileClient;

            if (projectile == null || projectile.SpellCaster == null || projectile.SData == null || projectile.SpellCaster.Type != GameObjectType.AIHeroClient)
                return;

            if (projectile.SpellCaster.IsMe && projectile.SData.Name == "VelkozQMissile")
            {
                activeQ = null;
                startPos = Vector2.Zero;
                LeftPos = Vector2.Zero;
                RightPos = Vector2.Zero;
                QTarget = null;
                //Console.WriteLine("Q Missile Destroyed");
            }
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender == null)
                return;

            MissileClient projectile = sender as MissileClient;

            if (projectile == null || projectile.SpellCaster == null || projectile.SData == null || projectile.SpellCaster.Type != GameObjectType.AIHeroClient)
                return;

            if (projectile.SpellCaster.IsMe && projectile.SData.Name == "VelkozQMissile")
            {
                activeQ = projectile;
                //Console.WriteLine("Q Missile Created");
            }
        }
        
        public static int numQDivideTimes = 5;

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (QTarget == null)
                return;

            if(startPos != Vector2.Zero && LeftPos != Vector2.Zero)
                Drawing.DrawLine(startPos.To3D().WorldToScreen(), LeftPos.To3D().WorldToScreen(), 2, System.Drawing.Color.White);
            if (startPos != Vector2.Zero && RightPos != Vector2.Zero)
                Drawing.DrawLine(startPos.To3D().WorldToScreen(), RightPos.To3D().WorldToScreen(), 2, System.Drawing.Color.White);

            /*Vector2 startPosition = vel.Position.To2D() + new Vector2(0, 0f);
            
            for (int angle = 0; angle < 360; angle += 15)
            {
                for (int i = 1; i <= numQDivideTimes; i++)
                {
                    Obj_AI_Base enemyHitWithInitialQ = null,
                        enemyHitWithLeftQSplit = null,
                        enemyHitWithRightQSplit = null;

                    Vector2 Q1ExtendingPosition = startPosition + new Vector2(0, (Q.Range / numQDivideTimes) * i),
                        RotatedPosition = Q1ExtendingPosition.RotateAroundPoint(startPosition, (float)(angle * Math.PI / 180)),
                        EndOfQPosition = getEndOfMissilePosition(startPosition, RotatedPosition, i, out enemyHitWithInitialQ);

                    //if the linear spell hits the target, don't check branching
                    if (enemyHitWithInitialQ == target)
                    {
                        Drawing.DrawLine(startPosition.To3D().WorldToScreen(), EndOfQPosition.To3D().WorldToScreen(), 2, System.Drawing.Color.White);

                        //don't check the next 5 points in the Q range (based off the dividend)
                        i += numQDivideTimes;
                    }
                    //if the linear spell doesn't hit the target, find the perp angles and calculate them.
                    else
                    {
                        Vector2 PerpendicularPos1 = EndOfQPosition.Perpendicular(),
                            PerpendicularPos2 = startPosition.Perpendicular(),
                            temp = new Vector2(EndOfQPosition.X - PerpendicularPos1.X, EndOfQPosition.Y - PerpendicularPos1.Y),
                            PerpendicularPos3 = EndOfQPosition.Perpendicular2(),
                            PerpendicularPos4 = startPosition.Perpendicular2(),
                            temp2 = new Vector2(EndOfQPosition.X - PerpendicularPos3.X, EndOfQPosition.Y - PerpendicularPos3.Y),
                            QLeftSplitPos = PerpendicularPos2 + temp,
                            QRightSplitPos = PerpendicularPos4 + temp2;

                        QLeftSplitPos = getEndOfMissilePosition(EndOfQPosition, QLeftSplitPos, target, out enemyHitWithLeftQSplit);
                        QRightSplitPos = getEndOfMissilePosition(EndOfQPosition, QRightSplitPos, target, out enemyHitWithRightQSplit);
                        
                        //draw the point to cast to and the point to branch at for left side
                        if (enemyHitWithLeftQSplit == target)
                        {
                            Drawing.DrawLine(EndOfQPosition.To3D().WorldToScreen(), QLeftSplitPos.To3D().WorldToScreen(), 2, System.Drawing.Color.White);
                            Drawing.DrawLine(startPosition.To3D().WorldToScreen(), EndOfQPosition.To3D().WorldToScreen(), 2, System.Drawing.Color.White);
                        }
                        //draw the point to cast to and the point to branch at for right side
                        else if (enemyHitWithRightQSplit == target)
                        {
                            Drawing.DrawLine(EndOfQPosition.To3D().WorldToScreen(), QRightSplitPos.To3D().WorldToScreen(), 2, System.Drawing.Color.White);
                            Drawing.DrawLine(startPosition.To3D().WorldToScreen(), EndOfQPosition.To3D().WorldToScreen(), 2, System.Drawing.Color.White);
                        }
                    }
                }
            }*/
        }

        private static Vector2 getEndOfMissilePosition(Vector2 basePosition, Vector2 endPosition, int dividend, out Obj_AI_Base enemyHit, Obj_AI_Base ignoreCollisionWithThisUnit = null)
        {
            List<Obj_AI_Base> enemiesHitByQ = new List<Obj_AI_Base>();
            foreach (Obj_AI_Base enemy in EntityManager.Enemies.Where(a=>!a.IsDead && a.Distance(vel) <= Q.Range * 2))
                if ((ignoreCollisionWithThisUnit == null || enemy.Name != ignoreCollisionWithThisUnit.Name) && Prediction.Position.Collision.LinearMissileCollision(enemy, basePosition, endPosition, Q.Speed, Q.Width, Q.CastDelay))
                    enemiesHitByQ.Add(enemy);

            if (enemiesHitByQ.Count == 0)
            {
                enemyHit = null;
                return basePosition.Extend(endPosition, (Q.Range / numQDivideTimes) * dividend);
            }
            else
            {
                enemyHit = enemiesHitByQ.OrderByDescending(a => a.Distance(basePosition)).FirstOrDefault();
                return endPosition;//basePosition.Extend(endPosition, enemiesHitByQ.OrderByDescending(a => a.Distance(basePosition)).FirstOrDefault().Distance(basePosition));
            }
        }

        private static Vector2 getEndOfMissilePosition(Vector2 basePosition, Vector2 endPosition, Obj_AI_Base target, out Obj_AI_Base enemyHit)
        {
            if (Prediction.Position.Collision.LinearMissileCollision(target, basePosition, endPosition, Q.Speed, Q.Width, Q.CastDelay))
            {
                enemyHit = target;
                return endPosition;
            }
            else
            {
                enemyHit = null;
                return basePosition.Extend(endPosition, Q.Range);
            }
        }

        public static Vector2 startPos,
            LeftPos,
            RightPos;
        public static Obj_AI_Base QTarget = null;

        private static void Game_OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (Q.Name == "VelkozQ")
                {
                    //1555 is the range a perpendicular q can go
                    Obj_AI_Base target = EntityManager.Heroes.Enemies.Where(a => !a.IsDead && a.Distance(vel) <= 1555).FirstOrDefault();
                    QTarget = target;

                    if (target != null)
                    {
                        Vector2 startPosition = vel.Position.To2D() + new Vector2(0, 0f);

                        for (int angle = 0; angle < 360; angle += 15)
                        {
                            if (!Q.IsReady())
                                break;

                            for (int i = 1; i <= numQDivideTimes; i++)
                            {
                                if (!Q.IsReady())
                                    break;

                                Obj_AI_Base enemyHitWithInitialQ = null,
                                    enemyHitWithLeftQSplit = null,
                                    enemyHitWithRightQSplit = null;

                                Vector2 Q1ExtendingPosition = startPosition + new Vector2(0, (Q.Range / numQDivideTimes) * i),
                                    RotatedPosition = Q1ExtendingPosition.RotateAroundPoint(startPosition, (float)(angle * Math.PI / 180)),
                                    EndOfQPosition = getEndOfMissilePosition(startPosition, RotatedPosition, i, out enemyHitWithInitialQ);

                                //if the linear spell hits the target, don't check branching
                                if (enemyHitWithInitialQ == target)
                                {
                                    Vector3 pos = EndOfQPosition.To3D();
                                    if (pos != null && pos != startPos.To3D() && pos != Vector3.Zero)
                                    {
                                        //Console.WriteLine("used it here :/");
                                        Q.Cast(pos);
                                    }

                                    //don't check the next 5 points in the Q range (based off the dividend)
                                    i += numQDivideTimes;
                                }
                                //if the linear spell doesn't hit the target, find the perp angles and calculate them.
                                else
                                {
                                    Vector2 PerpendicularPos1 = EndOfQPosition.Perpendicular(),
                                        PerpendicularPos2 = startPosition.Perpendicular(),
                                        temp = new Vector2(EndOfQPosition.X - PerpendicularPos1.X, EndOfQPosition.Y - PerpendicularPos1.Y),
                                        PerpendicularPos3 = EndOfQPosition.Perpendicular2(),
                                        PerpendicularPos4 = startPosition.Perpendicular2(),
                                        temp2 = new Vector2(EndOfQPosition.X - PerpendicularPos3.X, EndOfQPosition.Y - PerpendicularPos3.Y),
                                        QLeftSplitPos = PerpendicularPos2 + temp,
                                        QRightSplitPos = PerpendicularPos4 + temp2;

                                    QLeftSplitPos = getEndOfMissilePosition(EndOfQPosition, QLeftSplitPos, target, out enemyHitWithLeftQSplit);
                                    QRightSplitPos = getEndOfMissilePosition(EndOfQPosition, QRightSplitPos, target, out enemyHitWithRightQSplit);

                                    //draw the point to cast to and the point to branch at for left side
                                    if (enemyHitWithLeftQSplit == target || enemyHitWithRightQSplit == target)
                                    {
                                        //Console.WriteLine("used it here");
                                        Vector3 pos = EndOfQPosition.To3D();
                                        if (pos != null && pos != startPos.To3D() && pos != Vector3.Zero)
                                            Q.Cast(pos);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if(activeQ != null && Q.Name == "VelkozQSplitActivate")
            {
                TrySplitQ(QTarget);
            }
        }

        public static void TrySplitQ(Obj_AI_Base target)
        {
            if (target == null)
                return;
            
            Obj_AI_Base enemyHitWithLeftQSplit = null,
                    enemyHitWithRightQSplit = null;

            Vector2 PerpendicularPos1 = activeQ.Position.To2D().Perpendicular(),
                        PerpendicularPos2 = activeQ.StartPosition.To2D().Perpendicular(),
                        temp = new Vector2(activeQ.Position.To2D().X - PerpendicularPos1.X, activeQ.Position.To2D().Y - PerpendicularPos1.Y),
                        PerpendicularPos3 = activeQ.Position.To2D().Perpendicular2(),
                        PerpendicularPos4 = activeQ.StartPosition.To2D().Perpendicular2(),
                        temp2 = new Vector2(activeQ.Position.To2D().X - PerpendicularPos3.X, activeQ.Position.To2D().Y - PerpendicularPos3.Y),
                        QLeftSplitPos = PerpendicularPos2 + temp,
                        QRightSplitPos = PerpendicularPos4 + temp2;

            QLeftSplitPos = getEndOfMissilePosition(activeQ.Position.To2D(), QLeftSplitPos, target, out enemyHitWithLeftQSplit);
            QRightSplitPos = getEndOfMissilePosition(activeQ.Position.To2D(), QRightSplitPos, target, out enemyHitWithRightQSplit);

            startPos = activeQ.StartPosition.Extend(activeQ.EndPosition, activeQ.Position.To2D().Distance(activeQ.StartPosition) + 25f);
            LeftPos = QLeftSplitPos;
            RightPos = QRightSplitPos;

            //draw the point to cast to and the point to branch at for left side
            if (enemyHitWithLeftQSplit == target || enemyHitWithRightQSplit == target)
            {
                Q.Cast(vel.Position);
                //Chat.Print("Q Split" + target.Name);
            }
        }
    }
}