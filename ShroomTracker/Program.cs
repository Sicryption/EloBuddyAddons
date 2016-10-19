using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace ShroomTracker
{
    internal class Program
    {
        public static Menu menu;
        public static List<string> badNames = new List<string>()
        {
            "R",
            "G",
            "B",
            "A",
            "IsEmpty",
            "IsKnownColor",
            "Name",
            "IsNamedColor",
            "IsSystemColor"
        };
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
            menu.Add("Text", new CheckBox("Draw Text on Shroom", true));
            menu.Add("Width", new Slider("Shroom Circle Width", 20, 1, 59));
            
            List<string> colorList = new List<string>();

            foreach (PropertyInfo info in typeof(Color).GetProperties())
                if (!badNames.Contains(info.Name))
                    colorList.Add(info.Name);

            Slider slider = menu.Add("Color", new Slider("Shroom Color", 52, 0, colorList.Count - 1));

            //check for existing shrooms           
            foreach (GameObject obj in ObjectManager.Get<GameObject>())
                if (obj.Name == "Noxious Trap" && obj.IsEnemy && !obj.IsDead)
                    Shrooms.Add(obj);

            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
            slider.OnValueChange += Slider_OnValueChange;

            Slider_OnValueChange(slider, null);
        }

        private static void Slider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            List<string> colorList = new List<string>();

            foreach (PropertyInfo info in typeof(Color).GetProperties())
                if(!badNames.Contains(info.Name))
                    colorList.Add(info.Name);

            ((Slider)sender).DisplayName = "Shroom Color: " + colorList[((Slider)sender).CurrentValue];
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
                        for(int i = 60; i > 60 - menu.Get<Slider>("Width").CurrentValue; i--)
                            Drawing.DrawCircle(shroom.Position, i, GetActiveColor());
            if (menu.Get<CheckBox>("Text").CurrentValue)
                foreach (GameObject shroom in Shrooms)
                    if (!shroom.IsDead)
                        Drawing.DrawText(shroom.Position.WorldToScreen(), GetActiveColor(), "Shroom", 25);
        }

        public static Color GetActiveColor()
        {
            List<string> colorList = new List<string>();

            foreach (PropertyInfo info in typeof(Color).GetProperties())
                colorList.Add(info.Name);

            foreach (PropertyInfo info in typeof(Color).GetProperties())
                if (!badNames.Contains(info.Name))
                    if (info.Name == colorList[menu.Get<Slider>("Color").CurrentValue])
                    return (Color)info.GetValue(typeof(Color), null);

            return Color.Black;
        }
    }
}