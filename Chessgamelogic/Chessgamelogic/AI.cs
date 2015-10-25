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
        int seconds;
        public ChessBoard CB { get; set; }

        public AI(Player player, int seconds)
        {
            this.player = player;
            this.seconds = seconds;
            CB = new ChessBoard();
        }

        public AI(Player player, int seconds, ChessBoard cb)
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
            return CB.bestState.nextMove;
        }

        private void startIterativeSearch()
        {
            DateTime currentTime = new DateTime();
            DateTime target = currentTime.AddSeconds(10);
            
            for (int i = 1; i < 100; i++)
            {
                CB.AlphaBetaSearch(int.MinValue, int.MaxValue, i, true);
                currentTime = new DateTime();
                if (target.CompareTo(currentTime) > 0)
                {
                    CB.bestState.drawArray();
                    System.Threading.Thread.Sleep(2000);
                    break;
                }
            }
        }
    }
}

