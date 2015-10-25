using Chessgamelogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGameAI
{
    class AI
    {
        Player player;
        double seconds;
        public ChessBoard CB { get; set; }

        public AI(Player player, double seconds)
        {
            this.player = player;
            this.seconds = seconds;
            CB = new ChessBoard(MoveGenerator.white_pawns,MoveGenerator.white_knights,MoveGenerator.white_bishops,MoveGenerator.white_queens,MoveGenerator.white_rooks,MoveGenerator.white_king,MoveGenerator.black_pawns,MoveGenerator.black_knights,MoveGenerator.black_bishops,MoveGenerator.black_queens,MoveGenerator.black_rooks,MoveGenerator.black_king,null);
        }

        public AI(Player player, double seconds, ChessBoard cb)
        {
            this.player = player;
            this.seconds = seconds;
            this.CB = cb;
        }

        //TO DO
        public Move getNextMove()
        {
            startIterativeSearch();

            MoveGenerator.setCurrentBitboards(CB.bestState.BP, CB.bestState.BR, CB.bestState.BN, CB.bestState.BB, CB.bestState.BQ, CB.bestState.BK, CB.bestState.WP, CB.bestState.WR, CB.bestState.WN, CB.bestState.WB, CB.bestState.WQ, CB.bestState.WK);
            return CB.bestState.move;
        }

        private void startIterativeSearch()
        {
            DateTime currentTime = DateTime.Now;
            DateTime target = currentTime.AddSeconds(seconds);

           for (int i = 1; i < 100; i++)
            {

                CB.AlphaBetaSearch(int.MinValue, int.MaxValue, i, true);
                Console.WriteLine("Searching in layer: {0} through {1} boardstates", i,MoveGenerator.searchcounter);
                currentTime = DateTime.Now;
                
                if (target.CompareTo(currentTime)<0)
                {
                    CB.bestState.drawArray();
                    Console.WriteLine("Evaluation of Best state: {0}", CB.bestState.evaluateBoard(true, CB));
                    MoveGenerator.searchcounter = 0;
                    break;
                }
            }
        }
    }
}

