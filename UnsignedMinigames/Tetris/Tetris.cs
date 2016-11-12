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
    class TetrisGame
    {
        static float lastTick = 0;
        public enum Direction
        {
            North,
            South,
            East,
            West,
            None
        }
        
        static Direction direction = Direction.None;

        public static void StartGame()
        {

        }

        public static void DoDrawings()
        {

        }

        public static void MoveWindow(Button move)
        {
            if (move.isBeingClicked)
            {
                Texture mover = DrawManager.GetTexture("WindowMover");
                Vector2 MousePos = Game.CursorPos2D;
                Vector2 backgroundTruePos = mover.Position;
                Vector2 difference = MousePos - backgroundTruePos;

                Program.dragObject = mover;

                foreach (Texture texture in DrawManager.activeTextures)
                    texture.Position = texture.Position + difference - new Vector2(0, mover.Image.Height / 2);
            }
        }

        public static void HandleTick()
        {
            //if it has been within a 1/10th of a second, return
            //this limits how many times the character moves
            if (Game.Time - lastTick < 0.01f * MenuHandler.GetSliderValue(MenuHandler.TetrisMenu, "Game Speed"))
                return;

            lastTick = Game.Time;

            if (direction != Direction.None)
                DoMovement();
        }

        public static void DoMovement()
        {

        }
    }
}
