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

        public override bool CanMove(int xM, int yM, Piece[,] board)
        {
            int xx = Math.Abs(x - xM);
            int yy = Math.Abs(y - yM);
            if ((xx == 2 && yy == 1) || (xx == 1 && yy == 2))
                if (board[xM, yM].Color != board[x, y].Color)
                    return true;
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board)
        {
            return base.Move(xM, yM, Board);
        }
    }
}
