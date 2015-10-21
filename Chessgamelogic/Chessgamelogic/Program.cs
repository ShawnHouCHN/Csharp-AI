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
            Console.WriteLine("Evaluation for black = "+ CB.evaluateBoard());
            Console.WriteLine("Occupied: " + CB.occupied);
            Console.WriteLine("empty: " + CB.empty);
            Console.ReadLine();
        }



        public class ChessBoard
        {
            ulong WP = 0L, WN = 0L, WB = 0L, WQ = 0L, WR = 0L, WK = 0L,
                    BP = 0L, BN = 0L, BB = 0L, BQ = 0L, BR = 0L, BK = 0L;
            public ulong fullboard, occupied, empty, enemy, enemyOrEmpty;

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
                WQ = 0x0000008000000008;
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
                occupied = (WP | BP | WR | BR | WN | BN | WB | BB | WQ | BQ | WK | BK);
                empty = (occupied ^ fullboard);
                enemy = (WP | WR | WN | WB | WQ | WK);
                enemyOrEmpty = (empty | enemy);


                PawnTable = new int[] {
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                7   ,   7   ,   13  ,   23  ,   26  ,   13  ,   7   ,   7   ,
                -2  ,   -2  ,   4   ,   12  ,   15  ,   4   ,   -2  ,   -2  ,
                -3  ,   -3  ,   2   ,   9   ,   11  ,   2   ,   -3  ,   -3  ,
                -4  ,   -4  ,   0   ,   6   ,   8   ,   0   ,   -4  ,   -4  ,
                -4  ,   -4  ,   0   ,   4   ,   6   ,   0   ,   -4  ,   -4  ,
                -1  ,   -1  ,   1   ,   5   ,   6   ,   1   ,   -1  ,   -1  ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

                KnightTable = new int[] {
                -2  ,   2   ,   7   ,   9   ,   9   ,   7   ,   2   ,   -2  ,
                1   ,   4   ,   12  ,   13  ,   13  ,   12  ,   4   ,   1   ,
                5   ,   11  ,   18  ,   19  ,   19  ,   18  ,   11  ,   5   ,
                3   ,   10  ,   14  ,   14  ,   14  ,   14  ,   10  ,   3   ,
                0   ,   5   ,   8   ,   9   ,   9   ,   8   ,   5   ,   0   ,
                -3  ,   1   ,   3   ,   4   ,   4   ,   3   ,   1   ,   -3  ,
                -5  ,   -3  ,   -1  ,   0   ,   0   ,   -1  ,   -3  ,   -5  ,
                -7  ,   -5  ,   -4  ,   -2  ,   -2  ,   -4  ,   -5  ,   -7
                };

                BishopTable = new int[] {
                2   ,   3   ,   4   ,   4   ,   4   ,   4   ,   3   ,   2   ,
                4   ,   7   ,   7   ,   7   ,   7   ,   7   ,   7   ,   4   ,
                3   ,   5   ,   6   ,   6   ,   6   ,   6   ,   5   ,   3   ,
                3   ,   5   ,   7   ,   7   ,   7   ,   7   ,   5   ,   3   ,
                4   ,   5   ,   6   ,   8   ,   8   ,   6   ,   5   ,   4   ,
                4   ,   5   ,   5   ,   -2  ,   -2  ,   5   ,   5   ,   4   ,
                5   ,   5   ,   5   ,   3   ,   3   ,   5   ,   5   ,   5   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

                RookTable = new int[] {
                9   ,   9   ,   11  ,   10  ,   11  ,   9   ,   9   ,   9   ,
                4   ,   6   ,   7   ,   9   ,   9   ,   7   ,   6   ,   4   ,
                9   ,   10  ,   10  ,   11  ,   11  ,   10  ,   10  ,   9   ,
                8   ,   8   ,   8   ,   9   ,   9   ,   8   ,   8   ,   8   ,
                6   ,   6   ,   5   ,   6   ,   6   ,   5   ,   6   ,   6   ,
                4   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   4   ,
                3   ,   4   ,   4   ,   6   ,   6   ,   4   ,   4   ,   3  ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

                KingTableO = new int[] {
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                };

                KingTableE = new int[] {
                -50 ,   -10 ,   0   ,   0   ,   0   ,   0   ,   -10 ,   -50 ,
                -10 ,    0  ,   10  ,   10  ,   10  ,   10  ,   0   ,   -10 ,
                0   ,   10  ,   20  ,   20  ,   20  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   40  ,   40  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   40  ,   40  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   20  ,   20  ,   20  ,   10  ,   0   ,
                -10,    0   ,   10  ,   10  ,   10  ,   10  ,   0   ,   -10 ,
                -50 ,   -10 ,   0   ,   0   ,   0   ,   0   ,   -10 ,   -50
                };

                QueenTable = new int[] {
                2   ,   3   ,   4   ,   3   ,   4   ,   3   ,   3   ,   2   ,
                2   ,   3   ,   4   ,   4   ,   4   ,   4   ,   3   ,   2   ,
                3   ,   4   ,   4   ,   4   ,   4   ,   4   ,   4   ,   3   ,
                3   ,   3   ,   4   ,   4   ,   4   ,   4   ,   3   ,   3   ,
                2   ,   3   ,   3   ,   4   ,   4   ,   3   ,   3   ,   2   ,
                2   ,   2   ,   2   ,   3   ,   3   ,   2   ,   2   ,   2   ,
                2   ,   2   ,   2   ,   2   ,   2   ,   2   ,   2   ,   2   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
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
                    if (((WP >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "P"; }
                    if (((WN >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "N"; }
                    if (((WB >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "B"; }
                    if (((WQ >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "Q"; }
                    if (((WK >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "K"; }
                    if (((WR >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "R"; }
                    if (((BP >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "p"; }
                    if (((BN >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "n"; }
                    if (((BB >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "b"; }
                    if (((BQ >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "q"; }
                    if (((BK >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "k"; }
                    if (((BR >> i) & 1) == 1) { chessboard_revert[i / 8, i % 8] = "r"; }
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
                                whitePoints += BishopTable[(8 * (7 - i / 8) + i % 8)];
                                
                            }
                            else if (((WK >> i) & 1) == 1)
                            {
                                //Evaluation of White King
                                whitePoints += KingTableO[(8 * (7 - i / 8) + i % 8)];
                                
                            }
                            else if (((WN >> i) & 1) == 1)
                            {
                                //Evaluation of White Knight 
                                whitePoints += KnightTable[(8 * (7 - i / 8) + i % 8)];
                                
                            }
                            else if (((WP >> i) & 1) == 1)
                            {
                                //Evaluation of White Pawns 
                                whitePoints += PawnTable[(8 * (7 - i / 8) + i % 8)];

                                //Evaluation of Double Pawn
                                if (((WP >> (i+8)) & 1) ==1 ) {
                                    whitePoints -= 7;
                                }
                                
                            }
                            else if (((WQ >> i) & 1) == 1)
                            {
                                //Evaluation of White Queen 
                                whitePoints += QueenTable[(8 * (7 - i / 8) + i % 8)];
                                
                            }
                            else if (((WR >> i) & 1) == 1)
                            {
                                //Evaluation of White Rook 
                                whitePoints += RookTable[(8 * (7 - i / 8) + i % 8)];
                                
                            }

                        } else
                        {
                            //Evaluation of Black pieces 
                            if (((BB >> i) & 1) == 1)
                            {
                                //Evaluation of Black Bishop 
                                blackPoints += BishopTable[Mirror64[(8 * (7 - i / 8) + i % 8)]];

                            }
                            else if (((BK >> i) & 1) == 1)
                            {
                                //Evaluation of Black King 
                                blackPoints += KingTableO[Mirror64[(8 * (7 - i / 8) + i % 8)]];
                                
                            }
                            else if (((BN >> i) & 1) == 1)
                            {
                                //Evaluation of Black Knight 
                                blackPoints += KnightTable[Mirror64[(8 * (7 - i / 8) + i % 8)]];
                                
                            }
                            else if (((BP >> i) & 1) == 1)
                            {
                                //Evaluation of Black Pawns 
                                blackPoints += PawnTable[Mirror64[(8 * (7 - i / 8) + i % 8)]];

                                //Evaluation of Double Pawn
                                if (((BP >> (i - 8)) & 1) == 1)
                                {
                                    whitePoints += -7;
                                }
                            }
                            else if (((BQ >> i) & 1) == 1)
                            {
                                //Evaluation of Black Queen 
                                blackPoints += QueenTable[Mirror64[(8 * (7 - i / 8) + i % 8)]];
                                
                            }
                            else if (((BR >> i) & 1) == 1)
                            {
                                //Evaluation of Black Rook 
                                blackPoints += RookTable[Mirror64[(8 * (7 - i / 8) + i % 8)]];
                                
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
