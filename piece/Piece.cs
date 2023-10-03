using ChessGameWPF.boardSctripts;
using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessGameWPF.piece
{
    abstract class Piece : ICloneable
    {
        public int x { get; set; }
        public int y { get; set; }
        public color Color { get; set; }
        public virtual pieceName PieceName { get; set; }
        public Button? Button { get; set; }
        public virtual bool HasMoved { get; set; } = false;
        ~Piece() 
        {
            Button = null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual bool CanMove(int xM, int yM, Piece[,] Board, bool isChecking = false)
        {
            if (Checkings.IsInCheck(Color, (Piece[,])Move(xM, yM, (Piece[,])Board.Clone(), false).Clone()))
                return true;
            return false;
        }

        public void ClearEnPassanto(Piece[,] Board)
        {
            bool alsohave = false;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int l = 0; l < Board.GetLength(1); l++)
                {
                    if (Board[i, l].PieceName == pieceName.Pawn &&
                        this.Color == Board[i, l].Color)
                        (Board[i, l] as Pawn).CanEnPassant = false;
                    if (Board[i, l].PieceName == pieceName.Pawn)
                        if ((Board[i, l] as Pawn).CanEnPassant)
                            alsohave = true;
                }
            }
            if (alsohave)
                ChessBoard.isHaveEnPassanto = true;
            else
                ChessBoard.isHaveEnPassanto = false;
        }

        public virtual Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            if (ChessBoard.isHaveEnPassanto && isRealMove)
                ClearEnPassanto(Board);
            if (isRealMove)
            {
                if (ChessBoard.now == color.white)
                    ChessBoard.now = color.black;
                else
                    ChessBoard.now = color.white;

                ChessBoard.grid.Children.Remove(Board[xM, yM].Button);
                ChessBoard.grid.Children.Remove(Board[x, y].Button);
            }

            Board[xM, yM] = (Piece)this.Clone();
            
            Board[xM, yM].x = xM;
            Board[xM, yM].y = yM;

           
            if (PieceName != pieceName.Pawn)
            {
                Board = ChessBoard.CreateButton(Board[xM, yM].PieceName, Board[xM, yM].Color, xM, yM, Board, HasMoved);
                Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, y, Board);
            }

            if (PieceName != pieceName.Pawn && isRealMove)
            {
                ChessBoard.grid.Children.Add(Board[xM, yM].Button);
                ChessBoard.grid.Children.Add(Board[x, y].Button);
            }
            return Board;
        }

        public void showMoves(int x, int y)
        {
            List<Moves> list = Gets.GetMoves(x, y, ChessBoard.Board);
            foreach (var item in list)
            {
                ChessBoard.Board[item.x, item.y].Button.BorderBrush = Brushes.Red;
            }
        }

    }
}
