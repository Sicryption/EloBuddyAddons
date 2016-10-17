using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System.Linq;

namespace UnsignedEvade
{
    class Geometry
    {
        public static void DrawTargetedSpell(SpellInfo info)
        {
            if (info.missile == null || info == null || info.missile.Position == null && info.target.Name == Player.Instance.Name)
                return;

            if (info.Width == 0)
                info.Width = 10;
            
            for(int i = 0; i < info.GetPath().Count - 1; i++)
            {
                Drawing.DrawLine(info.GetPath()[i].WorldToScreen(), info.GetPath()[i + 1].WorldToScreen(), 4, System.Drawing.Color.Red);
            }

            //Drawing.DrawLine(info.missile.Position.WorldToScreen(), info.target.Position.WorldToScreen(), info.Width, System.Drawing.Color.Blue);
        }
        
        public static Vector3 CalculateEndPosition(SpellInfo info)
        {
            return Extensions.Extend(info.startPosition, info.endPosition, info.Range).To3DWorld();
        }

        //if onSpellCast was used then the spells end position is the direction the caster is facing extended to max range. IE: xerath q, velkoz r
        public static void DrawLinearSkillshot(SpellInfo info)
        {
            if (info.Width == 0)
                info.Width = 10;

            Vector2 startPosition = info.startPosition.WorldToScreen();
            if (info.missile == null)
            {
                //using this for gragas e dash
                if (info.DashType == SpellInfo.Dashtype.Linear)
                {
                    //dashes
                    if (info.SpellName != "CarpetBomb2" && info.SpellName != "CarpetBombMega2")
                        startPosition = info.caster.Position.WorldToScreen();
                }
            }
            else
            {
                //using this for missile spells ie velkoz q
                startPosition = info.missile.Position.WorldToScreen()   ; 

                if (info.MissileName.ToLower().Contains("return"))
                    info.endPosition = info.missile.SpellCaster.Position;
            }

            Vector2 endPosition = info.endPosition.WorldToScreen();

            if (info.CollisionCount != 0)
            {
                List<Obj_AI_Base> enemiesThatWillBeHit = new List<Obj_AI_Base>();
                //get if unit(s) will be hit by spell if so get the info.CollisionCount's units position and set it as the end position
                foreach(Obj_AI_Base enemy in EntityManager.Enemies.Where(a=>!a.IsDead &&a.Distance(info.startPosition) <= info.Range))
                    if (Prediction.Position.Collision.LinearMissileCollision(enemy, info.startPosition.To2D(), info.endPosition.To2D(), info.MissileSpeed, (int)info.Width, (int)info.Delay))
                        enemiesThatWillBeHit.Add(enemy);

                enemiesThatWillBeHit.OrderByDescending(a => a.Distance(info.startPosition));
                if(enemiesThatWillBeHit.Count() >= info.CollisionCount)
                    endPosition = enemiesThatWillBeHit[(int)info.CollisionCount - 1].Position.WorldToScreen();
            }


            Vector2 northernMostPoint;
            Vector2 southernMostPoint;

            if (startPosition.Y >= endPosition.Y)
            {
                northernMostPoint = startPosition;
                southernMostPoint = endPosition;
            }
            else
            {
                northernMostPoint = endPosition;
                southernMostPoint = startPosition;
            }
            Vector2 betweenVector = new Vector2(northernMostPoint.X - southernMostPoint.X, northernMostPoint.Y - southernMostPoint.Y);
            Vector2 betweenVector2 = new Vector2(betweenVector.Y, -betweenVector.X);
            double Length = Math.Sqrt(betweenVector2.X * betweenVector2.X + betweenVector2.Y * betweenVector2.Y); //Thats length of perpendicular
            Vector2 NewVector = new Vector2((float)(betweenVector2.X / Length), (float)(betweenVector2.Y / Length)); //Now N is normalized perpendicular


            Vector2 NEPoint = new Vector2(southernMostPoint.X + NewVector.X * (info.Width / 2), southernMostPoint.Y + NewVector.Y * (info.Width / 2));
            Vector2 NWPoint = new Vector2(southernMostPoint.X - NewVector.X * (info.Width / 2), southernMostPoint.Y - NewVector.Y * (info.Width / 2));
            Vector2 SEPoint = new Vector2(northernMostPoint.X + NewVector.X * (info.Width / 2), northernMostPoint.Y + NewVector.Y * (info.Width / 2));
            Vector2 SWPoint = new Vector2(northernMostPoint.X - NewVector.X * (info.Width / 2), northernMostPoint.Y - NewVector.Y * (info.Width / 2));

            //top
            Drawing.DrawLine(NEPoint, NWPoint, 3, System.Drawing.Color.Blue);
            //bottom
            Drawing.DrawLine(SEPoint, SWPoint, 3, System.Drawing.Color.Blue);
            //right
            Drawing.DrawLine(NEPoint, SEPoint, 3, System.Drawing.Color.Blue);
            //left
            Drawing.DrawLine(NWPoint, SWPoint, 3, System.Drawing.Color.Blue);
        }

        public static void DrawCircularSkillshot(SpellInfo info, bool SelfActive = false)
        {
            if(SelfActive)
                Drawing.DrawCircle(info.caster.Position, info.Radius, System.Drawing.Color.Blue);
            else
                Drawing.DrawCircle(info.endPosition, info.Radius, System.Drawing.Color.Blue);
        }

        public static void DrawConeSkillshot(SpellInfo info)
        {
            //drawing won't look like a cone, instead will look like <>

            Vector2 centerPoint = info.startPosition.WorldToScreen();
            Vector2 endPoint = info.endPosition.WorldToScreen();

            Vector2 leftPoint = centerPoint.RotateAroundPoint(endPoint, (float)(info.ConeDegrees * Math.PI / 180));
            Vector2 rightPoint = centerPoint.RotateAroundPoint(endPoint, -(float)(info.ConeDegrees * Math.PI / 180));

            //Drawing.DrawLine(centerPoint, leftPoint, 3, System.Drawing.Color.Red);
            //Drawing.DrawLine(centerPoint, rightPoint, 3, System.Drawing.Color.Red);
            //Drawing.DrawLine(leftPoint, endPoint, 3, System.Drawing.Color.Red);
            //Drawing.DrawLine(rightPoint, endPoint, 3, System.Drawing.Color.Red);
            Drawing.DrawLine(centerPoint, endPoint, 3, System.Drawing.Color.Blue);
        }
    }
}