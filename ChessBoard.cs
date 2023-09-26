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

namespace ChessGameWPF
{
    internal class ChessBoard
    {
        static Piece[,] Board = new Piece[8,8];

        private static Button CreateButton(string Name, int x, int y, Style style = null)
        {
            Button btn = new Button
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Content = new Image
                {
                    Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{Name}.png")),
                    Stretch = Stretch.Fill
                }
            };
            btn.Name = $"_{x}_{y}";
            if (Name == "Empty")
                btn.Style = style;
            Grid.SetRow(btn, x);
            Grid.SetColumn(btn, y);
            return btn;
        }

        public static void CreateBoard(Style style, Grid grid)
        {
            grid.Children.Add(CreateButton("Rook_B", 0, 0));
            grid.Children.Add(CreateButton("Rook_B", 0, 7));

            grid.Children.Add(CreateButton("Rook_W", 7, 0));
            grid.Children.Add(CreateButton("Rook_W", 7, 7));

            grid.Children.Add(CreateButton("Knight_B", 0, 1));
            grid.Children.Add(CreateButton("Knight_B", 0, 6));

            grid.Children.Add(CreateButton("Knight_W", 7, 1));
            grid.Children.Add(CreateButton("Knight_W", 7, 6));

            grid.Children.Add(CreateButton("Bishop_B", 0, 2));
            grid.Children.Add(CreateButton("Bishop_B", 0, 5));

            grid.Children.Add(CreateButton("Bishop_W", 7, 2));
            grid.Children.Add(CreateButton("Bishop_W", 7, 5));

            grid.Children.Add(CreateButton("Queen_B", 0, 3));
            grid.Children.Add(CreateButton("King_B", 0, 4));

            grid.Children.Add(CreateButton("Queen_W", 7, 3));
            grid.Children.Add(CreateButton("King_W", 7, 4));

            for (int i = 0; i < Board.GetLength(1); i++)
            {
                grid.Children.Add(CreateButton("Pawn_B", 1, i));
                grid.Children.Add(CreateButton("Pawn_W", 6, i));
            }
            for (int i = 2; i < Board.GetLength(0)-2; i++)
            {
                for (int l = 0; l < Board.GetLength(1)-2; l++)
                {
                    grid.Children.Add(CreateButton("Empty", i, l, style));
                }
            }
        }
    }
}
