using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using UnsignedMinigames.Properties;
using SharpDX;

namespace UnsignedMinigames
{
    class DrawManager
    {
        private static SharpDX.Direct3D9.Font font = new SharpDX.Direct3D9.Font(Drawing.Direct3DDevice, new System.Drawing.Font("Arial", 40));
        public enum HUDState
        {
            MainMenu,
            InGameSnake
        }
        public static HUDState MenuState = HUDState.MainMenu,
            lastMenuState;

        public enum ImagePosition
        {
            TopLeft,
            TopCenter,
            TopRight,
            MiddleLeft,
            Center,
            MiddleRight,
            BottomLeft,
            BottomCenter,
            BottomRight,
            Default
        }

        private static TextureLoader tl = new TextureLoader();
        public static List<Texture> textures = new List<Texture>(),
            activeTextures = new List<Texture>();
        public static List<Tuple<string, Vector2>> textList = new List<Tuple<string, Vector2>>();

        public static void InitializeSprites()
        {
            AddTexture("Settings", Resources.Settings);
            AddTexture("Shop", Resources.ShopIcon);
            AddTexture("Dummy", Resources.DummyIcon);
            AddTexture("Back Arrow", Resources.Back_Arrow);
            AddTexture("Buy", Resources.Buy);
            AddTexture("Sell", Resources.Sell);
            AddTexture("Forge", Resources.Forge);
            AddTexture("Snake", Resources.Snake);
            AddTexture("SnakeBackground", Resources.SnakeBackground);
            AddTexture("SnakePiece", Resources.SnakePiece);
            AddTexture("SnakeMoveWindow", Resources.SnakeMoveWindow);
            CreateScreen();
        }

        public static void Draw()
        {
            if (MenuState != lastMenuState)
            {
                CreateScreen();
                lastMenuState = MenuState;
            }

            foreach (Texture texture in activeTextures)
                texture.Draw();
            
            foreach (Tuple<string, Vector2> text in textList)
                DrawText(text.Item1, text.Item2);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Settings, "Draw Element Names"))
            {
                Texture HoverTexture = activeTextures.Where(a => a.IsMouseOver()).LastOrDefault();
                if (HoverTexture != null)
                    Drawing.DrawText(Center, System.Drawing.Color.Blue, HoverTexture.DisplayName, 15);
            }
        }

        public static void CreateScreen()
        {
            activeTextures = new List<Texture>();

            if (MenuState == HUDState.MainMenu)
            {
                Button Snake = CreateButton("Snake", "Play Snake", TopLeft, ImagePosition.TopLeft);
                Snake.OnMouseRelease += delegate { ButtonManager.LoadSnake(); };
            }
            else if(MenuState == HUDState.InGameSnake)
            {
                Snake.DoDrawings();
            }
        }

        public static Texture CreateTexture(string textureName, string displayName, Vector2 position, ImagePosition orientation, bool addToActiveList = true)
        {
            Texture active = textures.Where(a => a.Name == textureName).FirstOrDefault();

            if(active == null)
                active = textures.Where(a => a.Name == "Dummy").FirstOrDefault();
            
            Texture instance = new Texture(active.Name, displayName, active.Sprite, active.Image, position, orientation);

            if(addToActiveList)
                activeTextures.Add(instance);

            return instance;
        }

        public static Button CreateButton(string textureName, string displayName, Vector2 position, ImagePosition orientation)
        {
            Texture texture = CreateTexture(textureName, displayName, position, orientation, false);
            Button button = new Button(texture);

            activeTextures.Add(button);

            return button;
        }

        public static void DrawText(string text, Vector2 position)
        {
            font.DrawText(null, text, (int)position.X, (int)position.Y, SharpDX.Color.Black);
        }

        public static void AddTexture(string name, Bitmap image)
        {
            tl.Load(name, image);
            textures.Add(
                new Texture(name,
                    new Sprite(() => tl[name]),
                    image
                    )
                );
        }

        public static Texture GetTexture(string displayName)
        {
            return activeTextures.Where(a => a.DisplayName == displayName).FirstOrDefault();
        }

        #region Positions
        public static readonly Vector2 TopLeft = new Vector2(0, 0),
            TopCenter = new Vector2(Drawing.Width/2, 0),
            TopRight = new Vector2(Drawing.Width, 0),
            MiddleLeft = new Vector2(0, Drawing.Height/2),
            Center = new Vector2(Drawing.Width/2, Drawing.Height/2),
            MiddleRight = new Vector2(Drawing.Width, Drawing.Height/2),
            BottomLeft = new Vector2(0, Drawing.Height),
            BottomCenter = new Vector2(Drawing.Width/2, Drawing.Height),
            BottomRight = new Vector2(Drawing.Width, Drawing.Height);
        #endregion
    }
}
