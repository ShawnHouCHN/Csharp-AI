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
            ChessBoard CB = new ChessBoard();
            Console.WriteLine("Evaluation for black = " + CB.evaluateBoard(true));
            
            Console.ReadLine();
        }
    }
}


       



    
