using ChessGameWPF.Enum;
using ChessGameWPF.piece;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace ChessGameWPF.piece
{
    internal class Pawn : Piece
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
                if (isRealMove)
                    ChessBoard.grid.Children.Remove(Board[x, yM].Button);
                Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, yM, Board);
                if (isRealMove)
                    ChessBoard.grid.Children.Add(Board[x, yM].Button);
            }
            base.Move(xM, yM, Board, isRealMove);
            if (isRealMove)
            {
                if (!HasMoved && Math.Abs(xM - x) == 2)
                {
                    if (yM != 0)
                        if (Board[xM, yM - 1].PieceName == pieceName.Pawn)
                        {
                            (Board[xM, yM - 1] as Pawn).CanEnPassant = true;
                            (Board[xM, yM - 1] as Pawn).IsEnPassantLeft = true;
                            ChessBoard.isHaveEnPassanto = true;
                        }
                    if (yM != 7)
                        if (Board[xM, yM + 1].PieceName == pieceName.Pawn)
                        {
                            (Board[xM, yM + 1] as Pawn).CanEnPassant = true;
                            (Board[xM, yM + 1] as Pawn).IsEnPassantLeft = false;
                            ChessBoard.isHaveEnPassanto = true;
                        }
                }
                HasMoved = true;
            }
            Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, y, Board);
            Board = ChessBoard.CreateButton(Board[xM, yM].PieceName, Board[xM, yM].Color, xM, yM, Board, HasMoved, CanEnPassant);
            if (isRealMove)
            {
                ChessBoard.grid.Children.Add(Board[x, y].Button);
                if (xM == 0 || xM == 7)
                {
                    TransformationWIn win = new TransformationWIn();
                    TransformationWIn.xM = xM;
                    TransformationWIn.yM = yM;
                    TransformationWIn.Color = Color;
                    win.ShowDialog();
                    ChessBoard.grid.Children.Remove(Board[xM, yM].Button);
                    ChessBoard.CreateButton(newName, Color, xM, yM, Board);
                }
            }
            if(isRealMove)
                ChessBoard.grid.Children.Add(Board[xM, yM].Button);
            return Board;
        }
    }
}
