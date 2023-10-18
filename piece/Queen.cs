using ChessGameWPF.Enum;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ChessGameWPF.piece
{
    internal class Queen : Piece
    {
        public override pieceName PieceName => pieceName.Queen;

        public override bool CanMove(int xM, int yM, Piece[,] Board, bool isChecking = false)
        {
            if (!isChecking)
                if (base.CanMove(xM, yM, Board))
                    return false;

            int deltaX = Math.Abs(xM - x);
            int deltaY = Math.Abs(yM - y);

            if (deltaX == 0 || deltaY == 0 || deltaX == deltaY)
            {
                int xDirection = xM > x ? 1 : (xM < x ? -1 : 0);
                int yDirection = yM > y ? 1 : (yM < y ? -1 : 0);

                int currentX = x + xDirection;
                int currentY = y + yDirection;

                while (currentX != xM || currentY != yM)
                {
                    if (Board[currentX, currentY].PieceName != pieceName.Empty)
                    {
                        return false;
                    }

                    currentX += xDirection;
                    currentY += yDirection;
                }
                if (Board[xM, yM].PieceName == pieceName.Empty || Board[xM, yM].Color != Color)
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
