using ChessGameWPF.Enum;
using ChessGameWPF.piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChessGameWPF.piece
{
    internal class Pawn : Piece
    {
        public override pieceName PieceName => pieceName.Pawn;
        public bool CanEnPassant { get; set; } = false;
        public bool isFirstMove { get; set; } = true;

        public override bool CanMove(int xM, int yM, Piece[,] board)
        {
            int deltaX = xM - x;
            int deltaY = Math.Abs(yM - y);
            if (Color == color.black)
            {
                if (deltaX == 1 && deltaY == 0 && board[xM, yM].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == 1 && deltaY == 1 && board[xM, yM].PieceName == pieceName.Empty &&
                   board[x, yM] is Pawn && this.CanEnPassant)
                    return true;
                else if (isFirstMove && deltaX == 2 && deltaY == 0 && board[xM, yM].PieceName == pieceName.Empty &&
                    board[x + 1, y].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == 1 && deltaY == 1 && board[xM, yM].Color == color.white)
                    return true;
            }
            else
            {
                if (deltaX == -1 && deltaY == 0 && board[xM, yM].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == -1 && deltaY == 1 && board[xM, yM].PieceName == pieceName.Empty &&
                   board[x, yM] is Pawn && this.CanEnPassant)
                    return true;
                else if (isFirstMove && deltaX == -2 && deltaY == 0 && board[xM, yM].PieceName == pieceName.Empty &&
                    board[x - 1, y].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == -1 && deltaY == 1 && board[xM, yM].Color == color.black)
                    return true;
            }
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board)
        {
            if (CanEnPassant && Math.Abs(yM - y) == 1)
            {
                ChessBoard.grid.Children.Remove(Board[x, yM].Button);
                Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, yM, (Piece[,])Board.Clone());
                ChessBoard.grid.Children.Add(Board[x, yM].Button);
            }
            base.Move(xM, yM, Board);
            if (isFirstMove && Math.Abs(xM - x) == 2)
            {
                if (Board[xM, yM - 1].PieceName == pieceName.Pawn)
                {
                    (Board[xM, yM - 1] as Pawn).CanEnPassant = true;
                    ChessBoard.isHaveEnPassanto = true;
                }
                if (Board[xM, yM + 1].PieceName == pieceName.Pawn)
                {
                    (Board[xM, yM + 1] as Pawn).CanEnPassant = true;
                    ChessBoard.isHaveEnPassanto = true;
                }
            }
            isFirstMove = false;
            Board = ChessBoard.CreateButton(Board[xM, yM].PieceName, Board[xM, yM].Color, xM, yM, (Piece[,])Board.Clone(), CanEnPassant, isFirstMove);
            Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, y, (Piece[,])Board.Clone());
            ChessBoard.grid.Children.Add(Board[xM, yM].Button);
            ChessBoard.grid.Children.Add(Board[x, y].Button);
            return Board;
        }
    }
}
