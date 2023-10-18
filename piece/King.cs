using ChessGameWPF.boardSctripts;
using ChessGameWPF.Enum;
using System;
using System.Windows.Controls;

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
                    for (int i = y + 1; i <= yM; i++)
                    {
                        if (Board[x, i] != null && Board[x, i].PieceName != pieceName.Empty)
                        {
                            return false;
                        }
                    }
                    if (Board[x, 7] is Rook && !Board[x, 7].HasMoved)
                    {
                        if (!Checkings.IsInCheck(Color, Board, x, y) && !Checkings.IsInCheck(Color, Board, x, y + 1))
                        {
                            return true;
                        }
                    }
                }
                else if (yM < y)
                {
                    for (int i = y - 1; i >= yM-1; i--)
                    {
                        if (Board[x, i] != null && Board[x, i].PieceName != pieceName.Empty)
                        {
                            return false;
                        }
                    }

                    if (Board[x, 0] is Rook && !Board[x, 0].HasMoved)
                    {
                        if (!Checkings.IsInCheck(Color, Board, x, y) && !Checkings.IsInCheck(Color, Board, x, y - 1))
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

                        ChessBoard.CreatePieces(Board[x, 7].PieceName, Board[x, 7].Color, x, 5, Board, true);
                        ChessBoard.CreatePieces(PieceName , Color, x, 6, Board, true);
                        ChessBoard.CreatePieces(pieceName.Empty, color.none, x, 7, Board);
                        ChessBoard.CreatePieces(pieceName.Empty, color.none, x, 4, Board);

                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 7] as Button, Board[x, 7].PieceName, Board[x, 7].Color);
                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 5] as Button, Board[x, 5].PieceName, Board[x, 5].Color);
                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 6] as Button, Board[x, 6].PieceName, Board[x, 6].Color);
                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 4] as Button, Board[x, 4].PieceName, Board[x, 4].Color);
                    }
                    else
                    {
                        ChessBoard.CreatePieces(Board[x, 0].PieceName, Board[x, 0].Color, x, 3, Board);
                        ChessBoard.CreatePieces(PieceName, Color, x, 2, Board, true);
                        ChessBoard.CreatePieces(pieceName.Empty, color.none, x, 0, Board);
                        ChessBoard.CreatePieces(pieceName.Empty, color.none, x, 4, Board);

                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 0] as Button, Board[x, 0].PieceName, Board[x, 0].Color);
                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 2] as Button, Board[x, 2].PieceName, Board[x, 2].Color);
                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 3] as Button, Board[x, 3].PieceName, Board[x, 3].Color);
                        ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + 4] as Button, Board[x, 4].PieceName, Board[x, 4].Color);
                    }
                    if (ChessBoard.now == color.white)
                        ChessBoard.now = color.black;
                    else
                        ChessBoard.now = color.white;
                    this.ClearEnPassanto(Board);
                    return Board;
                }
            }

            return base.Move(xM, yM, Board, isRealMove);
        }
    }
}
