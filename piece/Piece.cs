using ChessGameWPF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ChessGameWPF.piece
{
    abstract class Piece : ICloneable
    {
        public int x { get; set; }
        public int y { get; set; }
        public color Color { get; set; }
        public virtual pieceName PieceName { get; }
        public Button? Button { get; set; }
        public virtual bool HasMoved { get; set; } = false;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual bool CanMove(int xM, int yM, Piece[,] Board, bool isChecking = false)
        {
            if (IsInCheck(Color, (Piece[,])Move(xM, yM, (Piece[,])Board.Clone(), false).Clone()))
                return true;
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

        public virtual Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            if (ChessBoard.isHaveEnPassanto && isRealMove)
                ClearEnPassanto(Board);
            if (isRealMove)
            {
                ChessBoard.grid.Children.Remove(Board[xM, yM].Button);
                ChessBoard.grid.Children.Remove(Board[x, y].Button);
            }
            Board[xM, yM] = (Piece)this.Clone();
            
            Board[xM, yM].x = xM;
            Board[xM, yM].y = yM;

           
            if (PieceName != pieceName.Pawn)
            {
                Board = ChessBoard.CreateButton(Board[xM, yM].PieceName, Board[xM, yM].Color, xM, yM, Board);
                Board = ChessBoard.CreateButton(pieceName.Empty, color.none, x, y, Board);
            }

            if (PieceName != pieceName.Pawn && isRealMove)
            {
                ChessBoard.grid.Children.Add(Board[xM, yM].Button);
                ChessBoard.grid.Children.Add(Board[x, y].Button);
            }
            return Board;
        }

        public static King SearchKing(color color, Piece[,] Board)
        {
            King king = new King();
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int l = 0; l < Board.GetLength(1); l++)
                {
                    if (Board[i, l].PieceName == pieceName.King && Board[i, l].Color == color)
                    {
                        king = Board[i, l].Clone() as King;
                        break;
                    }
                }
            }
            return king;
        }

        public static bool IsInCheck(color color, Piece[,] Board, int thrx = -1, int thry = -1)
        {
            if (thrx == -1 && thry == -1)
            {
                King king = SearchKing(color, Board);
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int l = 0; l < Board.GetLength(1); l++)
                    {
                        if (Board[i, l].Color != color)
                            if (Board[i, l].CanMove(king.x, king.y, Board, true))
                                return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int l = 0; l < Board.GetLength(1); l++)
                    {
                        if (Board[i, l].Color != color)
                            if (Board[i, l].CanMove(thrx, thry, Board, true))
                                return true;
                    }
                }
            }
            return false;
        }

        public static Piece[,] clonePieces()
        {
            Piece[,] pieces = new Piece[ChessBoard.Board.GetLength(0), ChessBoard.Board.GetLength(1)];
            for (int i = 0; i < ChessBoard.Board.GetLength(0); i++)
            {
                for (int l = 0; l < ChessBoard.Board.GetLength(1); l++)
                {
                    pieces[i, l] = (Piece)ChessBoard.Board[i, l].Clone();
                }
            }
            return pieces;
        }

        public static bool IsMate(color color, Piece[,] Board)
        {
            King king = SearchKing(color, Board);
            List<Piece> pieces = GetPieces(color);
            foreach (var item in pieces)
            {
                List<Moves> moves = GetMoves(item.x, item.y, Board);
                foreach (var move in moves)
                {
                    Piece[,] newBoard = item.Move(move.x, move.y, (Piece[,])Board.Clone(), false);
                    if (!IsInCheck(color, newBoard))
                        return false;
                }
            }

            return true;
        }

        private static List<Moves> GetMoves(int x, int y, Piece[,] Board)
        {
            List<Moves> list = new List<Moves>();
            Piece piece = Board[x, y];
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int l = 0; l < Board.GetLength(1); l++)
                {
                    if (piece.CanMove(i, l, Board, false))
                    {
                        list.Add(new Moves { x = i, y = l });
                    }
                }
            }
            return list;
        }

        private static List<Piece> GetPieces(color clr)
        {
            List<Piece> list = new List<Piece>();
            for (int i = 0; i < ChessBoard.Board.GetLength(0); i++)
            {
                for (int l = 0; l < ChessBoard.Board.GetLength(1); l++)
                {
                    if (ChessBoard.Board[i, l].Color == clr)
                        list.Add((Piece)ChessBoard.Board[i, l].Clone());
                }
            }
            return list;
        }

        public void showMoves(int x, int y)
        {
            List<Moves> list = GetMoves(x, y, ChessBoard.Board);
            foreach (var item in list)
            {
                ChessBoard.Board[item.x, item.y].Button.BorderBrush = Brushes.Green;
            }
        }

    }
}
