﻿using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWPF.piece
{
    internal class King : Piece
    {
        public override pieceName PieceName => pieceName.King;

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