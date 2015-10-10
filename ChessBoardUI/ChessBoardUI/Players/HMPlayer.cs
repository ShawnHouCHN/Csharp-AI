using ChessBoardUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChessBoardUI.Players
{
    class HMPlayer
    {
        private TimerViewModel human_timer;
        private SPCapturedViewModel human_capture;
        //ivate GameViewModel board;


        //servableCollection<ChessPiece> all_pieces;
        //ctionary<int, ChessPiece> board_layout;

        public HMPlayer()
        {
            human_timer = new TimerViewModel
            {
                Participant = Participant.Player,
                TimeSpan = TimeSpan.FromMinutes(30),
                TimerDispatcher = new DispatcherTimer(),
                Display = "00:30:00"
            };

            human_capture = new SPCapturedViewModel { CapturedPiecesCollection = new ObservableCollection<Image>() };
            //is.ChessBoard = board;
            // this.board_layout = board_layout;

        }



        public SPCapturedViewModel HumanCaptureStack  //stack of pieces captured by human player(collection of images)
        {
            get { return human_capture; }
            set { human_capture = value; }
        }

        //public GameViewModel ChessBoard  //stack of pieces captured by human player(collection of images)
        //{
        //    get { return board; }
        //    set { board = value; }
        //}

        public TimerViewModel HumanTimer
        {
            get { return human_timer; }
            set { human_timer = value; }
        }
        

        //test method
        //public void remove()
        //{
        //    all_pieces.Remove(board_layout[7]);
        //    Console.WriteLine("Content in the hashtable {0}", board_layout[7].Type);
        //}


    }
}
