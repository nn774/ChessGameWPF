using ChessGameWPF.Enum;
using ChessGameWPF.piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWPF.piece
{
    internal class Pawn : Piece
    {
        public override pieceName PieceName => pieceName.Pawn;
        public bool CanEnPassant { get; set; } = false;

        public override bool CanMove(int xMove, int yMove, Piece[,] board)
        {
            return false;
        }
        public override bool tryMove(int xM, int yM, Piece[,] board)
        {
            return false;
        }
        public override void Move(int xM, int yM)
        {

        }
    }
}
