using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RotTsunami
{
    class Program
    {
        public static AIHeroClient Champion { get { return Player.Instance; } }
        public static Vector3 rotpos = new Vector3(11846, 11412, 91.42979f);

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            Loading.OnLoadingComplete += MenuHandler.Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Game.MapId != GameMapId.SummonersRift)
            {
                Chat.Print("Rot Tsunami only works on Summoners Rift");
                return;
            }
            if(Player.Instance.Team != GameObjectTeam.Chaos)
            {
                Chat.Print("Rot Tsunami only works if you spawn on Red Side");
                return;
            }

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Champion.IsDead) return;

            if (MenuHandler.GetCheckboxValue(MenuHandler.mainMenu, "Enable Rot Placement") && Champion.HasItem(ItemId.ZzRot_Portal))
            {
                InventorySlot zz = Champion.InventoryItems.Where(a => a.Id == ItemId.ZzRot_Portal).FirstOrDefault();

                var ActiveZZRot = ObjectManager.Get<Obj_AI_Base>().Where(a=>a.Name == "VoidGate" && a.IsInRange(rotpos, 20f)).FirstOrDefault();
                if ((ActiveZZRot == null || ActiveZZRot.IsDead || ActiveZZRot.Health == 0 || !ActiveZZRot.IsHPBarRendered) && zz.CanUseItem() && Player.Instance.IsInRange(rotpos, 400f))
                    zz.Cast(rotpos);
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Champion.IsDead)
                return;
            if(MenuHandler.GetCheckboxValue(MenuHandler.mainMenu, "Enable Drawings"))
                Drawing.DrawCircle(rotpos, 400, System.Drawing.Color.Blue);
        }
    }
}
