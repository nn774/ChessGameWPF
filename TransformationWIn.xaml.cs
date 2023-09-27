using ChessGameWPF.Enum;
using ChessGameWPF.piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChessGameWPF
{
    /// <summary>
    /// Логика взаимодействия для TransformationWIn.xaml
    /// </summary>

    public partial class TransformationWIn : Window
    {
        public static int xM { get; set; } = 0;
        public static int yM { get; set; } = 0;
        public static color Color { get; set; }

        public TransformationWIn()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pawn.newName = pieceName.Queen;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pawn.newName = pieceName.Rook;
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Pawn.newName = pieceName.Bishop;
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Pawn.newName = pieceName.Knight;
            this.Close();
        }
    }
}
