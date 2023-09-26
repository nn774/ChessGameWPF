using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        public virtual bool tryMove(int xM, int yM, Piece[,] board) 
        {
            return false;
        }

        public virtual void Move(int xM, int yM)
        {
            ChessBoard.grid.Children.Remove(ChessBoard.Board[x, y].Button);
            ChessBoard.grid.Children.Remove(ChessBoard.Board[xM, yM].Button);
            ChessBoard.Board[xM, yM] = (Piece)this.Clone();
            ChessBoard.Board[x, y] = new Empty { x = x, y = y, Color = color.none};
            ChessBoard.Board[xM, yM].x = xM;
            ChessBoard.Board[xM, yM].y = yM;
            ChessBoard.grid.Children.Add(ChessBoard.CreateButton(ChessBoard.Board[xM, yM].PieceName, ChessBoard.Board[xM, yM].Color, xM, yM));
            ChessBoard.grid.Children.Add(ChessBoard.CreateButton(pieceName.Empty, color.none, x, y));
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
