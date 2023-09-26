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
        static Piece[,] Board = new Piece[8,8];

        private static Button CreateButton(pieceName Name,color clr,  int x, int y, Style style = null)
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
                Content = new Image
                {
                    Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{name}.png")),
                    Stretch = Stretch.Fill
                }
            };
            btn.Name = $"_{x}_{y}";
            if (Name == pieceName.Empty)
                btn.Style = style;
            Grid.SetRow(btn, x);
            Grid.SetColumn(btn, y);
            return btn;
        }

        public static void CreateBoard(Style style, Grid grid)
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
            for (int i = 2; i < Board.GetLength(0)-2; i++)
            {
                for (int l = 0; l < Board.GetLength(1)-2; l++)
                {
                    grid.Children.Add(CreateButton(pieceName.Empty, color.none, i, l, style));
                }
            }
        }

    }
}
