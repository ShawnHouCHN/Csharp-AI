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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessBoardUI.ViewModel;
using System.Collections.ObjectModel;

namespace ChessBoardUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public static class Constants
    {
        internal const int CELL_EDGE_LENGTH = 70;
    }



    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.ChessBoard.ItemsSource = new ObservableCollection<ChessPiece>
            {
            new ChessPiece{Pos=new Point(0, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(1, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(2, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(3, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(4, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(5, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(6, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(7, 6), Type=PieceType.Pawn, Player=Player.White},
            new ChessPiece{Pos=new Point(0, 7), Type=PieceType.Rook, Player=Player.White},
            new ChessPiece{Pos=new Point(1, 7), Type=PieceType.Knight, Player=Player.White},
            new ChessPiece{Pos=new Point(2, 7), Type=PieceType.Bishop, Player=Player.White},
            new ChessPiece{Pos=new Point(3, 7), Type=PieceType.King, Player=Player.White},
            new ChessPiece{Pos=new Point(4, 7), Type=PieceType.Queen, Player=Player.White},
            new ChessPiece{Pos=new Point(5, 7), Type=PieceType.Bishop, Player=Player.White},
            new ChessPiece{Pos=new Point(6, 7), Type=PieceType.Knight, Player=Player.White},
            new ChessPiece{Pos=new Point(7, 7), Type=PieceType.Rook, Player=Player.White},
            new ChessPiece{Pos=new Point(0, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(1, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(2, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(3, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(4, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(5, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(6, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(7, 1), Type=PieceType.Pawn, Player=Player.Black},
            new ChessPiece{Pos=new Point(0, 0), Type=PieceType.Rook, Player=Player.Black},
            new ChessPiece{Pos=new Point(1, 0), Type=PieceType.Knight, Player=Player.Black},
            new ChessPiece{Pos=new Point(2, 0), Type=PieceType.Bishop, Player=Player.Black},
            new ChessPiece{Pos=new Point(3, 0), Type=PieceType.King, Player=Player.Black},
            new ChessPiece{Pos=new Point(4, 0), Type=PieceType.Queen, Player=Player.Black},
            new ChessPiece{Pos=new Point(5, 0), Type=PieceType.Bishop, Player=Player.Black},
            new ChessPiece{Pos=new Point(6, 0), Type=PieceType.Knight, Player=Player.Black},
            new ChessPiece{Pos=new Point(7, 0), Type=PieceType.Rook, Player=Player.Black}
        };

        }
    }
}
