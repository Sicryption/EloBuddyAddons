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
    class Snake
    {
        static Random random = new Random();
        static int snakePieceSize = 25;
        static float lastTick = 0;

        public enum Direction
        {
            North,
            South,
            East,
            West,
            None
        }

        static List<Vector2> snakePositions = new List<Vector2>();
        static Vector2 eggPosition;
        static Direction direction = Direction.None;

        public static void StartGame()
        {
            snakePositions = new List<Vector2>();
            for (int i = 0; i < 15; i++)
                snakePositions.Add(new Vector2(-snakePieceSize * i, 50));
            direction = Direction.None;
            eggPosition = GetNewEggPosition();
        }

        public static void DoDrawings()
        {
            Texture background = DrawManager.CreateTexture("SnakeBackground", "Background", DrawManager.TopCenter + new Vector2(0, 100), DrawManager.ImagePosition.TopCenter);
            Button windowMover = DrawManager.CreateButton("SnakeMoveWindow", "WindowMover",
                background.Position - new Vector2(0, 25), DrawManager.ImagePosition.TopCenter);

            windowMover.OnMouseMove += delegate { MoveWindow(windowMover); };

            foreach (Vector2 pos in snakePositions)
                DrawManager.CreateTexture("SnakePiece", "Snake Piece", background.Position + pos, DrawManager.ImagePosition.TopLeft);

            Texture txt = DrawManager.CreateTexture("SnakePiece", "Egg", background.Position + eggPosition, DrawManager.ImagePosition.TopLeft);
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
            if (Game.Time - lastTick < 0.01f * MenuHandler.GetSliderValue(MenuHandler.SnakeMenu, "Game Speed"))
                return;

            lastTick = Game.Time;

            if(direction != Direction.None)
                DoMovement();
        }

        public static void DoMovement()
        {
            Vector2 nextPosition = snakePositions.First().Move(direction, snakePieceSize);
            Texture background = DrawManager.GetTexture("Background");
            
            //allow the snake to go through the borders of the screen
            //this if statement checks if it is still within the screen border. if not, it needs to put the snake on the other side
            if (!new System.Drawing.Rectangle(
                background.getTexturePosition(DrawManager.ImagePosition.TopLeft)
                .ToPoint(), new Size(background.Image.Width - snakePieceSize, background.Image.Height - snakePieceSize))
                .IsInside(nextPosition + background.Position))
            {
                if (direction == Direction.West)
                    nextPosition.X = (background.getTexturePosition(DrawManager.ImagePosition.TopLeft) - background.Position).X + background.Image.Width - snakePieceSize;
                if (direction == Direction.South)
                    nextPosition.Y = 0;
                if (direction == Direction.East)
                    nextPosition.X = (background.getTexturePosition(DrawManager.ImagePosition.TopLeft) - background.Position).X;
                if (direction == Direction.North)
                    nextPosition.Y = background.Image.Height - snakePieceSize;
            }

            //try to move the snake or destroy it
            if (!WillBeDestroyed(nextPosition))
            {
                if (eggPosition.Distance(nextPosition) < 25)
                {
                    snakePositions.Add(snakePositions[snakePositions.Count() - 1]);
                    eggPosition = GetNewEggPosition();

                    DrawManager.activeTextures.RemoveAll(new Predicate<Texture>(a => a.DisplayName == "Egg"));
                    DrawManager.CreateTexture("SnakePiece", "Egg", background.Position + eggPosition, DrawManager.ImagePosition.TopLeft);
                }

                //ends at 1 so it skips the first snake piece to override later
                for (int i = snakePositions.Count() - 1; i > 0; i--)
                    snakePositions[i] = snakePositions[i - 1];
                snakePositions[0] = nextPosition;

                //remove all active snakes
                DrawManager.activeTextures.RemoveAll(new Predicate<Texture>(a => a.DisplayName == "Snake Piece"));

                //draw the new ones 
                foreach (Vector2 pos in snakePositions)
                    DrawManager.CreateTexture("SnakePiece", "Snake Piece", 
                        background.Position + pos, DrawManager.ImagePosition.TopLeft);

            }
            else
            {
                Chat.Print("You lose! Your snake was " + snakePositions.Count() + " in length with a speed setting of " + MenuHandler.GetSliderValue(MenuHandler.SnakeMenu, "Game Speed"));
                DrawManager.MenuState = DrawManager.HUDState.MainMenu;
            }
        }

        public static Vector2 GetNewEggPosition()
        {
            Vector2 test = new Vector2(random.Next(-12, 12) * 25, random.Next(0, 19) * 25);

            if (snakePositions.Contains(test))
                test = GetNewEggPosition();

            return test;
        }

        public static bool WillBeDestroyed(Vector2 endPosition)
        {
            if (snakePositions.Contains(endPosition))
                return true;

            return false;
        }
        
        public static void SetDirection(Direction dir)
        {
            Direction absoluteCurrentDirection = Direction.None;
            if (snakePositions[0].Y > snakePositions[1].Y)
                absoluteCurrentDirection = Direction.South;
            if (snakePositions[0].Y < snakePositions[1].Y)
                absoluteCurrentDirection = Direction.North;
            if (snakePositions[0].X > snakePositions[1].X)
                absoluteCurrentDirection = Direction.East;
            if (snakePositions[0].X < snakePositions[1].X)
                absoluteCurrentDirection = Direction.West;
            
            //stops player from going backwards into itself
            if (
                (dir == Direction.East && absoluteCurrentDirection == Direction.West) ||
                (dir == Direction.West && absoluteCurrentDirection == Direction.East) ||
                (dir == Direction.North && absoluteCurrentDirection == Direction.South) ||
                (dir == Direction.South && absoluteCurrentDirection == Direction.North) ||
                WillBeDestroyed(snakePositions.First().Move(dir, snakePieceSize))
                )
                return;

            direction = dir;
        }
    }
}
