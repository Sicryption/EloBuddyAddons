using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace Template
{
    class ModeHandler
    {
        public static AIHeroClient Champion => Player.Instance;
        public static bool hasDoneActionThisTick = false;

        public static void Combo()
        {
            Menu menu = MenuHandler.Combo;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

        }

        public static void Harass()
        {
            Menu menu = MenuHandler.Harass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

        }

        public static void AutoHarass()
        {
            if (Champion.IsUnderEnemyturret())
                return;

            Menu menu = MenuHandler.AutoHarass;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

        }

        public static void JungleClear()
        {
            Menu menu = MenuHandler.JungleClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList();

        }

        public static void Killsteal()
        {
            Menu menu = MenuHandler.Killsteal;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();
            

        }
        
        public static void Flee()
        {
            Menu menu = MenuHandler.Flee;
            List<Obj_AI_Base> enemies = EntityManager.Heroes.Enemies.ToList().ToObj_AI_BaseList();

        }

        public static void LaneClear()
        {
            Menu menu = MenuHandler.LaneClear;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

        }

        public static void LastHit()
        {
            Menu menu = MenuHandler.LastHit;
            List<Obj_AI_Base> enemies = EntityManager.MinionsAndMonsters.EnemyMinions.ToList().ToObj_AI_BaseList();

        }
        
        public static void CastQ(List<Obj_AI_Base> enemies, bool ks, Menu menu)
        {

        }

        public static void CastW(List<Obj_AI_Base> enemies, bool ks, Menu menu)
        {

        }

        public static void CastE(List<Obj_AI_Base> enemies, bool ks, Menu menu)
        {

        }

        public static void CastR(List<Obj_AI_Base> enemies, bool ks, Menu menu)
        {

        }


        public static void UseItems()
        {
            Menu menu = MenuHandler.Items;

            #region Item Initialization
            //InventorySlot SeraphsEmbrace = menu.GetCheckboxValue("Seraphs Embrace") ? Ryze.GetItem(ItemId.Seraphs_Embrace) : null,
            //    Zhonyas = menu.GetCheckboxValue("Zhonyas Hourglass") ? Ryze.GetItem(ItemId.Zhonyas_Hourglass) : null;
            #endregion

        }
    }
}