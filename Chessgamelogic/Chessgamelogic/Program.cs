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



        public class ChessBoard
        {
            ulong WP = 0L, WN = 0L, WB = 0L, WQ = 0L, WR = 0L, WK = 0L,
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



            public void Copyboard(ulong WP, ulong WN, ulong WB, ulong WQ, ulong WR, ulong WK, ulong BP, ulong BN, ulong BB, ulong BQ, ulong BR, ulong BK)
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
                            WP += convertStringToBitboard(binary);
                            break;
                        case "R":
                            WR += convertStringToBitboard(binary);
                            break;
                        case "N":
                            WN += convertStringToBitboard(binary);
                            break;
                        case "B":
                            WB += convertStringToBitboard(binary);
                            break;
                        case "Q":
                            WQ += convertStringToBitboard(binary);
                            break;
                        case "K":
                            WK += convertStringToBitboard(binary);
                            break;
                        case "p":
                            BP += convertStringToBitboard(binary);
                            break;
                        case "r":
                            BR += convertStringToBitboard(binary);
                            break;
                        case "n":
                            BN += convertStringToBitboard(binary);
                            break;
                        case "b":
                            BB += convertStringToBitboard(binary);
                            break;
                        case "q":
                            BQ += convertStringToBitboard(binary);
                            break;
                        case "k":
                            BK += convertStringToBitboard(binary);
                            break;
                        case " ":
                            break;

                    }
                }
            }

            public ulong convertStringToBitboard(string binary)
            {

                if (binary[0].Equals('0'))
                {
                    return Convert.ToUInt64(binary, 2);
                }
                else
                {
                    return Convert.ToUInt64("1" + binary.Substring(2), 2) * 2;
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
                ulong blackpieces = BB+BK+BN+BP+BQ+BR;
                ulong whitepices = WB + WK + WN + WP + WQ + WR;
                ulong allpieces = blackpieces + whitepices;

                int blackPoints = 0;
                int whitePoints = 0;


                for (int i = 0; i<64;i++)
                {
                    if (((allpieces >> i) & 1) == 1)
                    {
                        if (((blackpieces >> i) & 1) == 1)
                        {
                            //Evaluation of Black pieces 
                            if (((BB >> i) & 1) == 1)
                            {
                                //Evaluation of Black Bishop 

                            } else if (((BK >> i) & 1) == 1)
                            {
                                //Evaluation of Black King 

                            }
                            else if (((BN >> i) & 1) == 1)
                            {
                                //Evaluation of Black Knight 

                            }
                            else if (((BP >> i) & 1) == 1)
                            {
                                //Evaluation of Black Pawns 

                            }
                            else if (((BQ >> i) & 1) == 1)
                            {
                                //Evaluation of Black Queen 

                            }
                            else if (((BR >> i) & 1) == 1)
                            {
                                //Evaluation of Black Rook 

                            }

                        } else
                        {
                            //Evaluation of White pieces
                            if (((WB >> i) & 1) == 1)
                            {
                                //Evaluation of White Bishop 

                            }
                            else if (((WK >> i) & 1) == 1)
                            {
                                //Evaluation of White King 

                            }
                            else if (((WN >> i) & 1) == 1)
                            {
                                //Evaluation of White Knight 

                            }
                            else if (((WP >> i) & 1) == 1)
                            {
                                //Evaluation of White Pawns 

                            }
                            else if (((WQ >> i) & 1) == 1)
                            {
                                //Evaluation of White Queen 

                            }
                            else if (((WR >> i) & 1) == 1)
                            {
                                //Evaluation of White Rook 

                            }
                        }
                    }
                    else { }
                }
                return blackPoints - whitePoints;
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
            public bool check()
            {
                throw new NotImplementedException();
            }

            public Move AlphaBetaSearch(int alpha, int beta, int layer, bool max)
            {
                Move returnMove = new Move();
                if (layer == 0 || check() == true)
                {
                    //returnMove = getMove();
                    returnMove.value = evaluateBoard();
                    return returnMove;
                }
                else if (max == true)
                {
                    List<ChessBoard> chessboards = generateChessBoards();
                    foreach (ChessBoard CB in chessboards)
                    {
                        Move result = AlphaBetaSearch(alpha, beta, layer - 1, false);
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
                    List<ChessBoard> chessboards = generateChessBoards();
                    foreach (ChessBoard CB in chessboards)
                    {
                        Move result = AlphaBetaSearch(alpha, beta, layer - 1, true);
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
