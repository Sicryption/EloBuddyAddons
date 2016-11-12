using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Linq;

namespace UnsignedMinigames
{
    internal class Program
    {
        public static bool isMouseDown = false;
        public static Texture dragObject = null;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            MenuHandler.Initialize();
            DrawManager.InitializeSprites();

            Game.OnTick += Game_OnTick;
            Game.OnWndProc += Game_OnWndProc;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (Player.Instance.IsDead || MenuHandler.GetCheckboxValue(MenuHandler.Settings, "Draw when Alive"))
            {
                if (args.Msg == (uint)WindowMessages.LeftButtonDown)
                    isMouseDown = true;
                else if (args.Msg == (uint)WindowMessages.LeftButtonUp)
                    isMouseDown = false;

                Texture HoverTexture = DrawManager.activeTextures.Where(a => a.IsMouseOver()).LastOrDefault();

                if (HoverTexture != null && HoverTexture.Type == Texture.ObjectType.Button)
                {
                    Button activeButton = ((Button)HoverTexture);
                    activeButton.OnKeyPress(args);
                }

                if (dragObject != null && args.Msg == (uint)WindowMessages.MouseMove && isMouseDown)
                    ((Button)dragObject).OnKeyPress(args);
                else if (dragObject != null && !isMouseDown)
                    dragObject = null;
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if(Player.Instance.IsDead || MenuHandler.GetCheckboxValue(MenuHandler.Settings, "Draw when Alive"))
                DrawManager.Draw();
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead || MenuHandler.GetCheckboxValue(MenuHandler.Settings, "Draw when Alive"))
            {
                if (DrawManager.MenuState == DrawManager.HUDState.InGameSnake)
                    Snake.HandleTick();
            }
        }
    }
}