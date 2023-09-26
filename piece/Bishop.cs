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

        public override bool CanMove(int xMove, int yMove, Piece[,] board)
        {
            return false;
        }
        public override bool Move(int xM, int yM, Piece[,] board)
        {
            return false;
        }
    }
}
