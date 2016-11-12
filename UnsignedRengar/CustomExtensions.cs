using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedRengar
{
    static class CustomExtensions
    {
        public static int Ferocity(this AIHeroClient self)
        {
            if (self.ChampionName != "Rengar")
                return 0;
            else
                return (int)self.Mana;
        }
        public static bool MeetsCriteria(this Obj_AI_Base target)
        {
            if (!target.IsDead && target.IsVisible && !target.IsInvulnerable && target.IsTargetable && target.IsHPBarRendered)
                return true;
            return false;
        }

        public static bool GetCheckboxValue(this Menu self, string text)
        {
            return MenuHandler.GetCheckboxValue(self, text);
        }

        public static int HitNumber(this Spell.Skillshot.BestPosition self, Spell.Skillshot spell)
        {
            if(spell.Type == EloBuddy.SDK.Enumerations.SkillShotType.Cone)
            {
                Geometry.Polygon.Sector cone = new Geometry.Polygon.Sector(Program.Rengar.Position, Program.Rengar.Position - self.CastPosition, spell.ConeAngleDegrees, spell.Range);
                return EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && cone.IsInside(a)).Count();
            }
            return 0;
        }
        public static List<Obj_AI_Base> ToObj_AI_BaseList(this List<AIHeroClient> list)
        {
            List<Obj_AI_Base> returnList = new List<Obj_AI_Base>();
            foreach (AIHeroClient unit in list)
                returnList.Add(unit as Obj_AI_Base);
            return returnList;
        }
        public static List<Obj_AI_Base> ToObj_AI_BaseList(this List<Obj_AI_Minion> list)
        {
            List<Obj_AI_Base> returnList = new List<Obj_AI_Base>();
            foreach (Obj_AI_Minion unit in list)
                returnList.Add(unit as Obj_AI_Base);
            return returnList;
        }
        public static Vector3 GetBestConeAndLinearCastPosition(this Spell.Skillshot cone, Spell.Skillshot linearSpell, List<Obj_AI_Base> enemies, Vector3 sourcePosition, out int bestHitNumber)
        {
            int radius = (int)cone.Range;

            enemies = enemies.Where(a => a.MeetsCriteria() && a.IsInRange(sourcePosition, radius)).ToList();

            bestHitNumber = 0;
            Vector3 castPosition = Vector3.Zero;

            //if there is nothing that meets the criteria
            if (enemies.Count() == 0)
                return castPosition;

            List<Tuple<Geometry.Polygon.Sector, Vector3>> conePositions = new List<Tuple<Geometry.Polygon.Sector, Vector3>>();

            Vector3 extendingPosition = sourcePosition + new Vector3(0, radius, 0);

            //checks every 15 degrees
            for(int i = 0; i < 24; i++)
            {
                Vector3 endPosition = sourcePosition.To2D().RotateAroundPoint(extendingPosition.To2D(), (float)(i * 15 * Math.PI / 180)).To3D((int)sourcePosition.Z);
                Geometry.Polygon.Sector sector = new Geometry.Polygon.Sector(sourcePosition,
                     endPosition, (float)(cone.ConeAngleDegrees * Math.PI / 180), radius);
                
                conePositions.Add(Tuple.Create(sector, endPosition));
            }

            Tuple<Geometry.Polygon.Sector, Vector3> bestCone = 
                conePositions
                    .OrderBy(a => 
                        enemies.Where(b =>
                             b.MeetsCriteria()
                             && a.Item1.IsInside(b.Position)
                             //&& new Geometry.Polygon.Rectangle(sourcePosition, a.Item2, linearSpell.Width).IsInside(b)
                        ).Count()
                    )
                .FirstOrDefault();

            bestHitNumber = enemies.Where(a => bestCone.Item1.IsInside(a)
                && a.MeetsCriteria()
                //&& new Geometry.Polygon.Rectangle(sourcePosition, bestCone.Item2, linearSpell.Width).IsInside(a)
                ).Count();

            foreach (Tuple<Geometry.Polygon.Sector, Vector3> sector in conePositions)
                sector.Item1.Draw(System.Drawing.Color.Blue);


            bestCone.Item1.Draw(System.Drawing.Color.Blue);
            new Geometry.Polygon.Rectangle(sourcePosition, bestCone.Item2, linearSpell.Width).Draw(System.Drawing.Color.Blue);
            Drawing.DrawCircle(enemies.First().Position, 50, System.Drawing.Color.Blue);


            Chat.Print(bestHitNumber + "|" + bestCone.Item2);
            return sourcePosition.Extend(bestCone.Item2, radius).To3D((int)sourcePosition.Z);
        }
    }
}
