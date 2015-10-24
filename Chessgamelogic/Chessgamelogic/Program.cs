using ChessGameAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessgamelogic
{
    class Program
    {
        
        static void Main(string[] args)
        {
            MoveGenerator moveGenerator = new MoveGenerator();
            MoveGenerator.player_color = false;
            AI black = new AI(Player.Black, 1);
            //AI white = new AI(Player.White, 1);
            //white.CB.drawArray();
            //white.getNextMove();
            for (int i = 0; i<5;i++)
            {
                //white.CB.bestState.drawArray();
                //black.CB = white.CB.bestState;
                black.getNextMove();

                black.CB.bestState.drawArray();
                //white.CB = black.CB.bestState;
                //white.getNextMove();
            }
        }
    }
}


       



    
