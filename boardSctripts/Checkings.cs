using ChessGameWPF.Enum;
using ChessGameWPF.piece;
using System.Collections.Generic;

namespace ChessGameWPF.boardSctripts
{
    internal class Checkings
    {
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

        public static bool IsMate(color color, Piece[,] Board)
        {
            King king = SearchKing(color, Board);
            List<Piece> pieces = Gets.GetPieces(color);
            foreach (var item in pieces)
            {
                List<Moves> moves = Gets.GetMoves(item.x, item.y, Board);
                foreach (var move in moves)
                {
                    Piece[,] newBoard = item.Move(move.x, move.y, (Piece[,])Board.Clone(), false);
                    if (!IsInCheck(color, newBoard))
                        return false;
                }
            }

            return true;
        }

         public static bool IsPat(Piece[,] Board)
        {
            bool ispatWhite = true;
            bool ispatBlack = true;
            List<Piece> piecesWhite = Gets.GetPieces(color.white);
            List<Piece> piecesBlack = Gets.GetPieces(color.black);
            if (piecesWhite.Count == 1 && piecesBlack.Count == 1)
                return true;
            if (piecesWhite.Count == 1 && piecesBlack.Count == 2)
                if (piecesBlack[0].PieceName == pieceName.Bishop || piecesBlack[0].PieceName == pieceName.Knight ||
                piecesBlack[1].PieceName == pieceName.Bishop || piecesBlack[1].PieceName == pieceName.Knight)
                    return true;

            if (piecesWhite.Count == 2 && piecesBlack.Count == 1)
                if (piecesWhite[0].PieceName == pieceName.Bishop || piecesWhite[0].PieceName == pieceName.Knight ||
                    piecesWhite[1].PieceName == pieceName.Bishop || piecesWhite[1].PieceName == pieceName.Knight)
                    return true;

            if (piecesWhite.Count == 2 && piecesBlack.Count == 2)
                foreach (var wItem in piecesWhite)
                    foreach (var bItem in piecesBlack) 
                        if (wItem.PieceName == pieceName.Bishop && bItem.PieceName == pieceName.Bishop)
                            if ((wItem.x + wItem.y) % 2 == (bItem.x + wItem.y) % 2)
                                return true;

            if (piecesWhite.Count == 1 && piecesBlack.Count == 3)
            {
               List<int> ints = new List<int>();
                for (int i = 0; i < piecesBlack.Count; i++)
                {
                    if (piecesBlack[i].PieceName == pieceName.Bishop)
                        ints.Add(i);
                }
                if (ints.Count == 2)
                {
                    if ((piecesBlack[ints[0]].x + piecesBlack[ints[0]].y)%2== (piecesBlack[ints[1]].x + piecesBlack[ints[1]].y) % 2)
                        return true;
                }
            }

            if (piecesBlack.Count == 1 && piecesWhite.Count == 3)
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < piecesWhite.Count; i++)
                {
                    if (piecesWhite[i].PieceName == pieceName.Bishop)
                        ints.Add(i);
                }
                if (ints.Count == 2)
                {
                    if ((piecesWhite[ints[0]].x + piecesWhite[ints[0]].y) % 2 == (piecesWhite[ints[1]].x + piecesWhite[ints[1]].y))
                        return true;
                }
            }

            foreach (var item in piecesBlack)
            {
                List<Moves> moves = Gets.GetMoves(item.x, item.y, Board);
                if (moves.Count != 0)
                {
                    ispatBlack = false;
                    break;
                }
            }
            foreach (var item in piecesWhite)
            {
                List<Moves> moves = Gets.GetMoves(item.x, item.y, Board);
                if (moves.Count != 0)
                {
                    ispatWhite = false;
                    break;
                }
            }
            if (ispatBlack || ispatWhite)
                return true;
            return false;
        }
    }
}
