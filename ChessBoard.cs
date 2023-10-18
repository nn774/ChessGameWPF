using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using ChessGameWPF.piece;
using ChessGameWPF.Enum;
using ChessGameWPF.boardSctripts;
using System.Xml.Linq;
using System.Runtime.ConstrainedExecution;

namespace ChessGameWPF
{
    public class ChessBoard
    {
        public static color now = color.white;
        public static Piece[,] Board = new Piece[8, 8];

        public static bool isHaveEnPassanto = false;
        static bool isSelected = false;
        private static Button[,] btns;

        static int x = 0;
        static int y = 0;
        public static Grid grid;

        public static Piece[,] CreatePieces(pieceName Name, color clr, int x, int y, Piece[,] boar,
            bool isFirst = false, bool IsEnpasanto = false, bool ISbtn = false)
        {
            switch (Name)
            {
                case pieceName.Empty:
                    boar[x, y] = new Empty { Color = clr, x = x, y = y};
                    break;
                case pieceName.Pawn:
                    boar[x, y] = new Pawn { Color = clr, x = x, y = y, CanEnPassant = IsEnpasanto, HasMoved = isFirst };
                    break;
                case pieceName.King:
                    boar[x, y] = new King { Color = clr, x = x, y = y, HasMoved = isFirst };
                    break;
                case pieceName.Knight:
                    boar[x, y] = new Knight { Color = clr, x = x, y = y };
                    break;
                case pieceName.Bishop:
                    boar[x, y] = new Bishop { Color = clr, x = x, y = y };
                    break;
                case pieceName.Rook:
                    boar[x, y] = new Rook { Color = clr, x = x, y = y, HasMoved = isFirst };
                    break;
                case pieceName.Queen:
                    boar[x, y] = new Queen { Color = clr, x = x, y = y };
                    break;
            }
            if (ISbtn)
                CreateButton_(Name, clr, x, y);
            return boar;
        }

        public static string getName(pieceName Name, color clr)
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
            return name;
        }

        public static void CreateButton_(pieceName Name, color clr, int x, int y)
        {
            Button btn = new Button
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(5),
                Content = new Image
                {
                    Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{getName(Name,clr)}.bmp")),
                    Stretch = Stretch.Fill
                }
            };
            btn.Click += Button_Click;
            btn.Name = $"_{x}_{y}";
            Grid.SetRow(btn, x);
            Grid.SetColumn(btn, y);
            btns[x,y] = btn;
        }

        public static void EditButton(Button btn, pieceName Name, color clr)
        {
            btn.Content = new Image
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{getName(Name, clr)}.bmp")),
                Stretch = Stretch.Fill
            };
        }

        public static void CreateBoard(Style style)
        {
            btns = new Button[8,8];
            CreatePieces(pieceName.Rook, color.black, 0, 0, Board, ISbtn: true);
            CreatePieces(pieceName.Rook, color.black, 0, 7, Board, ISbtn: true);

            CreatePieces(pieceName.Rook, color.white, 7, 0, Board, ISbtn: true);
            CreatePieces(pieceName.Rook, color.white, 7, 7, Board, ISbtn: true);

            CreatePieces(pieceName.Knight, color.black, 0, 1, Board, ISbtn: true);
            CreatePieces(pieceName.Knight, color.black, 0, 6, Board, ISbtn: true);

            CreatePieces(pieceName.Knight, color.white, 7, 1, Board, ISbtn: true);
            CreatePieces(pieceName.Knight, color.white, 7, 6, Board, ISbtn: true);

            CreatePieces(pieceName.Bishop, color.black, 0, 2, Board, ISbtn: true);
            CreatePieces(pieceName.Bishop, color.black, 0, 5, Board, ISbtn: true);

            CreatePieces(pieceName.Bishop, color.white, 7, 2, Board, ISbtn: true);
            CreatePieces(pieceName.Bishop, color.white, 7, 5, Board, ISbtn: true);

            CreatePieces(pieceName.Queen, color.black, 0, 3, Board, ISbtn: true);
            CreatePieces(pieceName.King, color.black, 0, 4, Board, ISbtn: true);

            CreatePieces(pieceName.Queen, color.white, 7, 3, Board, ISbtn: true);
            CreatePieces(pieceName.King, color.white, 7, 4, Board, ISbtn: true);

            for (int i = 0; i < Board.GetLength(1); i++)
            {
                CreatePieces(pieceName.Pawn, color.black, 1, i, Board, ISbtn: true);

                CreatePieces(pieceName.Pawn, color.white, 6, i, Board, ISbtn: true);
            }
            for (int i = 2; i < Board.GetLength(0) - 2; i++)
            {
                for (int l = 0; l < Board.GetLength(1); l++)
                {
                    CreatePieces(pieceName.Empty, color.none, i, l, Board,ISbtn: true);
                }
            }
            foreach (var btn in btns)
                grid.Children.Add(btn);

        }

        private static void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            int xM = int.Parse(btn.Name.Split('_')[1]);
            int yM = int.Parse(btn.Name.Split('_')[2]);
            if (isSelected)
            {
                Piece[,] boar = (Piece[,])Board.Clone();
                if (Board[x, y].CanMove(xM, yM, boar))
                {
                    Board = boar[x, y].Move(xM, yM, boar);
                }
                isSelected = false;
                Clear_Moves();
                if (!CheckWin())
                    CheckPat();
            }
            else if (Board[xM, yM].PieceName != pieceName.Empty)
            {
                if (now != Board[xM, yM].Color)
                    return;
                if (Board[xM, yM].Color != color.none)
                    btn.BorderBrush = Brushes.DarkBlue;
                x = xM;
                y = yM;
                isSelected = true;
                Board[x, y].showMoves(x, y);
            }
            GC.Collect();
        }

        private static bool CheckWin()
        {
            if (Checkings.IsInCheck(color.black, Board))
                if (Checkings.IsMate(color.black, (Piece[,])Board.Clone()))
                {
                    MessageBox.Show($"Победа {color.white}");
                    return true;
                }
            if (Checkings.IsInCheck(color.white, Board))
                if (Checkings.IsMate(color.white, (Piece[,])Board.Clone()))
                {
                    MessageBox.Show($"Победа {color.black}");
                    return true;
                }
            return false;
        }

        private static void CheckPat()
        {
            if (Checkings.IsPat(Board))
                MessageBox.Show($"Пат!!!");
        }

        private static void Clear_Moves()
        {
            foreach (Button item in grid.Children)
                item.BorderBrush = Brushes.Transparent;
        }
    }
}
