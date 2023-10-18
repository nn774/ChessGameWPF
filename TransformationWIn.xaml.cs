using ChessGameWPF.Enum;
using ChessGameWPF.piece;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace ChessGameWPF
{
    /// <summary>
    /// Логика взаимодействия для TransformationWIn.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
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
