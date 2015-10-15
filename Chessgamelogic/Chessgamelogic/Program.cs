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
            Console.WriteLine("Evalution for black = "+ CB.evaluateBoard());
            Console.WriteLine("Occupied: " + CB.occupied);
            Console.ReadLine();
        }



        public class ChessBoard
        {
            ulong WP = 0L, WN = 0L, WB = 0L, WQ = 0L, WR = 0L, WK = 0L,
                    BP = 0L, BN = 0L, BB = 0L, BQ = 0L, BR = 0L, BK = 0L;
            public ulong fullboard, occupied, empty, enemy, enemyAndEmpty;

            int[] PawnTable, KnightTable, BishopTable, RookTable, QueenTable, KingTableO, KingTableE, Mirror64;
      

            public ChessBoard()
            {
                WP = 0x000000000000ff00;
                BP = 0x00ff000000000000;
                WN = 0x0000000000000042;
                BN = 0x4200000000000000;
                WB = 0x0000000000000024;
                BB = 0x2400000000000000;
                WR = 0x0000000000000081;
                BR = 0x8100000000000000;
                WQ = 0x0000000000000008;
                BQ = 0x0800000000000000;
                WK = 0x0000000000000010;
                BK = 0x1000000000000000;

                createUsefullBitboards();


                drawArray();



                //    string[,] chessboard = new string[8, 8] {
                //{"r","n","b","q","k","b","n","r"},   //black
                //{"p","p","p","p","p","p","p","p"},
                //{" "," "," "," "," "," "," "," "},
                //{" "," "," "," "," "," "," "," "},
                //{" "," "," "," "," "," "," "," "},
                //{" "," "," "," "," "," "," "," "},
                //{"P","P","P","P","P","P","P","P"},   //white
                //{"R","N","B","Q","K","B","N","R"}
                //};
                //    arrayToBitBoard(chessboard);
                // Console.Write(Convert.ToString(WR+WK, 2));
                // Console.WriteLine();
                //Console.Write(BP | WP);
                // Console.Write(Convert.ToString(WN, 2));
                // Console.WriteLine();

                // Console.Write(Convert.ToString(BQ, 2));

                //drawWhiteQueenArray(WQ);


            }

            private void createUsefullBitboards()
            {
                fullboard = 0xffffffffffffffff;
                occupied = WP | BP | WR | BR | WN | BN | WB | BB | WQ | BQ | WK | BK;
                empty = occupied ^ fullboard;
                enemy = WP | WR | WN | WB | WQ | WK;
                enemyAndEmpty = empty | enemy;


                PawnTable = new int[] {
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                10  ,   10  ,   0   ,   -10 ,   -10 ,   0   ,   10  ,   10  ,
                5   ,   0   ,   0   ,   5   ,   5   ,   0   ,   0   ,   5   ,
                0   ,   0   ,   10  ,   20  ,   20  ,   10  ,   0   ,   0   ,
                5   ,   5   ,   5   ,   10  ,   10  ,   5   ,   5   ,   5   ,
                10  ,   10  ,   10  ,   20  ,   20  ,   10  ,   10  ,   10  ,
                20  ,   20  ,   20  ,   30  ,   30  ,   20  ,   20  ,   20  ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

                KnightTable = new int[] {
                0   ,   -10 ,   0   ,   0   ,   0   ,   0   ,   -10 ,   0   ,
                0   ,   0   ,   0   ,   5   ,   5   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   10  ,   10  ,   10  ,   10  ,   0   ,   0   ,
                0   ,   0   ,   10  ,   20  ,   20  ,   10  ,   5   ,   0   ,
                5   ,   10  ,   15  ,   20  ,   20  ,   15  ,   10  ,   5   ,
                5   ,   10  ,   10  ,   20  ,   20  ,   10  ,   10  ,   5   ,
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

                BishopTable = new int[] {
                0   ,   0   ,   -10 ,   0   ,   0   ,   -10 ,   0   ,   0   ,
                0   ,   0   ,   0   ,   10  ,   10  ,   0   ,   0   ,   0   ,
                0   ,   0   ,   10  ,   15  ,   15  ,   10  ,   0   ,   0   ,
                0   ,   10  ,   15  ,   20  ,   20  ,   15  ,   10  ,   0   ,
                0   ,   10  ,   15  ,   20  ,   20  ,   15  ,   10  ,   0   ,
                0   ,   0   ,   10  ,   15  ,   15  ,   10  ,   0   ,   0   ,
                0   ,   0   ,   0   ,   10  ,   10  ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

                RookTable = new int[] {
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0   ,
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0   ,
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0   ,
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0   ,
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0   ,
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0   ,
                25  ,   25  ,   25  ,   25  ,   25  ,   25  ,   25  ,   25  ,
                0   ,   0   ,   5   ,   10  ,   10  ,   5   ,   0   ,   0
                };

                KingTableO = new int[] {
                0   ,   5   ,   5   ,   -10 ,   -10 ,   0   ,   10  ,   5   ,
                -30 ,   -30 ,   -30 ,   -30 ,   -30 ,   -30 ,   -30 ,   -30 ,
                -50 ,   -50 ,   -50 ,   -50 ,   -50 ,   -50 ,   -50 ,   -50 ,
                -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,
                -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,
                -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,
                -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,
                -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70 ,   -70
                };

                KingTableE = new int[] {
                -50 ,   -10 ,   0   ,   0   ,   0   ,   0   ,   -10 ,   -50 ,
                -10,    0   ,   10  ,   10  ,   10  ,   10  ,   0   ,   -10 ,
                0   ,   10  ,   20  ,   20  ,   20  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   40  ,   40  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   40  ,   40  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   20  ,   20  ,   20  ,   10  ,   0   ,
                -10,    0   ,   10  ,   10  ,   10  ,   10  ,   0   ,   -10 ,
                -50 ,   -10 ,   0   ,   0   ,   0   ,   0   ,   -10 ,   -50
                };  


                Mirror64 = new int[] {
                56  ,   57  ,   58  ,   59  ,   60  ,   61  ,   62  ,   63  ,
                48  ,   49  ,   50  ,   51  ,   52  ,   53  ,   54  ,   55  ,
                40  ,   41  ,   42  ,   43  ,   44  ,   45  ,   46  ,   47  ,
                32  ,   33  ,   34  ,   35  ,   36  ,   37  ,   38  ,   39  ,
                24  ,   25  ,   26  ,   27  ,   28  ,   29  ,   30  ,   31  ,
                16  ,   17  ,   18  ,   19  ,   20  ,   21  ,   22  ,   23  ,
                8   ,   9   ,   10  ,   11  ,   12  ,   13  ,   14  ,   15  ,
                0   ,   1   ,   2   ,   3   ,   4   ,   5   ,   6   ,   7
                };

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

                createUsefullBitboards();
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
                            Console.Write(chessboard_revert[7-row, 7-col]);
                    }
                    Console.WriteLine();
                }
            }

            // TO DO
            public int evaluateBoard()
            {


                int blackPoints = 0;
                int whitePoints = 0;


                for (int i = 0; i<64;i++)
                {
                    if (((occupied >> i) & 1) == 1)
                    {
                        if (((enemy >> i) & 1) == 1)
                        {
                            //Evaluation of White pieces
                            if (((WB >> i) & 1) == 1)
                            {
                                //Evaluation of White Bishop
                                whitePoints += BishopTable[i];

                            }
                            else if (((WK >> i) & 1) == 1)
                            {
                                //Evaluation of White King
                                whitePoints += KingTableO[i];

                            }
                            else if (((WN >> i) & 1) == 1)
                            {
                                //Evaluation of White Knight 
                                whitePoints += KnightTable[i];
                            }
                            else if (((WP >> i) & 1) == 1)
                            {
                                //Evaluation of White Pawns 
                                whitePoints += PawnTable[i];
                            }
                            else if (((WQ >> i) & 1) == 1)
                            {
                                //Evaluation of White Queen 
                                //whitePoints += QueenTable[i];
                            }
                            else if (((WR >> i) & 1) == 1)
                            {
                                //Evaluation of White Rook 
                                whitePoints += RookTable[i];
                            }

                        } else
                        {
                            //Evaluation of Black pieces 
                            if (((BB >> i) & 1) == 1)
                            {
                                //Evaluation of Black Bishop 
                                blackPoints += BishopTable[Mirror64[i]];
                            }
                            else if (((BK >> i) & 1) == 1)
                            {
                                //Evaluation of Black King 
                                blackPoints += KingTableO[Mirror64[i]];
                            }
                            else if (((BN >> i) & 1) == 1)
                            {
                                //Evaluation of Black Knight 
                                blackPoints += KnightTable[Mirror64[i]];
                            }
                            else if (((BP >> i) & 1) == 1)
                            {
                                //Evaluation of Black Pawns 
                                blackPoints += PawnTable[Mirror64[i]];
                            }
                            else if (((BQ >> i) & 1) == 1)
                            {
                                //Evaluation of Black Queen 
                                //blackPoints += QueenTable[Mirror64[i]];
                            }
                            else if (((BR >> i) & 1) == 1)
                            {
                                //Evaluation of Black Rook 
                                blackPoints += RookTable[Mirror64[i]];
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
