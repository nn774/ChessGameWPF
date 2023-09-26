using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWPF.piece
{
    internal class Queen : Piece
    {
        public override pieceName PieceName => pieceName.Queen;

        public override bool CanMove(int xMove, int yMove, Piece[,] board)
        {
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board)
        {
            return Board;
        }
    }
}
