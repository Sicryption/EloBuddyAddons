using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace ShroomTracker
{
    internal class Program
    {
        public static Menu menu;
        public static List<GameObject> Shrooms = new List<GameObject>();
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            menu = MainMenu.AddMenu("Shroom Tracker", "ShroomTracker");
            menu.Add("ABOUT", new Label("This Addon was designed by Chaos"));
            menu.Add("Draw", new CheckBox("Draw Shrooms", true));

            //check for existing shrooms           
            foreach (GameObject obj in ObjectManager.Get<GameObject>())
                if (obj.Name == "Noxious Trap" && obj.IsEnemy)
                    Shrooms.Add(obj);

            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.Name == "Noxious Trap" && sender.IsEnemy)
                Shrooms.Remove(sender);
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if(sender.Name == "Noxious Trap" && sender.IsEnemy)
                Shrooms.Add(sender);
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if(menu.Get<CheckBox>("Draw").CurrentValue)
                foreach(GameObject shroom in Shrooms)
                    if(!shroom.IsDead)
                        Drawing.DrawCircle(shroom.Position, 60, System.Drawing.Color.Green);
        }
    }
}
