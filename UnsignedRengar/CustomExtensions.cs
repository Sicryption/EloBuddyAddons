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
        public static int GetSliderValue(this Menu self, string text)
        {
            return MenuHandler.GetSliderValue(self, text);
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
                Vector3 endPosition = extendingPosition.To2D().RotateAroundPoint(sourcePosition.To2D(), (float)((i * 15) * Math.PI / 180)).To3D((int)sourcePosition.Z);
                Geometry.Polygon.Sector sector = new Geometry.Polygon.Sector(sourcePosition,
                     endPosition, (float)(cone.ConeAngleDegrees * Math.PI / 180), radius);
                
                conePositions.Add(Tuple.Create(sector, endPosition));
            }

            //order list by most hit by Q1 and Q2
            conePositions = conePositions.OrderByDescending(a => a.Item1.EnemiesHitInSectorAndRectangle(new Geometry.Polygon.Rectangle(sourcePosition, a.Item2, linearSpell.Width), enemies)).ToList();
            //only leave the ones with the highest amount
            conePositions = conePositions.Where(a => a.Item1.EnemiesHitInSectorAndRectangle(new Geometry.Polygon.Rectangle(sourcePosition, a.Item2, linearSpell.Width), enemies) == conePositions[0].Item1.EnemiesHitInSectorAndRectangle(new Geometry.Polygon.Rectangle(sourcePosition, a.Item2, linearSpell.Width), enemies)).ToList();
            //from the ones with the most Sector/Line enemies hit, find the one with the most in the rectangle
            conePositions = conePositions.OrderByDescending(a => new Geometry.Polygon.Rectangle(sourcePosition, a.Item2, linearSpell.Width).EnemiesHitInRectangle(enemies)).ToList();
            //only take the ones with the most enemies
            conePositions = conePositions.Where(a => new Geometry.Polygon.Rectangle(sourcePosition, a.Item2, linearSpell.Width).EnemiesHitInRectangle(enemies) == new Geometry.Polygon.Rectangle(sourcePosition, conePositions[0].Item2, linearSpell.Width).EnemiesHitInRectangle(enemies)).ToList();
            //from the ones with the most sector/line enemies hit AND the most line enemies hit, find the ones with the most sector area
            conePositions = conePositions.OrderByDescending(a => a.Item1.EnemiesHitInSector(enemies)).ToList();

            Tuple<Geometry.Polygon.Sector, Vector3> bestCone = conePositions.First();
            bestHitNumber = bestCone.Item1.EnemiesHitInSectorAndRectangle(new Geometry.Polygon.Rectangle(sourcePosition, bestCone.Item2, linearSpell.Width), enemies);
            
            return sourcePosition.Extend(bestCone.Item2, radius - 1).To3D((int)sourcePosition.Z);
        }
        public static float GreyShieldPercent(this AIHeroClient self)
        {
            return 100 * (self.AttackShield / self.MaxHealth);
        }
        public static InventorySlot GetItem(this AIHeroClient self, ItemId item)
        {
            return self.InventoryItems.Where(a => a.Id == item).FirstOrDefault();
        }
        public static bool CanCancleCC(this AIHeroClient self)
        {
            return (self.HasBuffOfType(BuffType.Blind)
                || self.HasBuffOfType(BuffType.Charm)
                || self.HasBuffOfType(BuffType.Fear)
                || self.HasBuffOfType(BuffType.Knockback)
                || self.HasBuffOfType(BuffType.Silence)
                || self.HasBuffOfType(BuffType.Snare)
                || self.HasBuffOfType(BuffType.Stun)
                || self.HasBuffOfType(BuffType.Taunt))
                //not being knocked back by dragon
                && !self.HasBuff("moveawaycollision")
                //not standing on raka silence
                && !self.HasBuff("sorakaepacify");
        }
        public static bool MeetsCriteria(this InventorySlot item)
        {
            if (item != null && item.CanUseItem())
                return true;
            return false;
        }
        public static int EnemiesHitInSector(this Geometry.Polygon.Sector sector, List<Obj_AI_Base> enemies)
        {
            return enemies.Where(a => a.MeetsCriteria() && sector.IsInside(a)).Count();
        }
        public static int EnemiesHitInRectangle(this Geometry.Polygon.Rectangle rect, List<Obj_AI_Base> enemies)
        {
            return enemies.Where(a => a.MeetsCriteria() && rect.IsInside(a)).Count();
        }
        public static Vector3 GetBestLinearPredictionPos(this Spell.Skillshot self, List<Obj_AI_Base> enemies, Vector3 sourcePosition, out int enemiesHit)
        {
            enemiesHit = 0;

            enemies = enemies.Where(a => a.MeetsCriteria() && a.IsInRange(sourcePosition, self.Range)).ToList();
            Vector3 castPosition = Vector3.Zero;

            //if there is nothing that meets the criteria
            if (enemies.Count() == 0)
                return castPosition;

            List<Tuple<Geometry.Polygon.Rectangle, Vector3>> rectPositions = new List<Tuple<Geometry.Polygon.Rectangle, Vector3>>();

            Vector3 extendingPosition = sourcePosition + new Vector3(0, self.Range, 0);

            //checks every 15 degrees
            for (int i = 0; i < 24; i++)
            {
                Vector3 endPosition = extendingPosition.To2D().RotateAroundPoint(sourcePosition.To2D(), (float)((i * 15) * Math.PI / 180)).To3D((int)sourcePosition.Z);
                Geometry.Polygon.Rectangle rect = new Geometry.Polygon.Rectangle(sourcePosition, endPosition, self.Width);

                rectPositions.Add(Tuple.Create(rect, endPosition));
            }

            Tuple<Geometry.Polygon.Rectangle, Vector3> bestPos = null;
            if (self.AllowedCollisionCount == 1)
            {
                bestPos = rectPositions.Where(a =>
                   enemies.Where(enemy => a.Item1.IsInside(enemy)).OrderBy(enemy => enemy.Distance(sourcePosition)).FirstOrDefault() != null
                ).FirstOrDefault();
                if (bestPos != null)
                    enemiesHit = 1;
            }
            else
            {
                bestPos = rectPositions.OrderByDescending(a =>
                   enemies.Where(enemy => a.Item1.IsInside(enemy)).Count()
                ).FirstOrDefault();
                if (bestPos != null)
                   enemiesHit = enemies.Where(enemy => bestPos.Item1.IsInside(enemy)).Count();
            }

            if (bestPos != null)
                return sourcePosition.Extend(bestPos.Item2, self.Range / 2).To3D((int)sourcePosition.Z);
            else
                return Vector3.Zero;
        }
        public static int EnemiesHitInSectorAndRectangle(this Geometry.Polygon.Sector sector, Geometry.Polygon.Rectangle rect, List<Obj_AI_Base> enemies)
        {
            return enemies.Where(a => a.MeetsCriteria() && rect.IsInside(a)).Count();
        }
        public static bool IsAutoCanceling(this AIHeroClient self, List<Obj_AI_Base> enemies)
        {
            return !Orbwalker.CanAutoAttack || enemies.Where(a => a.IsInRange(self, self.GetAutoAttackRange())).FirstOrDefault() == null;
        }
        public static bool IsAbleToJump(this AIHeroClient self)
        {
            if (self.GetAutoAttackRange() == 825)
                return true;
            else
                return false;
        }
        public static float ComboDamage(this AIHeroClient enemy)
        {
            float qdmg = Program.Q.IsReady() ? Calculations.Q(enemy) : 0;
            float wdmg = Program.W.IsReady() ? Calculations.W(enemy) : 0;
            float edmg = Program.E.IsReady() ? Calculations.E(enemy) : 0;
            float empQDmg = Program.Q.IsReady() ? Calculations.EmpQ(enemy) : 0;
            float autoDmg = Player.Instance.GetAutoAttackDamage(enemy) * MenuHandler.Drawing.GetSliderValue("Autos in Combo");
            float tiamat = Player.Instance.GetItem(ItemId.Tiamat) != null && Player.Instance.GetItem(ItemId.Tiamat).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Tiamat) : 0;
            float thydra = Player.Instance.GetItem(ItemId.Titanic_Hydra) != null && Player.Instance.GetItem(ItemId.Titanic_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Titanic_Hydra) : 0;
            float rhydra = Player.Instance.GetItem(ItemId.Ravenous_Hydra) != null && Player.Instance.GetItem(ItemId.Ravenous_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Ravenous_Hydra) : 0;

            float comboDamage = qdmg + wdmg + edmg + empQDmg + autoDmg + tiamat + thydra + rhydra;

            return comboDamage;
        }
    }
}
