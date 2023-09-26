using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWPF.piece
{
    internal class Bishop : Piece
    {
        public override pieceName PieceName => pieceName.Bishop;

        public override bool CanMove(int xM, int yM, Piece[,] Board, bool isChecking = false)
        {
            if (!isChecking)
                if (base.CanMove(xM, yM, Board))
                    return false;

            int deltaX = Math.Abs(xM - x);
            int deltaY = Math.Abs(yM - y);

            if (deltaX == deltaY)
            {
                int stepX = Math.Sign(xM - x);
                int stepY = Math.Sign(yM - y);
                int currentX = x + stepX;
                int currentY = y + stepY;

                while (currentX != xM && currentY != yM)
                {
                    if (Board[currentX, currentY].PieceName != pieceName.Empty)
                        return false;
                    currentX += stepX;
                    currentY += stepY;
                }

                if (Board[xM, yM].Color != Color)
                    return true;
            }
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            return base.Move(xM, yM, Board, isRealMove);
        }
    }
}
