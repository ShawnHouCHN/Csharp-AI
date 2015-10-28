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
using System.Windows.Threading;
using ChessBoardUI.Players;

namespace ChessBoardUI
{
    public partial class MainWindow : Window
    {

        
        //HMPlayer human_player;
        //MCPlayer machine_player;
        Dictionary<int, ChessPiece> board_layout; //generic hashtable
        MainControl board;



        public MainWindow()
        {
            InitializeComponent();

            //test
            //SPCapturedViewModel test = new SPCapturedViewModel { CapturedPiecesCollection = new ObservableCollection<Image>() };
            //PlayerCapStack.ItemsSource = test.CapturedPiecesCollection;
            //Uri uri = new Uri("/PieceImg/chess_piece_white_bishop.png", UriKind.Relative);
            //BitmapImage source = new BitmapImage();
            //source.BeginInit();
            //source.UriSource = uri;
            //source.DecodePixelHeight = 70;
            //source.DecodePixelWidth = 70;
            //source.EndInit();

            //Uri uri2 = new Uri("/PieceImg/chess_piece_white_queen.png", UriKind.Relative);
            //BitmapImage source2 = new BitmapImage();
            //source2.BeginInit();
            //source2.UriSource = uri2;
            //source2.DecodePixelHeight = 70;
            //source2.DecodePixelWidth = 70;
            //source2.EndInit();

            //Image a = new Image();
            //a.Source = source;
            //a.Width = 40;
            //a.Height = 40;

            //Image b = new Image();
            //b.Source = source2;
            //b.Width = 40;
            //b.Height = 40;
            
            //test.CapturedPiecesCollection.Add(a);
            //test.CapturedPiecesCollection.Add(b);

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {

            board_layout = new Dictionary<int, ChessPiece>();

            if ((String)((ComboBoxItem)ChooseColor.SelectedItem).Content == "Black")
                board = new MainControl(false);
            else
                board = new MainControl(true);


            //Console.WriteLine("{0},{1}", ((ComboBoxItem)ChooseColor.SelectedItem).Content, ((ComboBoxItem)ChooseLevel.SelectedItem).Content);
            StartButton.IsEnabled = false;
            ChooseLevel.IsEnabled = false;
            ChooseColor.IsEnabled = false;


            player_timer.DataContext = board.HumanPlayer.HumanTimer;
            pc_timer.DataContext = board.MachinePlayer.MachineTimer;


            board.HumanPlayer.HumanTimer.startClock();
            //chess_canvas.SetValue = (Brush)Resources["Checkerboard2"];
            //TemplateContent a = ChessBoard.ItemsPanel.Template;
            // = (Brush)Resources["Checkerboard2"];
            //PlayerCapStack.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = board.HumanPlayer.HumanCaptureStack.CapturedPiecesCollection });
            //MachineCapStack.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = board.MachinePlayer.MachineCaptureStack.CapturedPiecesCollection });
            PlayerCapStack.ItemsSource = board.HumanPlayer.HumanCaptureStack.CapturedPiecesCollection;
            MachineCapStack.ItemsSource = board.MachinePlayer.MachineCaptureStack.CapturedPiecesCollection;


            this.ChessBoard.ItemsSource = board.BoardCollection;


        }
    }
}
