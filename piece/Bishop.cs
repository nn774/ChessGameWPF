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

        public override bool CanMove(int xMove, int yMove, Piece[,] Board, bool isChecking = false)
        {
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            return base.Move(xM, yM, Board);
        }
    }
}
