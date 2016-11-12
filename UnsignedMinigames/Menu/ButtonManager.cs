using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsignedMinigames
{
    class ButtonManager
    {
        public static void LoadSnake()
        {
            DrawManager.MenuState = DrawManager.HUDState.InGameSnake;
            Snake.StartGame();
        }
        public static void LoadTetris()
        {
            DrawManager.MenuState = DrawManager.HUDState.InGameTetris;
            TetrisGame.StartGame();
        }
    }
}
