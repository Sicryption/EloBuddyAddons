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

            Vector2 northernMostPoint = (startPosition.Y >= endPosition.Y) ? startPosition.To2D() : endPosition.To2D();
            Vector2 southernMostPoint =  (startPosition.Y >= endPosition.Y) ? endPosition.To2D() : startPosition.To2D();
            
            Vector2 betweenVector = new Vector2(northernMostPoint.X - southernMostPoint.X, northernMostPoint.Y - southernMostPoint.Y);
            Vector2 betweenVector2 = new Vector2(betweenVector.Y, -betweenVector.X);
            double Length = Math.Sqrt(betweenVector2.X * betweenVector2.X + betweenVector2.Y * betweenVector2.Y); //Thats length of perpendicular
            Vector2 NewVector = new Vector2((float)(betweenVector2.X / Length), (float)(betweenVector2.Y / Length)); //Now N is normalized perpendicular
            
            Vector2 NEPoint = new Vector2(southernMostPoint.X + NewVector.X * (width / 2), southernMostPoint.Y + NewVector.Y * (width / 2));
            Vector2 NWPoint = new Vector2(southernMostPoint.X - NewVector.X * (width / 2), southernMostPoint.Y - NewVector.Y * (width / 2));
            Vector2 SEPoint = new Vector2(northernMostPoint.X + NewVector.X * (width / 2), northernMostPoint.Y + NewVector.Y * (width / 2));
            Vector2 SWPoint = new Vector2(northernMostPoint.X - NewVector.X * (width / 2), northernMostPoint.Y - NewVector.Y * (width / 2));
            
            //top
            Drawing.DrawLine(NEPoint.To3D().WorldToScreen(), NWPoint.To3D().WorldToScreen(), 3, drawColor);
            //bottom
            Drawing.DrawLine(SEPoint.To3D().WorldToScreen(), SWPoint.To3D().WorldToScreen(), 3, drawColor);
            //right
            Drawing.DrawLine(NEPoint.To3D().WorldToScreen(), SEPoint.To3D().WorldToScreen(), 3, drawColor);
            //left
            Drawing.DrawLine(NWPoint.To3D().WorldToScreen(), SWPoint.To3D().WorldToScreen(), 3, drawColor);
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

            Drawing.DrawText(position.WorldToScreen(), drawColor, "Corki W", 16);
        }

        public static void DrawCircularSkillshot(Vector3 position, float radius)
        {
            Drawing.DrawCircle(position, radius, drawColor);
        }

        public static void DrawConeSkillshot(Vector3 startPosition, Vector3 endPosition, float coneAngle)
        {
            //drawing won't look like a cone, instead will look like <>

            Vector2 centerPoint = startPosition.WorldToScreen();
            Vector2 endPoint = endPosition.WorldToScreen();

            Vector2 leftPoint = centerPoint.RotateAroundPoint(endPoint, (float)(coneAngle * Math.PI / 180));
            Vector2 rightPoint = centerPoint.RotateAroundPoint(endPoint, -(float)(coneAngle * Math.PI / 180));

            //Drawing.DrawLine(centerPoint, leftPoint, 3, System.Drawing.Color.Red);
            //Drawing.DrawLine(centerPoint, rightPoint, 3, System.Drawing.Color.Red);
            //Drawing.DrawLine(leftPoint, endPoint, 3, System.Drawing.Color.Red);
            //Drawing.DrawLine(rightPoint, endPoint, 3, System.Drawing.Color.Red);
            Drawing.DrawLine(centerPoint, endPoint, 3, drawColor);
        }
    }
}