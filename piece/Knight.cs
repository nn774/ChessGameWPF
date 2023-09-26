using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessGameWPF.piece
{
    internal class Knight : Piece
    {
        public override pieceName PieceName => pieceName.Knight;

        public override bool CanMove(int xM, int yM, Piece[,] Board, bool isChecking = false)
        {
            if (!isChecking)
                if (base.CanMove(xM, yM, Board))
                    return false;

            int xx = Math.Abs(x - xM);
            int yy = Math.Abs(y - yM);
            if ((xx == 2 && yy == 1) || (xx == 1 && yy == 2))
                if (Board[xM, yM].Color != Board[x, y].Color)
                    return true;
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            return base.Move(xM, yM, Board, isRealMove);
        }
    }
}
