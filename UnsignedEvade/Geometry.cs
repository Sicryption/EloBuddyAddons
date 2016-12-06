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
            return Extensions.Extend(position, endPosition, range).To3D((int)position.Z);
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
            
            SW = new Vector3(position.X - (length / 2) + xoffset, position.Y - (width / 2) + yoffset, position.Z);
            SE = new Vector3(position.X + (length / 2) + xoffset, position.Y - (width / 2) + yoffset, position.Z);
            NW = new Vector3(position.X - (length / 2) + xoffset, position.Y + (width / 2) + yoffset, position.Z);
            NE = new Vector3(position.X + (length / 2) + xoffset, position.Y + (width / 2) + yoffset, position.Z);
            
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
        public static void DrawCircularWall(Vector3 position, float radius, float radius2)
        {
            var ring = new EloBuddy.SDK.Geometry.Polygon.Ring(position, radius, radius2);
            ring.Draw(drawColor);
        }
        //assuming this has a gap between -180 and 180
        public static List<int> GetAllAnglesBetween(int angle1InDegrees, int angle2InDegrees)
        {
            List<int> returnList = new List<int>();

            //we are crossing gap
            if (angle1InDegrees >= 90 && angle2InDegrees <= -90)
            {
                for (int i = angle2InDegrees; i > -180; i--)
                    returnList.Add(i);

                for (int i = 180; i > angle1InDegrees; i--)
                    returnList.Add(i);
            }
            else
                for (int i = angle1InDegrees; i < angle2InDegrees; i++)
                    returnList.Add(i);

            return returnList;
        }
        public static void DrawArc(Vector3 sourcePosition, Vector3 endPosition, float width)
        {
            Vector3 cursorPos = new Vector3(endPosition.X, endPosition.Y, NavMesh.GetHeightForPosition(endPosition.X, endPosition.Y));

            double norm = Math.Sqrt(Math.Pow(cursorPos.X - sourcePosition.X, 2) + Math.Pow(cursorPos.Y - sourcePosition.Y, 2)),
                s = norm / 2,
                d = s * (1 - cursorPos.LengthSquared()) / cursorPos.LengthSquared(),
                u = (cursorPos.X - sourcePosition.X) / norm,
                v = (cursorPos.Y - sourcePosition.Y) / norm,
                c1 = v * d + (sourcePosition.X + cursorPos.X) / 2,
                c2 = -u * d + (sourcePosition.Y + cursorPos.Y) / 2;

            Vector3 centerPoint = new Vector3((float)c1, (float)c2, cursorPos.Z);
            int angleOfPlayer = (int)MathUtil.RadiansToDegrees((float)Math.Atan2(sourcePosition.Y - centerPoint.Y, sourcePosition.X - centerPoint.X)),
                angleOfCursor = (int)MathUtil.RadiansToDegrees((float)Math.Atan2(cursorPos.Y - centerPoint.Y, cursorPos.X - centerPoint.X));
            
            float radius = centerPoint.Distance(sourcePosition) + width;

            //arc circle
            //Drawing.DrawCircle(centerPoint, radius, drawColor);

            var arc = new EloBuddy.SDK.Geometry.Polygon();
            //arc.Add(centerPoint);
            //arc.Add(sourcePosition);

            foreach (int i in Geometry.GetAllAnglesBetween(angleOfPlayer, angleOfCursor))
            {
                float angleInRadians = MathUtil.DegreesToRadians(i);
                Vector2 test = new Vector2((float)(centerPoint.X + radius * Math.Cos(angleInRadians)),
                    (float)(centerPoint.Y + radius * Math.Sin(angleInRadians)));
                arc.Add(centerPoint.Extend(test, radius).To3D((int)cursorPos.Z));
                // Drawing.DrawLine(centerPoint.WorldToScreen(), centerPoint.Extend(test, radius).To3D().WorldToScreen(), 5f, Geometry.drawColor);
            }

            //arc.Add(cursorPos);
            arc.Draw(drawColor);
        }
    }
}