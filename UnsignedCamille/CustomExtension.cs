using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedCamille
{
    static class CustomExtensions
    {
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
        public static string GetComboBoxText(this Menu self, string text)
        {
            return MenuHandler.GetComboBoxText(self, text);
        }
        public static List<Obj_AI_Base> ToObj_AI_BaseList(this List<AIHeroClient> list)
        {
            List<Obj_AI_Base> returnList = new List<Obj_AI_Base>();
            foreach (AIHeroClient unit in list.Where(a => a.MeetsCriteria()))
                returnList.Add(unit as Obj_AI_Base);
            return returnList;
        }
        public static List<Obj_AI_Base> ToObj_AI_BaseList(this List<Obj_AI_Minion> list)
        {
            List<Obj_AI_Base> returnList = new List<Obj_AI_Base>();
            foreach (Obj_AI_Minion unit in list.Where(a => a.MeetsCriteria()))
                returnList.Add(unit as Obj_AI_Base);
            return returnList;
        }
        public static float MissingHealth(this AIHeroClient self)
        {
            return self.MaxHealth - self.Health;
        }
        public static float BonusAttackDamage(this AIHeroClient self)
        {
            return self.TotalAttackDamage - self.BaseAttackDamage;
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
                && !self.HasBuff("sorakaepacify")
                && !self.HasBuff("plantsatchelknockback");
        }
        public static bool MeetsCriteria(this InventorySlot item)
        {
            if (item != null && item.CanUseItem())
                return true;
            return false;
        }
        public static bool IsAutoCanceling(this AIHeroClient self, List<Obj_AI_Base> enemies)
        {
            return Orbwalker.IsAutoAttacking || enemies.Where(a => a.IsInRange(self, self.GetAutoAttackRange())).FirstOrDefault() == null;
        }
        public static float ComboDamage(this AIHeroClient enemy)
        {
            float q1dmg = Program.Q.IsReady() ? Calculations.Q1(enemy) : 0;
            float q2dmg = Program.Q.IsReady() ? Calculations.Q2(enemy, true) : 0;
            float wdmg = Program.W.IsReady() ? Calculations.W(enemy) : 0;
            float edmg = Program.E.IsReady() ? Calculations.E2(enemy) : 0;
            float rdmg = Program.R.IsReady() ? Calculations.RBasicAttack(enemy) * MenuHandler.Drawing.GetSliderValue("Autos in Combo") : 0;
            float autoDmg = Player.Instance.GetAutoAttackDamage(enemy) * MenuHandler.Drawing.GetSliderValue("Autos in Combo");
            float tiamat = Player.Instance.GetItem(ItemId.Tiamat) != null && Player.Instance.GetItem(ItemId.Tiamat).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Tiamat) : 0;
            float thydra = Player.Instance.GetItem(ItemId.Titanic_Hydra) != null && Player.Instance.GetItem(ItemId.Titanic_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Titanic_Hydra) : 0;
            float rhydra = Player.Instance.GetItem(ItemId.Ravenous_Hydra) != null && Player.Instance.GetItem(ItemId.Ravenous_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Ravenous_Hydra) : 0;

            float comboDamage = q1dmg + q2dmg + wdmg + edmg + rdmg + autoDmg + tiamat + thydra + rhydra;

            return comboDamage;
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
        public static Vector3 GetBestConeCastPosition(this Spell.Skillshot cone, List<Obj_AI_Base> enemies, out int bestHitNumber)
        {
            int radius = (int)cone.Range;

            enemies = enemies.Where(a => a.MeetsCriteria() && a.Position(cone.CastDelay).IsInRange(Player.Instance.Position, radius)).ToList();

            bestHitNumber = 0;
            Vector3 castPosition = Vector3.Zero;

            //if there is nothing that meets the criteria
            if (enemies.Count() == 0)
                return castPosition;

            List<Tuple<Geometry.Polygon.Sector, Vector3>> conePositions = new List<Tuple<Geometry.Polygon.Sector, Vector3>>();

            Vector3 extendingPosition = Player.Instance.Position + new Vector3(0, radius, 0);

            //checks every 15 degrees
            for (int i = 0; i < 24; i++)
            {
                Vector3 endPosition = extendingPosition.To2D().RotateAroundPoint(Player.Instance.Position.To2D(), (float)((i * 15) * Math.PI / 180)).To3D((int)Player.Instance.Position.Z);
                Geometry.Polygon.Sector sector = new Geometry.Polygon.Sector(Player.Instance.Position,
                     endPosition, (float)(cone.ConeAngleDegrees * Math.PI / 180), radius);

                conePositions.Add(Tuple.Create(sector, endPosition));
            }

            //from the ones with the most sector/line enemies hit AND the most line enemies hit, find the ones with the most sector area
            conePositions = conePositions.OrderByDescending(a => a.Item1.EnemiesHitInSector(enemies, cone.CastDelay)).ToList();

            Tuple<Geometry.Polygon.Sector, Vector3> bestCone = conePositions.First();

            bestHitNumber = bestCone.Item1.EnemiesHitInSector(enemies, cone.CastDelay);

            return Player.Instance.Position.Extend(bestCone.Item2, radius - 1).To3D((int)Player.Instance.Position.Z);
        }
        public static int EnemiesHitInSector(this Geometry.Polygon.Sector sector, List<Obj_AI_Base> enemies, int delay)
        {
            return enemies.Where(a => a.MeetsCriteria() && sector.IsInside(a.Position(delay))).Count();
        }
        public static List<Obj_AI_Base> GetEnemiesHitInSector(this Geometry.Polygon.Sector sector, List<Obj_AI_Base> enemies, int delay)
        {
            return enemies.Where(a => a.MeetsCriteria() && sector.IsInside(a.Position(delay))).ToList();
        }
        public static bool ContainsAny(this string s, bool CaseSensitive, params string[] text)
        {
            List<string> temp = text.ToList();
            if (!CaseSensitive)
            {
                List<string> NonCaseSensitiveList = new List<string>();
                foreach (string str in temp)
                    NonCaseSensitiveList.Add(str.ToLower());
                temp = NonCaseSensitiveList;
            }

            foreach (string str in temp)
                if (s.ToLower().Contains(str.ToLower()))
                    return true;
            return false;
        }
    }
}