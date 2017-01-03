using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using SharpDX;

namespace UnsignedPolygonPing
{
    internal class Program
    {
        public static float timeOfLastPing = 0;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            MenuHandler.Initialize();
            Game.OnTick += Game_OnTick;
            Chat.OnClientSideMessage += Chat_OnClientSideMessage;
        }
        
        private static void Chat_OnClientSideMessage(ChatClientSideMessageEventArgs args)
        {
            if (args.Message.StartsWith("You must wait"))
                args.Process = false;
        }
        private static void Game_OnTick(EventArgs args)
        {
            Menu menu = MenuHandler.PingMenu;
            if (!menu.GetCheckboxValue("Enable") || Game.Time - timeOfLastPing < 5)
                return;
            
            Obj_AI_Base closestUnit = (menu.GetKeybindValue("Ping Key"))?
                ObjectManager.Get<Obj_AI_Base>().Where(a => Game.CursorPos.IsInRange(a, 200)).OrderByDescending(a=>a.CharData.SelectionHeight).FirstOrDefault():
                (menu.GetComboBoxText("Spam Ping Player:") != "None")? EntityManager.Heroes.AllHeroes.First(a=>a.Name == menu.GetComboBoxText("Spam Ping Player:")) :null;

            if (closestUnit == null && menu.GetKeybindValue("Ping Key"))
                SendPing(Game.CursorPos);
            else if(closestUnit != null)
                SendPing(closestUnit.Position);
        }

        private static void SendPing(Vector3 position)
        {
            Menu menu = MenuHandler.PingMenu;

            foreach (Vector3 pos in GetShapePositions(menu.GetComboBoxText("Ping Shape:"), position))
            {
                TacticalMap.SendPing((PingCategory)(menu.GetComboBoxValue("Ping Type:") + 1), pos);
            }
            timeOfLastPing = Game.Time;
        }
        
        public static List<Vector3> GetShapePositions(string shape, Vector3 originalPosition)
        {
            int dist = MenuHandler.PingMenu.GetSliderValue("Distance");
            List<Vector3> points = new List<Vector3>();
            switch (shape)
            {
                case "Square":
                case "Cross":
                    if(shape == "Cross")
                        points.Add(originalPosition);

                    points.Add(originalPosition + new Vector3(dist, dist, 0));
                    points.Add(originalPosition + new Vector3(-dist, dist, 0));
                    points.Add(originalPosition + new Vector3(dist, -dist, 0));
                    points.Add(originalPosition + new Vector3(-dist, -dist, 0));
                    break;
                case "Diamond":
                case "Plus Sign":
                    if (shape == "Plus Sign")
                        points.Add(originalPosition);

                    points.Add(originalPosition + new Vector3(dist, 0, 0));
                    points.Add(originalPosition + new Vector3(0, dist, 0));
                    points.Add(originalPosition + new Vector3(0, -dist, 0));
                    points.Add(originalPosition + new Vector3(-dist, 0, 0));
                    break;
                case "Vertical Line":
                    points.Add(originalPosition);
                    points.Add(originalPosition + new Vector3(0, dist, 0));
                    points.Add(originalPosition + new Vector3(0, dist * 2, 0));
                    points.Add(originalPosition + new Vector3(0, -dist, 0));
                    points.Add(originalPosition + new Vector3(0, -dist * 2, 0));
                    break;
                case "Horizontal Line":
                    points.Add(originalPosition);
                    points.Add(originalPosition + new Vector3(dist, 0, 0));
                    points.Add(originalPosition + new Vector3(dist * 2, 0, 0));
                    points.Add(originalPosition + new Vector3(-dist, 0, 0));
                    points.Add(originalPosition + new Vector3(-dist * 2, 0, 0));
                    break;
                case "Triangle":
                case "Pentagon":
                    int numPoints = ((shape == "Triangle") ? 3 : 5);
                    double constant = Math.PI / 2 - Math.PI / numPoints;
                    for (var i = 0; i < numPoints; i++)
                    {
                        Vector3 v = new Vector3(
                            (float)(originalPosition.X + dist * Math.Cos(i * 2 * Math.PI / numPoints + constant)),
                            (float)(originalPosition.Y + dist * Math.Sin(i * 2 * Math.PI / numPoints + constant)), 0);
                        points.Add(v);
                    }
                    break;
                case "Arrow to Player":
                case "Arrow away from Player":
                    points.Add(originalPosition);
                    points.Add(originalPosition.Extend(Player.Instance, (shape.Contains("away")?-dist:dist)).To3D());
                    Vector2 ExtendedPosition = originalPosition.Extend(Player.Instance, (shape.Contains("away") ? -dist : dist) * 2);
                    points.Add(ExtendedPosition.To3D());
                    Geometry.Polygon.Sector sec = new Geometry.Polygon.Sector(originalPosition, ExtendedPosition.To3D(), MathUtil.DegreesToRadians(45), dist * 1.5f);
                    points.Add(sec.Points[1].To3D());
                    points.Add(sec.Points.Last().To3D());
                    break;
                case "Single":
                default:
                    points.Add(originalPosition);
                    break;
            }

            return points;
        }
    }
}