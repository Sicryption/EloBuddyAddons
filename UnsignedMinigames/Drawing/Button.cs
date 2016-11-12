using EloBuddy;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsignedMinigames
{
    class Button : Texture
    {
        public bool isBeingClicked = false;

        public event Action OnMouseDown;
        public event Action OnMouseRelease;
        public event Action OnMouseMove;

        public void OnKeyPress(WndEventArgs args)
        {
            if (args.Msg == (uint)WindowMessages.LeftButtonDown)
            {
                if(OnMouseDown != null)
                    OnMouseDown();
                isBeingClicked = true;
                if (MenuHandler.GetCheckboxValue(MenuHandler.Settings, "Debug Click Actions"))
                    Console.WriteLine(DisplayName + " had left click pushed down on it.");
            }
            else if (args.Msg == (uint)WindowMessages.LeftButtonUp)
            {
                if (OnMouseRelease != null)
                    OnMouseRelease();
                isBeingClicked = false;
                if(MenuHandler.GetCheckboxValue(MenuHandler.Settings, "Debug Click Actions"))
                    Console.WriteLine(DisplayName + " had left click released on it.");
            }
            else if (args.Msg == (uint)WindowMessages.MouseMove)
            {
                if (OnMouseMove != null)
                    OnMouseMove();
                if (MenuHandler.GetCheckboxValue(MenuHandler.Settings, "Debug Hover Actions"))
                    Console.WriteLine(DisplayName + " had the mouse move over it.");
            }
        }

        public Button()
        {
            //blank initializer
        }

        public Button(Texture texture)
        {
            Sprite = texture.Sprite;
            DisplayName = texture.DisplayName;
            Image = texture.Image;
            Name = texture.Name;
            Position = texture.Position;
            Orientation = texture.Orientation;
            
            Type = ObjectType.Button;
        }
    }
}
