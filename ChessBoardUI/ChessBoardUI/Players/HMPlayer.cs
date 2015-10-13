using ChessBoardUI.ViewModel;
using GalaSoft.MvvmLight.Messaging;
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
        private Dictionary<int, ChessPiece> pieces_dict;
        private ObservableCollection<ChessPiece> pieces_collection;


        //servableCollection<ChessPiece> all_pieces;
        //ctionary<int, ChessPiece> board_layout;

        public HMPlayer(ObservableCollection<ChessPiece> pieces_collection, Dictionary<int, ChessPiece> pieces_dict)
        {
            human_timer = new TimerViewModel
            {
                Participant = Participant.Player,
                TimeSpan = TimeSpan.FromMinutes(30),
                TimerDispatcher = new DispatcherTimer(),
                Display = "00:30:00"
            };

            Messenger.Default.Register<HumanMoveMessage>(this, (action) => HumanPiecePositionChangeHandler(action));
            Messenger.Default.Register<MachineMoveMessage>(this, (action) => MachinePiecePositionChangeHandler(action));
            human_capture = new SPCapturedViewModel { CapturedPiecesCollection = new ObservableCollection<Image>() };
            this.pieces_collection = pieces_collection;
            this.pieces_dict = pieces_dict;

        }

        public Dictionary<int, ChessPiece> PieceDictionary
        {
            get { return pieces_dict; }
            set { pieces_dict = value; }
        }

        public ObservableCollection<ChessPiece> PieceCollection
        {
            get { return pieces_collection; }
            set { pieces_collection = value; }
        }

        public SPCapturedViewModel HumanCaptureStack  //stack of pieces captured by human player(collection of images)
        {
            get { return human_capture; }
            set { human_capture = value; }
        }

        public TimerViewModel HumanTimer
        {
            get { return human_timer; }
            set { human_timer = value; }
        }


        public void HumanPiecePositionChangeHandler(HumanMoveMessage action)
        {
            this.HumanTimer.stopClock();

            //lock the entire board so that use cannot click on piece when it is machine's turn.
            foreach (KeyValuePair<int, ChessPiece> item in this.pieces_dict)
            {
                if (item.Value.Player==Player.White)
                {
                    item.Value.Ownership = false;
                }
            }
        }

        public void MachinePiecePositionChangeHandler(MachineMoveMessage action)
        {
            //unlock human player pieces so he can go on
            foreach (KeyValuePair<int, ChessPiece> item in this.pieces_dict)
            {
                if (item.Value.Player == Player.White)
                {
                    item.Value.Ownership = action.Turn;
                }
            }
            this.HumanTimer.startClock();

        }

    }
}
