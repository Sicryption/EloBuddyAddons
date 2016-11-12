using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace UnsignedMinigames.Tetris
{
    class Piece
    {
        public enum PieceType
        {
            Straight,
            L,
            BackwardsL,
            Square,
            S,
            BackwardsS,
            W
        }

        //each piece can have 4 or fewer pieces. only fewer when it is destroyed
        List<Texture> pieces = new List<Texture>(4);
        
        public Piece(PieceType type)
        {
            if (type == PieceType.Straight)
                pieces = new List<Texture>()
                {
                    DrawManager.CreateTexture("Tetris - Blue Piece", "Blue Piece", Vector2.Zero, DrawManager.ImagePosition.TopLeft),
                    DrawManager.CreateTexture("Tetris - Blue Piece", "Blue Piece", DrawManager.GetTexture("Tetris - Blue Piece").Width(), DrawManager.ImagePosition.TopLeft),
                    DrawManager.CreateTexture("Tetris - Blue Piece", "Blue Piece", 2 * DrawManager.GetTexture("Tetris - Blue Piece").Width(), DrawManager.ImagePosition.TopLeft),
                    DrawManager.CreateTexture("Tetris - Blue Piece", "Blue Piece", 3 * DrawManager.GetTexture("Tetris - Blue Piece").Width(), DrawManager.ImagePosition.TopLeft),
                };
        }
    }
}
