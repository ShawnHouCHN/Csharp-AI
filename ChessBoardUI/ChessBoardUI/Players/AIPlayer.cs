﻿using ChessBoardUI.AIAlgorithm;
using ChessBoardUI.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
//using ChessBoardUI.AIAlgorithm;

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
        MoveGenerator move_generator;
        

        //Player ai_color;

        public AIPlayer(ObservableCollection<ChessPiece> pieces_collection, Dictionary<int, ChessPiece> pieces_dict)
        {
            move_generator = new MoveGenerator();

            //ai color to use;
            //this.ai_color = ai_color;

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
            Console.WriteLine("Meesage received From {0}  to {1}", action.FromPoint, action.ToPoint);
            ChessPiece moved=this.pieces_dict[(int)action.FromPoint.X * 10 + (int)action.FromPoint.Y];
            
            int to_location = (int)action.ToPoint.X * 10 + (int)action.ToPoint.Y;
            // capture move
            if (this.pieces_dict.ContainsKey(to_location))
            {
                // if two piece is same color,then it is a castling. 

                ChessPiece to_piece_location = this.pieces_dict[to_location];             
                int to_index = (7 - (int)action.ToPoint.Y) * 8 + (int)action.ToPoint.X;
                ulong moved_place = 0x0000000000000001;
                moved_place = (moved_place << (to_index));
                this.pieces_collection.Remove(to_piece_location);
                this.pieces_dict.Remove(to_location);
                MoveGenerator.UpdateAnyCapturedBitboard(to_piece_location.Type, moved_place);
            }

            //also need an passent move maybe?


            this.pieces_dict.Add((moved.Coor_X * 10 + moved.Coor_Y), moved);
            this.pieces_dict.Remove((int)action.FromPoint.X * 10 + (int)action.FromPoint.Y);
            

            Console.WriteLine("Moved piece is a {0} {1} placed at {2}", this.pieces_dict[(moved.Coor_X * 10 + moved.Coor_Y)].Player, this.pieces_dict[(moved.Coor_X * 10 + moved.Coor_Y)].Type, this.pieces_dict[(moved.Coor_X * 10 + moved.Coor_Y)].Pos);

            this.turn = action.Turn;
           
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

                        // this is the current chess board state
                        ChessBoard curr_board_state = new ChessBoard(MoveGenerator.white_pawns, MoveGenerator.white_knights, MoveGenerator.white_bishops, MoveGenerator.white_queens, MoveGenerator.white_rooks, MoveGenerator.white_king, MoveGenerator.black_pawns, MoveGenerator.black_knights, MoveGenerator.black_bishops, MoveGenerator.black_queens, MoveGenerator.black_rooks, MoveGenerator.black_king);

                        //Console.WriteLine("AI board state before ai runs " + Convert.ToString((long)curr_board_state.occupied, 2));

                        //code below is for updating frontend
                        Move ai_move = getNextMove(curr_board_state);


                        //Console.WriteLine("AI Moved piece is a " + this.pieces_dict[to_location].Type);
                        

                        //Console.WriteLine("AI board state after  ai runs " + Convert.ToString((long)curr_board_state.bestState.occupied, 2));

                        Console.WriteLine("AI Move " + ai_move.moved_type + " from " + ai_move.from_rank + " " + ai_move.from_file + " to " + ai_move.to_rank + " " + ai_move.to_file + " Cap " + ai_move.cap_type);
                        // test code 

                        //ArrayList all_moves = MoveGenerator.PossibleMovesPlayer();
                        //Console.WriteLine("Size is " + all_moves.Count);

                        //all_moves.Sort(new MoveCompare());  //sort the array so all capture will be placed at beginning

                        //foreach (Move a in all_moves)
                        //{
                        //    Console.WriteLine("Move " + a.moved_type + " from " + a.from_rank + " " + a.from_file + " to " + a.to_rank + " " + a.to_file + " Cap " + a.cap_type);
                        //}
                        //// test code end here

                        ////test code 
                        //System.Threading.Thread.Sleep(5000); // thread sleep. wait for algorithm to run
                        //ChessPiece ai_move_piece = this.pieces_dict[1];
                        

                        
                        //int priv_coor_x = ai_move_piece.Coor_X;
                        //int priv_coor_y = ai_move_piece.Coor_Y;

                        //ai_move_piece.Pos_X = 0;
                        //ai_move_piece.Pos_Y = 2;
                        //this.pieces_dict.Add(2, ai_move_piece);
                        //this.pieces_dict.Remove(1);
                        //test code end

                        //AI algorithm goes here.

                        Messenger.Default.Send(new MachineMoveMessage { Turn = this.turn, From_Rank=ai_move.from_rank, From_File = ai_move.from_file, To_Rank=ai_move.to_rank, To_File=ai_move.to_file});
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

        public Move getNextMove(ChessBoard curr_board_state)
        {
            startIterativeSearch(curr_board_state);
            MoveGenerator.setCurrentBitboards(curr_board_state.bestState.BP, curr_board_state.bestState.BR, curr_board_state.bestState.BN, curr_board_state.bestState.BB, curr_board_state.bestState.BQ, curr_board_state.bestState.BK, curr_board_state.bestState.WP, curr_board_state.bestState.WR, curr_board_state.bestState.WN, curr_board_state.bestState.WB, curr_board_state.bestState.WQ, curr_board_state.bestState.WK);
            return curr_board_state.bestState.move;

        }

        private void startIterativeSearch(ChessBoard init)
        {

            init.AlphaBetaSearch(int.MinValue, int.MaxValue, 5, true);
            //Console.WriteLine("Best state is "+init.bestState.eva);
               // if (DateTime.Now >= stop_time)
               // {
              //      break;
                //}
          //  }
        }



    }

   


}