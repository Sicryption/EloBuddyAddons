using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedVi
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
        public static int HitNumber(this Spell.Skillshot.BestPosition self, Spell.Skillshot spell)
        {
            if(spell.Type == EloBuddy.SDK.Enumerations.SkillShotType.Cone)
            {
                Geometry.Polygon.Sector cone = new Geometry.Polygon.Sector(Program.Vi.Position, Program.Vi.Position - self.CastPosition, spell.ConeAngleDegrees, spell.Range);
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
        public static bool IsAutoCanceling(this AIHeroClient self, List<Obj_AI_Base> enemies, bool AutoReset = false)
        {
            return !Orbwalker.CanAutoAttack || (!AutoReset && enemies.Where(a => a.IsInRange(self, self.GetAutoAttackRange())).FirstOrDefault() == null);
        }
        public static float ComboDamage(this AIHeroClient enemy)
        {
            float qdmg = Program.Q.IsReady() ? Calculations.Q(enemy, Program.Q.TimeSinceCharge()) : 0;
            float wdmg = Program.W.IsReady() ? Calculations.W(enemy) : 0;
            float edmg = Program.E.IsReady() ? Calculations.E(enemy) : 0;
            float rdmg = Program.R.IsReady() ? Calculations.R(enemy) : 0;
            float autoDmg = Player.Instance.GetAutoAttackDamage(enemy) * MenuHandler.Drawing.GetSliderValue("Autos in Combo");
            float tiamat = Player.Instance.GetItem(ItemId.Tiamat) != null && Player.Instance.GetItem(ItemId.Tiamat).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Tiamat) : 0;
            float thydra = Player.Instance.GetItem(ItemId.Titanic_Hydra) != null && Player.Instance.GetItem(ItemId.Titanic_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Titanic_Hydra) : 0;
            float rhydra = Player.Instance.GetItem(ItemId.Ravenous_Hydra) != null && Player.Instance.GetItem(ItemId.Ravenous_Hydra).CanUseItem() ? DamageLibrary.GetItemDamage(Player.Instance, enemy, ItemId.Ravenous_Hydra) : 0;

            float comboDamage = qdmg + wdmg + edmg + rdmg + autoDmg + tiamat + thydra + rhydra;

            return comboDamage;
        }
        public static float TimeSinceCharge(this Spell.Chargeable self)
        {
            return Game.Time - (self.ChargingStartedTime / 1000);
        }
        public static float Range(this Spell.Chargeable self)
        {
            return Math.Min(self.MinimumRange + (self.TimeSinceCharge() * 380), self.MaximumRange);
        }
    }
}
