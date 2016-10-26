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
        public static System.Drawing.Color drawColor = System.Drawing.Color.Blue;

        public static void DrawTargetedSpell(Vector3 startPosition, Vector3 endPosition)
        {
            Drawing.DrawLine(startPosition.WorldToScreen(), endPosition.WorldToScreen(), 10, drawColor);
        }
        public static void DrawTargetedSpell(Vector3 startPosition, Obj_AI_Base target)
        {
            Drawing.DrawLine(startPosition.WorldToScreen(), target.Position.WorldToScreen(), 10, drawColor);
        }
        public static void DrawTargetedSpell(Vector3 startPosition, GameObject target)
        {
            Drawing.DrawLine(startPosition.WorldToScreen(), target.Position.WorldToScreen(), 10, drawColor);
        }

        public static Vector3 CalculateEndPosition(Vector3 position, Vector3 endPosition, float range)
        {
            return Extensions.Extend(position, endPosition, range).To3DWorld();
        }

        //if onSpellCast was used then the spells end position is the direction the caster is facing extended to max range. IE: xerath q, velkoz r
        public static void DrawLinearSkillshot(Vector3 startPosition, Vector3 endPosition, float width, float missileSpeed, float range, float collisionCount)
        {
            if (collisionCount != 0 && collisionCount != int.MaxValue)
            {
                List<Obj_AI_Base> enemiesThatWillBeHit = new List<Obj_AI_Base>();
                //get if unit(s) will be hit by spell if so get the info.CollisionCount's units position and set it as the end position
                foreach(Obj_AI_Base enemy in EntityManager.Enemies.Where(a=>!a.IsDead &&a.Distance(startPosition) <= range))
                    if (Prediction.Position.Collision.LinearMissileCollision(enemy, startPosition.To2D(), endPosition.To2D(), missileSpeed, (int)width, 0))
                        enemiesThatWillBeHit.Add(enemy);

                enemiesThatWillBeHit.OrderByDescending(a => a.Distance(startPosition));
                if(enemiesThatWillBeHit.Count() >= collisionCount)
                    endPosition = enemiesThatWillBeHit[(int)collisionCount - 1].Position;
            }

            Vector3 northernMostPoint = (startPosition.Y >= endPosition.Y) ? startPosition : endPosition;
            Vector3 southernMostPoint =  (startPosition.Y >= endPosition.Y) ? endPosition : startPosition;

            Vector3 betweenVector = new Vector3(northernMostPoint.X - southernMostPoint.X, northernMostPoint.Y - southernMostPoint.Y, 0f);
            Vector2 betweenVector2 = new Vector2(betweenVector.Y, -betweenVector.X);
            double Length = Math.Sqrt(betweenVector2.X * betweenVector2.X + betweenVector2.Y * betweenVector2.Y); //Thats length of perpendicular
            Vector2 NewVector = new Vector2((float)(betweenVector2.X / Length), (float)(betweenVector2.Y / Length)); //Now N is normalized perpendicular

            Vector3 NEPoint = new Vector3(southernMostPoint.X + NewVector.X * (width / 2), southernMostPoint.Y + NewVector.Y * (width / 2), startPosition.Z);
            Vector3 NWPoint = new Vector3(southernMostPoint.X - NewVector.X * (width / 2), southernMostPoint.Y - NewVector.Y * (width / 2), startPosition.Z);
            Vector3 SEPoint = new Vector3(northernMostPoint.X + NewVector.X * (width / 2), northernMostPoint.Y + NewVector.Y * (width / 2), startPosition.Z);
            Vector3 SWPoint = new Vector3(northernMostPoint.X - NewVector.X * (width / 2), northernMostPoint.Y - NewVector.Y * (width / 2), startPosition.Z);
            
            //top
            Drawing.DrawLine(NEPoint.WorldToScreen(), NWPoint.WorldToScreen(), 3, drawColor);
            //bottom
            Drawing.DrawLine(SEPoint.WorldToScreen(), SWPoint.WorldToScreen(), 3, drawColor);
            //right
            Drawing.DrawLine(NEPoint.WorldToScreen(), SEPoint.WorldToScreen(), 3, drawColor);
            //left
            Drawing.DrawLine(NWPoint.WorldToScreen(), SWPoint.WorldToScreen(), 3, drawColor);
        }

        public static void DrawRectangle(int length, int width, Vector3 position, int xoffset = 0, int yoffset = 0)
        {
            Vector3 SW = Vector3.Zero, SE = Vector3.Zero, NW = Vector3.Zero, NE = Vector3.Zero;
            
            SW = new Vector3(position.X - (length / 2) + xoffset, position.Y - (width / 2) + yoffset, 0f);
            SE = new Vector3(position.X + (length / 2) + xoffset, position.Y - (width / 2) + yoffset, 0f);
            NW = new Vector3(position.X - (length / 2) + xoffset, position.Y + (width / 2) + yoffset, 0f);
            NE = new Vector3(position.X + (length / 2) + xoffset, position.Y + (width / 2) + yoffset, 0f);

            Drawing.DrawLine(SW.WorldToScreen(), SE.WorldToScreen(), 3, drawColor);
            Drawing.DrawLine(SW.WorldToScreen(), NW.WorldToScreen(), 3, drawColor);
            Drawing.DrawLine(NW.WorldToScreen(), NE.WorldToScreen(), 3, drawColor);
            Drawing.DrawLine(NE.WorldToScreen(), SE.WorldToScreen(), 3, drawColor);
        }

        public static void DrawCircularSkillshot(Vector3 position, float radius, float secondRadius = 0)
        {
            Drawing.DrawCircle(position, radius, drawColor);

            if(secondRadius != 0)
                Drawing.DrawCircle(position, secondRadius, drawColor);
        }

        public static void DrawConeSkillshot(Vector3 startPosition, Vector3 endPosition, float coneAngle, float range)
        {

            EloBuddy.SDK.Geometry.Polygon.Sector cone = new EloBuddy.SDK.Geometry.Polygon.Sector(startPosition, endPosition, (float)(coneAngle * 2 * Math.PI / 180), range);
            cone.Draw(drawColor, 3);
            
        }
        public static void DrawWall(Vector3 startPosition, Vector3 endPosition, float width, float radius)
        {
            Vector2 startPos = startPosition.To2D(),
                endPos = endPosition.To2D(),
                PerpendicularPos1 = startPos.Extend(endPos, radius / 2).Perpendicular(),
                PerpendicularPos2 = startPos.Perpendicular(),
                temp = new Vector2(endPos.X - PerpendicularPos1.X, endPos.Y - PerpendicularPos1.Y),
                PerpendicularPos3 = startPos.Extend(endPos, radius / 2).Perpendicular2(),
                PerpendicularPos4 = startPos.Perpendicular2(),
                temp2 = new Vector2(endPos.X - PerpendicularPos3.X, endPos.Y - PerpendicularPos3.Y);

            Vector3 leftPoint = (PerpendicularPos2 + temp).To3D() + new Vector3(0, 0, startPosition.Z),
                rightPoint = (PerpendicularPos4 + temp2).To3D() + new Vector3(0, 0, startPosition.Z);

            DrawLinearSkillshot(leftPoint, rightPoint, width, 0, 0, 0);
        }
    }
}