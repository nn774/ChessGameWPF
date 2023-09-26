using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWPF.piece
{
    internal class Empty : Piece
    {
        public override pieceName PieceName => pieceName.Empty;

        public override bool CanMove(int xMove, int yMove, Piece[,] Board, bool isChecking = false)
        {
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            return Board;
        }
    }
}
