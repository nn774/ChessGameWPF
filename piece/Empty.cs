using ChessGameWPF.Enum;
using System.Diagnostics.CodeAnalysis;

namespace ChessGameWPF.piece
{
    [ExcludeFromCodeCoverage]
    public class Empty : Piece
    {
        public override pieceName PieceName => pieceName.Empty;

        public override bool CanMove(int xMove, int yMove, Piece[,] Board, bool isChecking = false)
        {
            return false;
        }

        public override Piece[,] Move(int xM, int yM, Piece[,] Board, bool isRealMove = true)
        {
            return Board;
        }
    }
}
