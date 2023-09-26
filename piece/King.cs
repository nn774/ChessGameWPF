using ChessGameWPF.Enum;
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
        public override bool HasMoved { get; set; } = false;

        public override bool CanMove(int xM, int yM, Piece[,] Board, bool isChecking = false)
        {
            if (!isChecking)
                if (base.CanMove(xM, yM, Board))
                    return false;

            int deltaX = Math.Abs(xM - x);
            int deltaY = Math.Abs(yM - y);
            
            if (deltaX <= 1 && deltaY <= 1 )
            {
                if (deltaX == 0 && deltaY == 0)
                    return false;

                if (Board[xM, yM].Color != Color)
                {
                    return true;
                }
            }
            else if (!HasMoved && deltaX == 0 && deltaY == 2 && !isChecking)
            {
                if (yM > y)
                {
                    for (int i = y + 1; i < yM; i++)
                    {
                        if (Board[x, i] != null && Board[x, i].PieceName != pieceName.Empty)
                        {
                            return false;
                        }
                    }
                    if (Board[x, 7] is Rook && !Board[x, 7].HasMoved)
                    {
                        if (!IsInCheck(Color, Board, x, y) && !IsInCheck(Color, Board, x, y + 1))
                        {
                            return true;
                        }
                    }
                }
                else if (yM < y)
                {
                    for (int i = y - 1; i > yM; i--)
                    {
                        if (Board[x, i] != null && Board[x, i].PieceName != pieceName.Empty)
                        {
                            return false;
                        }
                    }

                    if (Board[x, 0] is Rook && !Board[x, 0].HasMoved)
                    {
                        if (!IsInCheck(Color, Board, x, y) && !IsInCheck(Color, Board, x, y - 1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            if (isRealMove)
            {
                HasMoved = true;
                if (Math.Abs(y - yM) == 2)
                {
                    if (yM > y)
                    {
                        ChessBoard.grid.Children.Remove(Board[x, 7].Button);
                        ChessBoard.grid.Children.Remove(Board[x, 5].Button);
                        ChessBoard.grid.Children.Remove(Board[x, 6].Button);
                        ChessBoard.grid.Children.Remove(Board[x, 4].Button);

                        Board = ChessBoard.CreateButton(Board[x, 7].PieceName, Board[x, 7].Color, x, 5, Board, true);
                        Board = ChessBoard.CreateButton(PieceName , Color, x, 6, Board, true);
                        Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, 7, Board );
                        Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, 4, Board);

                        ChessBoard.grid.Children.Add(Board[x, 7].Button);
                        ChessBoard.grid.Children.Add(Board[x, 5].Button);
                        ChessBoard.grid.Children.Add(Board[x, 6].Button);
                        ChessBoard.grid.Children.Add(Board[x, 4].Button);
                    }
                    else
                    {
                        ChessBoard.grid.Children.Remove(Board[x, 0].Button);
                        ChessBoard.grid.Children.Remove(Board[x, 2].Button);
                        ChessBoard.grid.Children.Remove(Board[x, 3].Button);
                        ChessBoard.grid.Children.Remove(Board[x, 4].Button);

                        Board = ChessBoard.CreateButton(Board[x, 0].PieceName, Board[x, 0].Color, x, 3, Board);
                        Board = ChessBoard.CreateButton(PieceName, Color, x, 2, Board, true);
                        Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, 0, Board);
                        Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, 4, Board);

                        ChessBoard.grid.Children.Add(Board[x, 3].Button);
                        ChessBoard.grid.Children.Add(Board[x, 2].Button);
                        ChessBoard.grid.Children.Add(Board[x, 0].Button);
                        ChessBoard.grid.Children.Add(Board[x, 4].Button);
                    }
                    return Board;
                }
            }

            return base.Move(xM, yM, Board, isRealMove);
        }
    }
}
