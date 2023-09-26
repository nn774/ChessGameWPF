using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using ChessGameWPF.piece;
using System.Reflection.Metadata;
using ChessGameWPF.Enum;

namespace ChessGameWPF
{
    internal class ChessBoard
    {
        public static Piece[,] Board = new Piece[8, 8];

        public static bool isHaveEnPassanto = false;
        static bool isSelected = false;

        static int x = 0;
        static int y = 0;
        public static Grid grid;

        private static Piece[,] CreatePieces(pieceName Name, color clr, int x, int y, Button btn, Piece[,] boar,
            bool isFirst = false, bool IsEnpasanto = false)
        {
            switch (Name)
            {
                case pieceName.Empty:
                    boar[x, y] = new Empty { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Pawn:
                    boar[x, y] = new Pawn { Color = clr, x = x, y = y, Button = btn, CanEnPassant = IsEnpasanto, HasMoved = isFirst };
                    break;
                case pieceName.King:
                    boar[x, y] = new King { Color = clr, x = x, y = y, Button = btn, HasMoved = isFirst };
                    break;
                case pieceName.Knight:
                    boar[x, y] = new Knight { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Bishop:
                    boar[x, y] = new Bishop { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Rook:
                    boar[x, y] = new Rook { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Queen:
                    boar[x, y] = new Queen { Color = clr, x = x, y = y, Button = btn };
                    break;
            }
            return boar;
        }

        public static Piece[,] CreateButton(pieceName Name, color clr, int x, int y, Piece[,] boar,
            bool HasMoved = false, bool IsEnpasanto = false)
        {
            string name = "";
            switch (Name)
            {
                case pieceName.Empty:
                    name += "Empty";
                    break;
                case pieceName.Pawn:
                    name += "Pawn_";
                    break;
                case pieceName.King:
                    name += "King_";
                    break;
                case pieceName.Knight:
                    name += "Knight_";
                    break;
                case pieceName.Bishop:
                    name += "Bishop_";
                    break;
                case pieceName.Rook:
                    name += "Rook_";
                    break;
                case pieceName.Queen:
                    name += "Queen_";
                    break;
            }
            switch (clr)
            {
                case color.none:
                    break;
                case color.black:
                    name += "B";
                    break;
                case color.white:
                    name += "W";
                    break;
            }
            Button btn = new Button
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(5),
                Content = new Image
                {
                    Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{name}.png")),
                    Stretch = Stretch.Fill
                }
            };
            btn.Click += Button_Click;
            btn.Name = $"_{x}_{y}";
            Grid.SetRow(btn, x);
            Grid.SetColumn(btn, y);
            boar = CreatePieces(Name, clr, x, y, btn, boar, HasMoved, IsEnpasanto);
            return boar;
        }

        public static void CreateBoard(Style style)
        {
            Board = CreateButton(pieceName.Rook, color.black, 0, 0, Board);
            Board = CreateButton(pieceName.Rook, color.black, 0, 7, Board);

            Board = CreateButton(pieceName.Rook, color.white, 7, 0, Board);
            Board = CreateButton(pieceName.Rook, color.white, 7, 7, Board);

            Board = CreateButton(pieceName.Knight, color.black, 0, 1, Board);
            Board = CreateButton(pieceName.Knight, color.black, 0, 6, Board);

            Board = CreateButton(pieceName.Knight, color.white, 7, 1, Board);
            Board = CreateButton(pieceName.Knight, color.white, 7, 6, Board);

            Board = CreateButton(pieceName.Bishop, color.black, 0, 2, Board);
            Board = CreateButton(pieceName.Bishop, color.black, 0, 5, Board);

            Board = CreateButton(pieceName.Bishop, color.white, 7, 2, Board);
            Board = CreateButton(pieceName.Bishop, color.white, 7, 5, Board);

            Board = CreateButton(pieceName.Queen, color.black, 0, 3, Board);
            Board = CreateButton(pieceName.King, color.black, 0, 4, Board);

            Board = CreateButton(pieceName.Queen, color.white, 7, 3, Board);
            Board = CreateButton(pieceName.King, color.white, 7, 4, Board);

            for (int i = 0; i < Board.GetLength(1); i++)
            {
                Board = CreateButton(pieceName.Pawn, color.black, 1, i, Board);

                Board = CreateButton(pieceName.Pawn, color.white, 6, i, Board);
            }
            for (int i = 2; i < Board.GetLength(0) - 2; i++)
            {
                for (int l = 0; l < Board.GetLength(1); l++)
                {
                    Board = CreateButton(pieceName.Empty, color.none, i, l, Board);
                }
            }
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int l = 0; l < Board.GetLength(1); l++)
                {
                    grid.Children.Add(Board[i,l].Button);
                }
            }
        }

        private static void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            int xM = int.Parse(btn.Name.Split('_')[1]);
            int yM = int.Parse(btn.Name.Split('_')[2]);
            if (ChessBoard.Board[xM, yM].Color != color.none)
            {
                btn.BorderBrush = Brushes.Red;
            }
            if (isSelected)
            {
                Board[x, y].Button.BorderBrush = Brushes.Transparent;
                Board[xM, yM].Button.BorderBrush = Brushes.Transparent;
                Piece[,] boar = (Piece[,])Board.Clone();
                if (Board[x, y].CanMove(xM, yM, boar))
                {
                    Board = null;
                    Board = boar[x, y].Move(xM, yM, boar);
                }
                isSelected = false;
                Clear_Moves();
                CheckWin();
            }
            else if (Board[xM, yM].PieceName != pieceName.Empty)
            {
                x = xM;
                y = yM;
                isSelected = true;
                Board[x, y].showMoves(x, y);
            }
        }

        private static void CheckWin()
        {
            if (Piece.IsInCheck(color.black, Board))
                if (Piece.IsMate(color.black, (Piece[,])Board.Clone()))
                    MessageBox.Show($"Победа {color.white}");
            if (Piece.IsInCheck(color.white, Board))
                if (Piece.IsMate(color.white, (Piece[,])Board.Clone()))
                    MessageBox.Show($"Победа {color.black}");
        }

        private static void Clear_Moves()
        {
            foreach (var item in Board)
                item.Button.BorderBrush = Brushes.Transparent;
        }
    }
}
