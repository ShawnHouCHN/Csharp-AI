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
            MoveGenerator.setInitBitboards( true, 0x00ff000000000000, 0x8100000000000000, 0x4200000000000000, 0x2400000000000000, 0x0800000000000000, 0x1000000000000000, 0x000000000000ff00, 0x000000000000081, 0x000000000000042, 0x000000000000024, 0x0000000000000008, 0x0000000000000010);
            
            AI black = new AI(Player.Black, 1);

            for (int i = 0; i < 10; i++)
            {
                black.CB.drawArray();

                black.getNextMove();

                Console.ReadLine();

                black.CB = black.CB.bestState;
            }

            //AI white = new AI(Player.White, 1);
            //white.CB.drawArray();
            //white.getNextMove();
            //for (int i = 0; i<5;i++)
            //{
            //    //white.CB.bestState.drawArray();
            //    //black.CB = white.CB.bestState;
            //    black.getNextMove();

            //    black.CB.bestState.drawArray();

            //    black.CB = black.CB.bestState;
            //    //white.CB = black.CB.bestState;
            //    //white.getNextMove();
            //}
        }
    }
}


       



    
