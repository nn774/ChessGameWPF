using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessGameWPF
{
    /// <summary>W
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style style = this.FindResource("Empty") as Style;
            ChessBoard.grid = Grid_Chess_Board;
            ChessBoard.CreateBoard(style);
        }
    }
}
