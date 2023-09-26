using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessGameWPF.piece
{
    abstract class Piece
    {
        public int x { get; set; }
        public int y { get; set; }
        public color Color { get; set; }
        public virtual pieceName PieceName { get; }

        public virtual bool CanMove(int xM, int yM, Piece[,] board)
        {
            return false;
        }

        public virtual bool Move(int xM, int yM, Piece[,] board) 
        {
            return false;
        }
    }
}
