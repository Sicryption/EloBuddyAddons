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
    public static class CustomExtensions
    {
        public static List<Obj_AI_Base> GetNearbyEnemies(this Obj_AI_Base self, uint range)
        {
            List<Obj_AI_Base> enemies = ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy && !a.IsDead && a.IsInRange(self, range)).ToList();
            return enemies;
        }
        public static bool IsUnderTower(this Vector3 position)
        {
            return (EntityManager.Turrets.AllTurrets.Where(a => !a.IsDead && a.Distance(position) <= a.GetAutoAttackRange()).OrderBy(a => a.Distance(position)).FirstOrDefault() == null) ? false : true;
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
        public static bool MeetsCriteria(this Obj_AI_Base target)
        {
            if (!target.IsDead && target.IsVisible && !target.IsInvulnerable && target.IsTargetable && target.IsHPBarRendered)
                return true;
            return false;
        }
        public static bool MeetsCriteria(this InventorySlot item)
        {
            if (item != null && item.CanUseItem())
                return true;
            return false;
        }
        public static InventorySlot GetItem(this AIHeroClient self, ItemId item)
        {
            return self.InventoryItems.Where(a => a.Id == item).FirstOrDefault();
        }
        public static float TimeLeftOnKnockup(this AIHeroClient self)
        {
            if (self == null)
                return 1000f;

            BuffInstance instance = self.Buffs.Where(a => a != null && a.IsKnockback || a.IsKnockup).OrderBy(a => a.EndTime).FirstOrDefault();
            if (instance != null)
                return instance.EndTime - Game.Time;
            else
                return 1000f;
        }
        public static bool IsKnockedUp(this AIHeroClient self)
        {
            return (self.HasBuffOfType(BuffType.Knockback) || self.HasBuffOfType(BuffType.Knockup));
        }
        public static bool GetCheckboxValue(this Menu self, string text)
        {
            return MenuHandler.GetCheckboxValue(self, text);
        }
        public static int GetSliderValue(this Menu self, string text)
        {
            return MenuHandler.GetSliderValue(self, text);
        }
        public static string GetComboBoxText(this Menu self, string text)
        {
            return MenuHandler.GetComboBoxText(self, text);
        }
        public static bool IsAutoCanceling(this AIHeroClient self, List<Obj_AI_Base> enemies)
        {
            return !Orbwalker.CanAutoAttack || enemies.Where(a => a.IsInRange(self, self.GetAutoAttackRange())).FirstOrDefault() == null;
        }
        public static float ComboDamage(this AIHeroClient enemy)
        {
            float qdmg = Program.Q.IsReady() ? YasuoCalcs.Q(enemy) : 0;
            float edmg = Program.E.IsReady() ? YasuoCalcs.E(enemy) : 0;
            float autoDmg = Player.Instance.GetAutoAttackDamage(enemy) * MenuHandler.Drawing.GetSliderValue("Autos used in Combo");
            float qTotalDmg = qdmg * MenuHandler.Drawing.GetSliderValue("Q's used in Combo");
            float tiamat = Player.Instance.GetItem(ItemId.Tiamat) != null && Player.Instance.GetItem(ItemId.Tiamat).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Tiamat) : 0;
            float thydra = Player.Instance.GetItem(ItemId.Titanic_Hydra) != null && Player.Instance.GetItem(ItemId.Titanic_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Titanic_Hydra) : 0;
            float rhydra = Player.Instance.GetItem(ItemId.Ravenous_Hydra) != null && Player.Instance.GetItem(ItemId.Ravenous_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Ravenous_Hydra) : 0;

            float comboDamage = qTotalDmg + edmg + autoDmg + tiamat + thydra + rhydra;

            return comboDamage;
        }
        public static Vector3 GetBestLinearPredictionPos(this Spell.Skillshot self, List<Obj_AI_Base> enemies, Vector3 sourcePosition, out int enemiesHit)
        {
            enemiesHit = 0;

            enemies = enemies.Where(a => a.MeetsCriteria() && a.Position(self.CastDelay).IsInRange(sourcePosition, self.Range)).ToList();
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
                   enemies.Where(enemy => a.Item1.IsInside(enemy)).OrderBy(enemy => enemy.Position(self.CastDelay).Distance(sourcePosition)).FirstOrDefault() != null
                ).FirstOrDefault();
                if (bestPos != null)
                    enemiesHit = 1;
            }
            else
            {
                bestPos = rectPositions.OrderByDescending(a =>
                   enemies.Where(enemy => a.Item1.IsInside(enemy.Position(self.CastDelay))).Count()
                ).FirstOrDefault();

                if (bestPos != null)
                {
                    enemiesHit = enemies.Where(enemy => bestPos.Item1.IsInside(enemy.Position(self.CastDelay))).Count();
                }
            }

            if (bestPos != null)
                return sourcePosition.Extend(bestPos.Item2, self.Range / 2).To3D((int)sourcePosition.Z);
            else
                return Vector3.Zero;
        }
        public static Vector3 Position(this Obj_AI_Base unit, int secondsTimes1000)
        {
            if (MenuHandler.mainMenu.GetComboBoxText("Prediction Type:") == "EloBuddy")
                return Prediction.Position.PredictUnitPosition(unit, secondsTimes1000).To3D((int)unit.Position.Z);
            else if (MenuHandler.mainMenu.GetComboBoxText("Prediction Type:") == "Current Position")
                return unit.Position;
            else
            {
                Console.WriteLine("This prediction is not support. Contact Chaos to fix this.");
                return unit.Position;
            }
        }
    }
}
