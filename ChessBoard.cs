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
        static bool isSelected = false;
        static int x = 0;
        static int y = 0;
        public static Grid grid;
        private static void CreatePieces(pieceName Name, color clr, int x, int y, Button btn)
        {
            switch (Name)
            {
                case pieceName.Empty:
                    Board[x, y] = new Empty { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Pawn:
                    Board[x, y] = new Pawn { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.King:
                    Board[x, y] = new King { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Knight:
                    Board[x, y] = new Knight { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Bishop:
                    Board[x, y] = new Bishop { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Rook:
                    Board[x, y] = new Rook { Color = clr, x = x, y = y, Button = btn };
                    break;
                case pieceName.Queen:
                    Board[x, y] = new Queen { Color = clr, x = x, y = y, Button = btn };
                    break;
            }
        }

        public static Button CreateButton(pieceName Name, color clr, int x, int y)
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
            CreatePieces(Name, clr, x, y, btn);
            return btn;
        }

        public static void CreateBoard(Style style)
        {
            grid.Children.Add(CreateButton(pieceName.Rook, color.black, 0, 0));
            grid.Children.Add(CreateButton(pieceName.Rook, color.black, 0, 7));

            grid.Children.Add(CreateButton(pieceName.Rook, color.white, 7, 0));
            grid.Children.Add(CreateButton(pieceName.Rook, color.white, 7, 7));

            grid.Children.Add(CreateButton(pieceName.Knight, color.black, 0, 1));
            grid.Children.Add(CreateButton(pieceName.Knight, color.black, 0, 6));

            grid.Children.Add(CreateButton(pieceName.Knight, color.white, 7, 1));
            grid.Children.Add(CreateButton(pieceName.Knight, color.white, 7, 6));

            grid.Children.Add(CreateButton(pieceName.Bishop, color.black, 0, 2));
            grid.Children.Add(CreateButton(pieceName.Bishop, color.black, 0, 5));

            grid.Children.Add(CreateButton(pieceName.Bishop, color.white, 7, 2));
            grid.Children.Add(CreateButton(pieceName.Bishop, color.white, 7, 5));

            grid.Children.Add(CreateButton(pieceName.Queen, color.black, 0, 3));
            grid.Children.Add(CreateButton(pieceName.King, color.black, 0, 4));

            grid.Children.Add(CreateButton(pieceName.Queen, color.white, 7, 3));
            grid.Children.Add(CreateButton(pieceName.King, color.white, 7, 4));

            for (int i = 0; i < Board.GetLength(1); i++)
            {
                grid.Children.Add(CreateButton(pieceName.Pawn, color.black, 1, i));

                grid.Children.Add(CreateButton(pieceName.Pawn, color.white, 6, i));
            }
            for (int i = 2; i < Board.GetLength(0) - 2; i++)
            {
                for (int l = 0; l < Board.GetLength(1); l++)
                {
                    grid.Children.Add(CreateButton(pieceName.Empty, color.none, i, l));
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
                Piece[,] board = (Piece[,])Board.Clone();
                Board[x, y].Button.BorderBrush = Brushes.Transparent;
                Board[xM, yM].Button.BorderBrush = Brushes.Transparent;
                if (Board[x, y].tryMove(xM, yM, board))
                    Board[x, y].Move(xM, yM);
                isSelected = false;
                Clear_Moves();
            }
            else
            {
                x = xM;
                y = yM;
                isSelected = true;
                Board[x, y].showMoves(x, y);
            }
        }

        private static void Clear_Moves()
        {
            foreach (var item in Board)
                item.Button.BorderBrush = Brushes.Transparent;
        }
    }
}
