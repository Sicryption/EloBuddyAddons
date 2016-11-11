using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using UnsignedMinigames.Properties;
using SharpDX;

namespace UnsignedMinigames
{
    static class Extensions
    {
        public static void Draw(this Texture texture)
        {
            texture.Sprite.Draw(texture.getTexturePosition());
        }

        public static Vector2 getTexturePosition(this Texture texture, DrawManager.ImagePosition overrideOrientation = DrawManager.ImagePosition.Default)
        {
            Vector2 Position = texture.Position;
            DrawManager.ImagePosition orientation = (overrideOrientation == DrawManager.ImagePosition.Default) ? texture.Orientation : overrideOrientation;
            
            switch (orientation)
            {
                case DrawManager.ImagePosition.TopLeft:
                    //no change
                    break;
                case DrawManager.ImagePosition.TopCenter:
                    Position -= new Vector2(texture.Image.Width / 2, 0);
                    break;
                case DrawManager.ImagePosition.TopRight:
                    Position -= new Vector2(texture.Image.Width, 0);
                    break;
                case DrawManager.ImagePosition.MiddleLeft:
                    Position -= new Vector2(0, texture.Image.Height / 2);
                    break;
                case DrawManager.ImagePosition.Center:
                    Position -= new Vector2(texture.Image.Width / 2, texture.Image.Height / 2);
                    break;
                case DrawManager.ImagePosition.MiddleRight:
                    Position -= new Vector2(texture.Image.Width, texture.Image.Height / 2);
                    break;
                case DrawManager.ImagePosition.BottomLeft:
                    Position -= new Vector2(0, texture.Image.Height);
                    break;
                case DrawManager.ImagePosition.BottomCenter:
                    Position -= new Vector2(texture.Image.Width / 2, texture.Image.Height);
                    break;
                case DrawManager.ImagePosition.BottomRight:
                    Position -= new Vector2(texture.Image.Width, texture.Image.Height);
                    break;
            }

            if(overrideOrientation != DrawManager.ImagePosition.Default)
                return Position - (Position - texture.getTexturePosition());

            return Position;
        }

        public static Vector2 Width(this Texture texture)
        {
            return new Vector2(texture.Image.Width, 0);
        }
        public static bool IsMouseOver(this Texture texture)
        {
            if (texture.SpriteRectange().IsInside(Game.CursorPos2D))
                return true;

            return false;
        }

        public static System.Drawing.Rectangle SpriteRectange(this Texture texture)
        {
            return new System.Drawing.Rectangle(texture.getTexturePosition().ToPoint(), texture.Image.Size);
        }

        public static System.Drawing.Point ToPoint(this Vector2 position)
        {
            return new System.Drawing.Point((int)position.X, (int)position.Y);
        }

        public static bool IsInside(this System.Drawing.Rectangle rect, Vector2 position)
        {
            if (position.X >= rect.X &&
                position.Y >= rect.Y &&
                position.X <= rect.X + rect.Width &&
                position.Y <= rect.Y + rect.Height)
                return true;
            return false;
        }
        public static bool IsInside(this Texture self, Vector2 position)
        {
            return new System.Drawing.Rectangle((int)self.Position.X - (self.Image.Width / 2), (int)self.Position.Y, self.Image.Width, self.Image.Height).IsInside(position);
        }

        public static Vector2 Move(this Vector2 pos, Snake.Direction direction, int size)
        {
            Vector2 newPos = pos;

            if (direction == Snake.Direction.North)
                newPos -= new Vector2(0, size);
            else if (direction == Snake.Direction.South)
                newPos += new Vector2(0, size);
            else if(direction == Snake.Direction.West)
                newPos -= new Vector2(size, 0);
            else if(direction == Snake.Direction.East)
                newPos += new Vector2(size, 0);

            return newPos;
        }
        public static float Distance(this Vector2 self, Vector2 pos)
        {
            return (float)Math.Sqrt(Math.Pow(self.X - pos.X, 2) + Math.Pow(self.Y - pos.Y, 2));
        }
    }
}
