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
    class PolygonCreater
    {
        public static System.Drawing.Color drawColor = System.Drawing.Color.Blue;
        
        #region Create Spell Polygons
        public static CustomPolygon CreateCone(SpellInfo info, Vector3 startPosition, Vector3 endPosition, float coneAngle, float range)
        {
            Geometry.Polygon cone = new Geometry.Polygon.Arc(startPosition, endPosition, MathUtil.DegreesToRadians(coneAngle*2), range);
            cone.Add(startPosition);
            //CustomPolygon cone = new CustomPolygon.Sector(startPosition, endPosition, MathUtil.DegreesToRadians(coneAngle * 2), range);

            return new CustomPolygon(cone, info);
        }
        public static CustomPolygon CreateWall(SpellInfo info, Vector3 startPosition, Vector3 endPosition, float width, float radius)
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

            return CreateLinearSkillshot(info, leftPoint, rightPoint, width);
        }
        public static CustomPolygon CreateArc(SpellInfo info, Vector3 sourcePosition, Vector3 endPosition, float width)
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

            Geometry.Polygon arc = new Geometry.Polygon();
            //arc.Add(centerPoint);
            //arc.Add(sourcePosition);

            foreach (int i in GetAllAnglesBetween(angleOfPlayer, angleOfCursor))
            {
                float angleInRadians = MathUtil.DegreesToRadians(i);
                Vector2 test = new Vector2((float)(centerPoint.X + radius * Math.Cos(angleInRadians)),
                    (float)(centerPoint.Y + radius * Math.Sin(angleInRadians)));
                arc.Add(centerPoint.Extend(test, radius).To3D((int)cursorPos.Z));
                // Drawing.DrawLine(centerPoint.WorldToScreen(), centerPoint.Extend(test, radius).To3D().WorldToScreen(), 5f, Geometry.drawColor);
            }

            return new CustomPolygon(arc, info);
        }
        public static CustomPolygon CreateCircularSkillshot(SpellInfo info, Vector3 position, float radius)
        {
            Geometry.Polygon circle = new Geometry.Polygon.Circle(position, radius);
            
            return new CustomPolygon(circle, info);
        }
        public static CustomPolygon CreateRectangleAroundPoint(ParticleInfo info, int length, int width, Vector3 position, int xoffset = 0, int yoffset = 0)
        {
            Geometry.Polygon rect = new Geometry.Polygon();

            rect.Add(new Vector3(position.X - (length / 2) + xoffset, position.Y - (width / 2) + yoffset, position.Z));
            rect.Add(new Vector3(position.X + (length / 2) + xoffset, position.Y - (width / 2) + yoffset, position.Z));
            rect.Add(new Vector3(position.X - (length / 2) + xoffset, position.Y + (width / 2) + yoffset, position.Z));
            rect.Add(new Vector3(position.X + (length / 2) + xoffset, position.Y + (width / 2) + yoffset, position.Z));
            
            return new CustomPolygon(rect, info);
        }
        public static CustomPolygon CreateCircularWall(SpellInfo info, Vector3 position, float radius, float secondRadius)
        {
            Geometry.Polygon ring = new Geometry.Polygon.Ring(position, radius, secondRadius);
            
            return new CustomPolygon(ring, info);
        }
        public static CustomPolygon CreateLinearSkillshot(SpellInfo info, Vector3 startPosition, Vector3 endPosition, float width)
        {
            /*
            if (collisionCount != 0 && collisionCount != int.MaxValue)
            {
                List<Obj_AI_Base> enemiesThatWillBeHit = new List<Obj_AI_Base>();
                //get if unit(s) will be hit by spell if so get the info.CollisionCount's units position and set it as the end position
                foreach (Obj_AI_Base enemy in EntityManager.Enemies.Where(a => !a.IsDead && a.Distance(startPosition) <= range))
                    if (Prediction.Position.Collision.LinearMissileCollision(enemy, startPosition.To2D(), endPosition.To2D(), missileSpeed, (int)width, 0))
                        enemiesThatWillBeHit.Add(enemy);

                enemiesThatWillBeHit.OrderByDescending(a => a.Distance(startPosition));
                if (enemiesThatWillBeHit.Count() >= collisionCount)
                    endPosition = enemiesThatWillBeHit[(int)collisionCount - 1].Position;
            }*/

            Geometry.Polygon rectangle = new Geometry.Polygon.Rectangle(startPosition, endPosition, width);

            return new CustomPolygon(rectangle, info);
        }
        public static CustomPolygon CreateTargetedSpell(SpellInfo info, Vector3 startPosition, Vector3 endPosition)
        {
            //these posistions could have to be put as worldtoscreen
            Geometry.Polygon line = new Geometry.Polygon();
            line.Add(startPosition);
            line.Add(endPosition);

            return new CustomPolygon(line, info);
        }
        public static CustomPolygon CreateTargetedSpell(SpellInfo info, Vector3 startPosition, Obj_AI_Base target)
        {
            Geometry.Polygon line = new Geometry.Polygon();
            line.Add(startPosition);
            line.Add(target.Position);
            return new CustomPolygon(line, info);
        }
        public static CustomPolygon CreateTargetedSpell(SpellInfo info, Vector3 startPosition, GameObject target)
        {
            Geometry.Polygon line = new Geometry.Polygon();
            line.Add(startPosition);
            line.Add(target.Position);
            return new CustomPolygon(line, info);
        }
        #endregion

        //assuming the circle has a gap between -180 and 180
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
    }
}