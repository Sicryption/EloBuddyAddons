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
    }
}
