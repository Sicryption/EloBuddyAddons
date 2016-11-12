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
    class Texture
    {
        public enum ObjectType
        {
            Texture,
            Button
        }

        public ObjectType Type;
        public Sprite Sprite;
        public Bitmap Image;
        public string Name,
            DisplayName;
        public Vector2 Position;
        public DrawManager.ImagePosition Orientation;

        public Texture()
        {
            //blank initializer
        }

        public Texture(string name, Sprite sprite, Bitmap image)
        {
            Sprite = sprite;
            Image = image;
            Name = name;
            Type = ObjectType.Texture;
        }

        public Texture(string name, string displayName, Sprite sprite, Bitmap image, Vector2 position, DrawManager.ImagePosition orientation)
        {
            Sprite = sprite;
            DisplayName = displayName;
            Position = position;
            Orientation = orientation;
            Image = image;
            Name = name;
            Type = ObjectType.Texture;
        }
    }
}
