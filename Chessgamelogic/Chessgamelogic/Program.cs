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
            

        }


        public Move AlphaBetaSearch(ChessBoard origin, ChessBoard cb,int alpha, int beta, int layer, bool max, Player one, Player two)
        {
            Move returnMove = new Move();
            if (layer == 0 | cb.check(one, two) == true)
            {
                returnMove = cb.getMove(origin);
                returnMove.value = cb.evaluateBoard();
                return returnMove;
            }
            else if (max == true)
            {
                List<ChessBoard> chessboards = cb.generateChessBoards();
                foreach (ChessBoard CB in chessboards)
                {
                    Move result = AlphaBetaSearch(origin, CB, alpha, beta, layer - 1, false, two, one);
                    if (result.value > alpha)
                    {
                        alpha = result.value;
                        returnMove = result;
                    }

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return returnMove;
            }
            else
            {
                List<ChessBoard> chessboards = cb.generateChessBoards();
                foreach (ChessBoard CB in chessboards)
                {
                    Move result = AlphaBetaSearch(origin, CB, alpha, beta, layer - 1, true, two, one);
                    if (result.value < beta)
                    {
                        beta = result.value;
                        returnMove = result;
                    }

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return returnMove;
            }
        }
    }

    public class ChessBoard
    {
        long WP = 0L, WN = 0L, WB = 0L, WQ = 0L, WR = 0L, WK = 0L,
                BP = 0L, BN = 0L, BB = 0L, BQ = 0L, BR = 0L, BK = 0L;

        public ChessBoard()
        {

            string[,] chessboard = new string[8, 8] {
            {"r","n","b","q","k","b","n","r"},   //black
            {"p","p","p","p","p","p","p","p"},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {"P","P","P","P","P","P","P","P"},   //white
            {"R","N","B","Q","K","B","N","R"}
            };
            arrayToBitBoard(chessboard);
            // Console.Write(Convert.ToString(WR+WK, 2)[3]);
            // Console.WriteLine();
            //Console.Write(BP | WP);
            // Console.Write(Convert.ToString(WN, 2));
            // Console.WriteLine();

            // Console.Write(Convert.ToString(BQ, 2));
            drawArray();
            //drawWhiteQueenArray(WQ);
            Console.ReadLine();

        }

        

        public void Copyboard(long WP, long WN, long WB, long WQ, long WR, long WK, long BP, long BN, long BB, long BQ, long BR, long BK)
        {
            this.WP = WP;
            this.WN = WN;
            this.WB = WB;
            this.WQ = WQ;
            this.WR = WR;
            this.WK = WK;
            this.BP = BP;
            this.BN = BN;
            this.BB = BB;
            this.BQ = BQ;
            this.BR = BR;
            this.BK = BK;
        }

        public void arrayToBitBoard(string[,] chessboard)
        {
            string binary;     //64-bit string
            for (int i = 0; i < 64; i++)
            {

                binary = "0000000000000000000000000000000000000000000000000000000000000000";
                binary = binary.Substring(i + 1) + "1" + binary.Substring(0, i);
                switch (chessboard[i / 8, i % 8])  //12 bitboard of each piece 
                {


                    case "P":
                        this.WP += convertStringToBitboard(binary);
                        break;
                    case "R":
                        this.WR += convertStringToBitboard(binary);
                        break;
                    case "N":
                        this.WN += convertStringToBitboard(binary);
                        break;
                    case "B":
                        this.WB += convertStringToBitboard(binary);
                        break;
                    case "Q":
                        this.WQ += convertStringToBitboard(binary);
                        break;
                    case "K":
                        this.WK += convertStringToBitboard(binary);
                        break;
                    case "p":
                        this.BP += convertStringToBitboard(binary);
                        break;
                    case "r":
                        this.BR += convertStringToBitboard(binary);
                        break;
                    case "n":
                        this.BN += convertStringToBitboard(binary);
                        break;
                    case "b":
                        this.BB += convertStringToBitboard(binary);
                        break;
                    case "q":
                        this.BQ += convertStringToBitboard(binary);
                        break;
                    case "k":
                        this.BK += convertStringToBitboard(binary);
                        break;
                    case " ":
                        break;

                }
            }
        }

        public long convertStringToBitboard(string binary)
        {

            if (binary[0].Equals('0'))
            {
                return Convert.ToInt64(binary, 2);
            }
            else
            {
                return Convert.ToInt64("1" + binary.Substring(2), 2) * 2;
            }
        }

        public void drawWhiteQueenArray(long WQ)
        {
            string[,] chessboard_revert = new string[8, 8];
            for (int i = 0; i < 64; i++)
            {
                if (((WQ >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "Q"; }
            }
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (chessboard_revert[row, col] == null || chessboard_revert[row, col] == " ")
                        Console.Write("*");
                    else
                        Console.Write(chessboard_revert[row, col]);
                }
                Console.WriteLine();
            }

        }

        public void drawArray()
        {

            string[,] chessboard_revert = new string[8, 8];
            for (int i = 0; i < 64; i++)
            {
                if (((this.WP >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "P"; }
                if (((this.WN >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "N"; }
                if (((this.WB >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "B"; }
                if (((this.WQ >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "Q"; }
                if (((this.WK >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "K"; }
                if (((this.WR >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "R"; }
                if (((this.BP >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "p"; }
                if (((this.BN >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "n"; }
                if (((this.BB >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "b"; }
                if (((this.BQ >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "q"; }
                if (((this.BK >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "k"; }
                if (((this.BR >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "r"; }
            }


            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (chessboard_revert[row, col] == null || chessboard_revert[row, col] == " ")
                        Console.Write("*");
                    else
                        Console.Write(chessboard_revert[row, col]);
                }
                Console.WriteLine();
            }
        }

        // TO DO
        public int evaluateBoard()
        {
            throw new NotImplementedException();
        }

        // TO DO
        public void updateBoard(Move move)
        {
            throw new NotImplementedException();
        }

        // TO DO
        public Move getMove(ChessBoard CB)
        {
            throw new NotImplementedException();
        }

        // TO DO
        public List<ChessBoard> generateChessBoards()
        {
            throw new NotImplementedException();
        }
    
        // TO DO
        public bool check(Player one, Player two)
        {
            throw new NotImplementedException();
        }
    }

    public class Move
    {
        public int srcX { get; set; }
        public int srcY { get; set; }
        public int dstX { get; set; }
        public int dstY { get; set; }
        public int value { get; set; }
    }

    // TO DO
    public class Player
    {

    }

}
