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
        ChessBoard CB;

        public AI(Player player, int seconds)
        {
            this.player = player;
            this.seconds = seconds;
        }

        public AI(Player player, int seconds, ChessBoard cb)
        {
            this.player = player;
            this.seconds = seconds;
            this.CB = cb;
        }

        //TO DO
        //public Move getNextMove()
        //{

        //}

        private void startIterativeSearch(DateTime date)
        {
            DateTime target = date.AddSeconds((double)seconds);
            DateTime currentTime = new DateTime();

            for (int i = 1; i < 100; i++)
            {
                CB.AlphaBetaSearch(int.MinValue, int.MaxValue, i, true);
                currentTime = new DateTime();
                if (currentTime > target)
                {
                    break;
                }
            }
        }
    }
}

