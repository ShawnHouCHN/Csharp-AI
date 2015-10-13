using ChessBoardUI.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChessBoardUI.Players
{
   
    class AIPlayer
    {
        private TimerViewModel machine_timer;
        private SPCapturedViewModel machine_stack;
        private Dictionary<int, ChessPiece> pieces_dict;
        private ObservableCollection<ChessPiece> pieces_collection;
        private bool turn = false;
        Thread algo_thread;

        public AIPlayer(ObservableCollection<ChessPiece> pieces_collection, Dictionary<int, ChessPiece> pieces_dict)
        {

            //machine clock instantiation
            machine_timer = new TimerViewModel
            {
                Participant = Participant.PC,
                TimeSpan = TimeSpan.FromMinutes(30),
                TimerDispatcher = new DispatcherTimer(),
                Display = "00:30:00"
            };

            //human move messenger registration
            Messenger.Default.Register<HumanMoveMessage>(this, (action) => HumanPiecePositionChangeHandler(action));

            //captured stack instantiation 
            machine_stack = new SPCapturedViewModel { CapturedPiecesCollection = new ObservableCollection<Image>() };

            //collection and dictionary referencing
            this.pieces_collection = pieces_collection;
            this.pieces_dict = pieces_dict;


            //algorithm thread instantiation
            algo_thread = new Thread(new ThreadStart(AlgorithmThread));
            algo_thread.IsBackground = true; //terminate thread when window is closed 
            algo_thread.Start();
        }

        public SPCapturedViewModel MachineCaptureStack  //stack of pieces captured by human player(collection of images)
        {
            get { return machine_stack; }
            set { machine_stack = value; }
        }

        public TimerViewModel MachineTimer
        {
            get { return machine_timer; }
            set { machine_timer = value; }
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

        public void HumanPiecePositionChangeHandler(HumanMoveMessage action)
        {
            //Console.WriteLine("Meesage received From {0}  to {1}", action.FromPoint, action.ToPoint);
            this.turn = true;
           
        }


        public void AlgorithmThread()
        {
            try
            {
                while (true)
                {
                    if (turn)
                    {
                        this.MachineTimer.startClock();
                        System.Threading.Thread.Sleep(5000);
                        ChessPiece ai_move_piece = this.pieces_dict[1];
                        Console.WriteLine("AI moves {0} {1}", ai_move_piece.Coor_X, ai_move_piece.Coor_Y);
                        int priv_coor_x = ai_move_piece.Coor_X;
                        int priv_coor_y = ai_move_piece.Coor_Y;

                        ai_move_piece.Pos_X = 0;
                        ai_move_piece.Pos_Y = 2;
                        this.pieces_dict.Add(2, ai_move_piece);
                        this.pieces_dict.Remove(1);

                        //Console.WriteLine("AI moves {0}", this.pieces_dict[1]);
                        //AI algorithm goes here.

                        //this.pieces_collection.Remove(this.pieces_dict[6]);
                        Messenger.Default.Send(new MachineMoveMessage { Turn = this.turn });
                        this.MachineTimer.stopClock();
                        this.turn = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something wrong with the algorithm. check the code!!!");
            }
        }
    }




}
