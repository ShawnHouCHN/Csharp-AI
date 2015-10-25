
using ChessBoardUI.AIAlgorithm;
using ChessBoardUI.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                if (item.Value.Player == Player.White && MoveGenerator.player_color)
                {
                    item.Value.Ownership = false;
                }
                else if (item.Value.Player == Player.Black && !MoveGenerator.player_color)
                {
                    item.Value.Ownership = false;
                }
            }


            // some bit operations to get the bit
            int from_index = (7 - (int)action.FromPoint.Y) * 8 + (int)action.FromPoint.X;
            int to_index = (7 - (int)action.ToPoint.Y) * 8 + (int)action.ToPoint.X;
            ulong moved_place = 0x0000000000000001;
            ulong new_place =   0x0000000000000001;
            moved_place = MoveGenerator.full_occupied & ~(moved_place << (from_index));
            new_place = (new_place << (to_index));
            MoveGenerator.UpdateAnyMovedBitboard(action.Type, moved_place, new_place);


        }

        public void MachinePiecePositionChangeHandler(MachineMoveMessage action)
        {
            int from_loca_index = action.From_File * 10 + (7 - action.From_Rank);
            int to_loca_index = action.To_File * 10 + (7 - action.To_Rank);

            ChessPiece moved = this.pieces_dict[from_loca_index];

            if (this.pieces_dict.ContainsKey(to_loca_index))
            {
                ChessPiece to_piece_location = this.pieces_dict[to_loca_index];
                Application.Current.Dispatcher.Invoke((Action)(() => this.pieces_collection.Remove(to_piece_location)));
                this.pieces_dict.Remove(to_loca_index);
            }
            moved.Pos_X = action.To_File;
            moved.Pos_Y = 7 - action.To_Rank;

            this.pieces_dict.Remove(from_loca_index);
            this.pieces_dict.Add(to_loca_index, moved);

            //unlock human player pieces so he can go on
            foreach (KeyValuePair<int, ChessPiece> item in this.pieces_dict)
            {
                if (item.Value.Player == Player.White && MoveGenerator.player_color)
                {
                    item.Value.Ownership = action.Turn;
                }
                else if (item.Value.Player == Player.Black && !MoveGenerator.player_color)
                {
                    item.Value.Ownership = action.Turn;
                }
            }


            this.HumanTimer.startClock();

        }

    }
}
