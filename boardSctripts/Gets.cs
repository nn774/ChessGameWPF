using ChessGameWPF.Enum;
using ChessGameWPF.piece;
using System.Collections.Generic;

namespace ChessGameWPF.boardSctripts
{
    internal class Gets
    {
        public static List<Moves> GetMoves(int x, int y, Piece[,] Board)
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

        public static List<Piece> GetPieces(color clr)
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
    }
}
