using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ChessGameWPF.piece
{
    abstract class Piece : ICloneable
    {
        public int x { get; set; }
        public int y { get; set; }
        public color Color { get; set; }
        public virtual pieceName PieceName { get; }
        public Button? Button { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual bool CanMove(int xM, int yM, Piece[,] board)
        {
            return false;
        }

        private void ClearEnPassanto(Piece[,] Board)
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

        public virtual Piece[,] Move(int xM, int yM, Piece[,] Board)
        {
            if (ChessBoard.isHaveEnPassanto)
                ClearEnPassanto(Board);

            ChessBoard.grid.Children.Remove(Board[x, y].Button);
            ChessBoard.grid.Children.Remove(Board[xM, yM].Button);
            Board[xM, yM] = (Piece)this.Clone();
            Board[xM, yM].x = xM;
            Board[xM, yM].y = yM;
            if (this.PieceName != pieceName.Pawn)
            {
                Board = ChessBoard.CreateButton(Board[xM, yM].PieceName, Board[xM, yM].Color, xM, yM, (Piece[,])Board.Clone());
                Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, y, (Piece[,])Board.Clone());
                ChessBoard.grid.Children.Add(Board[xM, yM].Button);
                ChessBoard.grid.Children.Add(Board[x, y].Button);
            }
            return Board;
        }
        
        public void showMoves(int x, int y)
        {
            List<Moves> list = new List<Moves>();
            for (int i = 0; i < ChessBoard.Board.GetLength(0); i++)
            {
                for (int l = 0; l < ChessBoard.Board.GetLength(1); l++)
                {
                    if (ChessBoard.Board[x,y].CanMove(i, l, (Piece[,])ChessBoard.Board.Clone()))
                    {
                        list.Add(new Moves { x = i, y = l });
                    }
                }
            }
            foreach (var item in list)
            {
                ChessBoard.Board[item.x, item.y].Button.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }
    }
}
