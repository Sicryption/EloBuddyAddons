using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedGangplank
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
            foreach (Obj_AI_Minion unit in list.Where(a => a.MeetsCriteria() && a.Name != "Barrel"))
                returnList.Add(unit as Obj_AI_Base);
            return returnList;
        }
        public static float MissingHealth(this AIHeroClient self)
        {
            return self.MaxHealth - self.Health;
        }
        public static float MissingHealthPercent(this AIHeroClient self)
        {
            return self.MissingHealth() / self.MaxHealth;
        }
        public static InventorySlot GetItem(this AIHeroClient self, ItemId item)
        {
            return self.InventoryItems.Where(a => a.Id == item).FirstOrDefault();
        }
        public static bool CanCancleCC(this AIHeroClient self, bool slow = false)
        {
            return (self.HasBuffOfType(BuffType.Blind)
                || self.HasBuffOfType(BuffType.Charm)
                || self.HasBuffOfType(BuffType.Fear)
                || self.HasBuffOfType(BuffType.Knockback)
                || self.HasBuffOfType(BuffType.Silence)
                || self.HasBuffOfType(BuffType.Snare)
                || self.HasBuffOfType(BuffType.Stun)
                || (self.HasBuffOfType(BuffType.Slow) && slow)
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
            return !Orbwalker.CanAutoAttack || enemies.Where(a => a.IsInRange(self, self.GetAutoAttackRange())).FirstOrDefault() == null;
        }
        public static float ComboDamage(this AIHeroClient self, AIHeroClient enemy)
        {
            float qdmg = Program.Q.IsReady() ? Calculations.Q(enemy) : 0;
            float edmg = Program.E.IsReady() ? Calculations.E(enemy, false) : 0;
            float autoDmg = Player.Instance.GetAutoAttackDamage(enemy) * MenuHandler.Drawing.GetSliderValue("Autos in Combo");
            float tiamat = Player.Instance.GetItem(ItemId.Tiamat) != null && Player.Instance.GetItem(ItemId.Tiamat).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Tiamat) : 0;
            float thydra = Player.Instance.GetItem(ItemId.Titanic_Hydra) != null && Player.Instance.GetItem(ItemId.Titanic_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Titanic_Hydra) : 0;
            float rhydra = Player.Instance.GetItem(ItemId.Ravenous_Hydra) != null && Player.Instance.GetItem(ItemId.Ravenous_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Ravenous_Hydra) : 0;

            float comboDamage = qdmg + edmg + autoDmg + tiamat + thydra + rhydra;

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
        public static bool IsInRangeOfBarrels(this Obj_AI_Base unit, List<Barrel> barrels)
        {
            return barrels.Any(a => a.barrel.IsInRange(unit, Program.barrelRadius));
        }
        public static bool IsInRangeOfBarrels(this Vector3 pos, List<Barrel> barrels)
        {
            return barrels.Any(a => a.barrel.IsInRange(pos, Program.barrelRadius));
        }
        public static int NearbyBarrelCount(this AIHeroClient self, float range)
        {
            return Program.barrels.Where(a => a.barrel.MeetsCriteria() && self.IsInRange(a.barrel, range)).Count();
        }
        public static int NearbyBarrelCount(this Obj_AI_Base self, float range)
        {
            return Program.barrels.Where(a => a.barrel.MeetsCriteria() && self.IsInRange(a.barrel, range)).Count();
        }
        public static int NearbyBarrelCount(this Vector2 self, float range)
        {
            return Program.barrels.Where(a => a.barrel.MeetsCriteria() && self.IsInRange(a.barrel, range)).Count();
        }
        public static int NearbyBarrelCount(this GrassObject self, float range)
        {
            return Program.barrels.Where(a => a.barrel.MeetsCriteria() && self.IsInRange(a.barrel, range)).Count();
        }
        public static Obj_AI_Base Nearby1HPBarrel(this AIHeroClient self, List<Obj_AI_Base> enemies, float range, int hitNumber, bool KS, bool KSWithQ)
        {
            if (KS && !KSWithQ)
                enemies = enemies.Where(a => a.Health <= Program.Gangplank.GetAutoAttackDamage(a)).ToList();
            else if (KS)
                enemies = enemies.Where(a => a.Health <= Calculations.Q(a)).ToList();

            Barrel b = Program.barrels.Where(a => a.barrel.MeetsCriteria() && self.IsInRange(a.barrel, range) && a.barrel.Health == 1 && a.EHitNumber(enemies) >= hitNumber).FirstOrDefault();

            if (b != null)
                return b.barrel;
            return null;
        }
        public static Obj_AI_Base Nearby1HPBarrel(this AIHeroClient self, float range)
        {
            Barrel b = Program.barrels.Where(a => a.barrel.MeetsCriteria() && self.IsInRange(a.barrel, range) && a.barrel.Health == 1).FirstOrDefault();

            if (b != null)
                return b.barrel;
            return null;
        }

        public static int EHitNumber(this Spell.Skillshot.BestPosition self, List<Obj_AI_Base> enemies)
        {
            return enemies.Count(a => self.CastPosition.IsInRange(a.Position(250), Program.barrelRadius));
        }
        public static int RHitNumber(this Spell.Skillshot.BestPosition self, List<Obj_AI_Base> enemies)
        {
            return enemies.Count(a => self.CastPosition.IsInRange(a.Position(250), 525f));
        }
        public static int EHitNumber(this Barrel self, List<Obj_AI_Base> enemies)
        {
            return enemies.Count(a => self.barrel.IsInRange(a.Position(250), Program.barrelRadius));
        }
        public static void DrawCircle(this Vector3 pos, int radius, Color color, float width = 3)
        {
            Extensions.DrawCircle(pos, radius, color, width);
        }
    }
}