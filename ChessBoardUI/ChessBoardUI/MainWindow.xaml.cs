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
        internal const int CANVAS_MARGIN_LEFT = 35;  //to pinpoint the cursor position in the canvas 
        internal const int CANVAS_MARGIN_TOP = 45;   //to pinpoint the cursor position in the canvas 

    }



    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0},{1}", ((ComboBoxItem)ChooseColor.SelectedItem).Content, ((ComboBoxItem)ChooseLevel.SelectedItem).Content);
            StartButton.IsEnabled = false;
            ChooseColor.IsEnabled = false;
            ChooseLevel.IsEnabled = false;

            Player selected_color = (Player)Enum.Parse(typeof(Player), ((ComboBoxItem)ChooseColor.SelectedItem).Content.ToString());
            Player unselected_color;

            if (selected_color.Equals(Player.White))
            {
                unselected_color = Player.Black;
            }
            else
            {
                unselected_color = Player.White;
            }
            

            this.ChessBoard.ItemsSource = new ObservableCollection<ChessPiece>
            {
            new ChessPiece{Pos=new Point(0, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(1, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(2, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(3, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(4, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(5, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(6, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(7, 6), Type=PieceType.Pawn, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(0, 7), Type=PieceType.Rook, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(1, 7), Type=PieceType.Knight, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(2, 7), Type=PieceType.Bishop, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(3, 7), Type=PieceType.King, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(4, 7), Type=PieceType.Queen, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(5, 7), Type=PieceType.Bishop, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(6, 7), Type=PieceType.Knight, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(7, 7), Type=PieceType.Rook, Player=selected_color, Ownership=true, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(0, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(1, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(2, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(3, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(4, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(5, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(6, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(7, 1), Type=PieceType.Pawn, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(0, 0), Type=PieceType.Rook, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(1, 0), Type=PieceType.Knight, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(2, 0), Type=PieceType.Bishop, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(3, 0), Type=PieceType.King, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(4, 0), Type=PieceType.Queen, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(5, 0), Type=PieceType.Bishop, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(6, 0), Type=PieceType.Knight, Player=unselected_color, Ownership=false, PieceMoveCommand=null},
            new ChessPiece{Pos=new Point(7, 0), Type=PieceType.Rook, Player=unselected_color, Ownership=false, PieceMoveCommand=null}
        };
        }

        private void TestMouseHandler(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Mouse position is ( {0},{1} )", ((int)Mouse.GetPosition(this).X-Constants.CANVAS_MARGIN_LEFT)/ Constants.CELL_EDGE_LENGTH, ((int)Mouse.GetPosition(this).Y- Constants.CANVAS_MARGIN_TOP) / Constants.CELL_EDGE_LENGTH);
        }
        
    }
}
