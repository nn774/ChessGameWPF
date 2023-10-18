using ChessGameWPF.Enum;
using System;
using System.Windows.Controls;

namespace ChessGameWPF.piece
{
    public class Pawn : Piece
    {
        public static pieceName newName = pieceName.Empty;
        public override pieceName PieceName => pieceName.Pawn;
        public bool CanEnPassant { get; set; } = false;
        public bool IsEnPassantLeft { get; set; } = true;

        public override bool CanMove(int xM, int yM, Piece[,] Board, bool isChecking = false)
        {
            if(!isChecking)
                if (base.CanMove(xM, yM, Board))
                    return false;

            int deltaX = xM - x;
            int deltaY = Math.Abs(yM - y);
            if (Color == color.black)
            {
                if (deltaX == 1 && deltaY == 0 && Board[xM, yM].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == 1 && deltaY == 1 && Board[xM, yM].PieceName == pieceName.Empty &&
                   Board[x, yM] is Pawn && this.CanEnPassant)
                {
                    if (!IsEnPassantLeft && yM - y == -1)
                        return true;
                    else if (IsEnPassantLeft && yM - y == 1)
                        return true;
                }
                else if (!HasMoved && deltaX == 2 && deltaY == 0 && Board[xM, yM].PieceName == pieceName.Empty &&
                    Board[x + 1, y].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == 1 && deltaY == 1 && Board[xM, yM].Color == color.white)
                    return true;
            }
            else
            {
                if (deltaX == -1 && deltaY == 0 && Board[xM, yM].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == -1 && deltaY == 1 && Board[xM, yM].PieceName == pieceName.Empty &&
                   Board[x, yM] is Pawn && this.CanEnPassant)
                {
                    if(!IsEnPassantLeft && yM-y == -1)
                        return true;
                    else if (IsEnPassantLeft && yM - y == 1)
                        return true;
                }
                else if (!HasMoved && deltaX == -2 && deltaY == 0 && Board[xM, yM].PieceName == pieceName.Empty &&
                    Board[x - 1, y].PieceName == pieceName.Empty)
                    return true;
                else if (deltaX == -1 && deltaY == 1 && Board[xM, yM].Color == color.black)
                    return true;
            }
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            if (CanEnPassant && Math.Abs(yM - y) == 1)
            { 
                Board = ChessBoard.CreatePieces(pieceName.Empty, color.none, x, yM, Board);
                if (isRealMove)
                    ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + yM] as Button, Board[x, yM].PieceName, Board[x, yM].Color);
            }
            base.Move(xM, yM, Board, isRealMove);
            if (isRealMove)
            {
                if (!HasMoved && Math.Abs(xM - x) == 2)
                {
                    if (yM != 0)
                        if (Board[xM, yM - 1].PieceName == pieceName.Pawn && Board[xM, yM - 1].Color != Color)
                        {
                            (Board[xM, yM - 1] as Pawn).CanEnPassant = true;
                            (Board[xM, yM - 1] as Pawn).IsEnPassantLeft = true;
                            ChessBoard.isHaveEnPassanto = true;
                        }
                    if (yM != 7)
                        if (Board[xM, yM + 1].PieceName == pieceName.Pawn && Board[xM, yM + 1].Color != Color)
                        {
                            (Board[xM, yM + 1] as Pawn).CanEnPassant = true;
                            (Board[xM, yM + 1] as Pawn).IsEnPassantLeft = false;
                            ChessBoard.isHaveEnPassanto = true;
                        }
                }
                HasMoved = true;
            }
            Board = ChessBoard.CreatePieces(pieceName.Empty, color.none, x, y, Board);
            Board = ChessBoard.CreatePieces(Board[xM, yM].PieceName, Board[xM, yM].Color, xM, yM, Board, HasMoved, CanEnPassant);

            if (isRealMove)
            {
                ChessBoard.EditButton(ChessBoard.grid.Children[x * 8 + y] as Button, Board[x, y].PieceName, Board[x, y].Color);
                if (xM == 0 || xM == 7)
                {
                    TransformationWIn win = new TransformationWIn();
                    TransformationWIn.xM = xM;
                    TransformationWIn.yM = yM;
                    TransformationWIn.Color = Color;
                    win.ShowDialog();
                    ChessBoard.CreatePieces(newName, Color, xM, yM, Board);
                }
                ChessBoard.EditButton(ChessBoard.grid.Children[xM * 8 + yM] as Button, Board[xM, yM].PieceName, Board[xM, yM].Color);
            }
               
            return Board;
        }
    }
}
