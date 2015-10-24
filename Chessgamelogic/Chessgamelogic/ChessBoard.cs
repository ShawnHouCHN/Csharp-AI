using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessgamelogic
{
    public class ChessBoard
    {
        public Player player { get; set; }
        public ulong WP = 0x000000000000ff00, WN = 0x0000000000000042, WB = 0x0000000000000024, WQ = 0x0000000000000008, WR = 0x0000000000000081, WK = 0x0000000000000010,
                      BP = 0x00ff000000000000, BN = 0x4200000000000000, BB = 0x2400000000000000, BQ = 0x0800000000000000, BR = 0x8100000000000000, BK = 0x1000000000000000;

        private ulong occupied, empty, whitePieces, enemyOrEmpty;

        //private int[] PawnTable, KnightTable, BishopTable, RookTable, QueenTable, KingTableO, KingTableE, Mirror64;

        public ChessBoard bestState;

        public Move nextMove { get; private set; }

        private static int[] PawnTable = new int[] {
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                7   ,   7   ,   13  ,   23  ,   26  ,   13  ,   7   ,   7   ,
                -2  ,   -2  ,   4   ,   12  ,   15  ,   4   ,   -2  ,   -2  ,
                -3  ,   -3  ,   2   ,   9   ,   11  ,   2   ,   -3  ,   -3  ,
                -4  ,   -4  ,   0   ,   6   ,   8   ,   0   ,   -4  ,   -4  ,
                -4  ,   -4  ,   0   ,   4   ,   6   ,   0   ,   -4  ,   -4  ,
                -1  ,   -1  ,   1   ,   5   ,   6   ,   1   ,   -1  ,   -1  ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

        private static int[] KnightTable = new int[] {
                -2  ,   2   ,   7   ,   9   ,   9   ,   7   ,   2   ,   -2  ,
                1   ,   4   ,   12  ,   13  ,   13  ,   12  ,   4   ,   1   ,
                5   ,   11  ,   18  ,   19  ,   19  ,   18  ,   11  ,   5   ,
                3   ,   10  ,   14  ,   14  ,   14  ,   14  ,   10  ,   3   ,
                0   ,   5   ,   8   ,   9   ,   9   ,   8   ,   5   ,   0   ,
                -3  ,   1   ,   3   ,   4   ,   4   ,   3   ,   1   ,   -3  ,
                -5  ,   -3  ,   -1  ,   0   ,   0   ,   -1  ,   -3  ,   -5  ,
                -7  ,   -5  ,   -4  ,   -2  ,   -2  ,   -4  ,   -5  ,   -7
                };

        private static int[] BishopTable = new int[] {
                2   ,   3   ,   4   ,   4   ,   4   ,   4   ,   3   ,   2   ,
                4   ,   7   ,   7   ,   7   ,   7   ,   7   ,   7   ,   4   ,
                3   ,   5   ,   6   ,   6   ,   6   ,   6   ,   5   ,   3   ,
                3   ,   5   ,   7   ,   7   ,   7   ,   7   ,   5   ,   3   ,
                4   ,   5   ,   6   ,   8   ,   8   ,   6   ,   5   ,   4   ,
                4   ,   5   ,   5   ,   -2  ,   -2  ,   5   ,   5   ,   4   ,
                5   ,   5   ,   5   ,   3   ,   3   ,   5   ,   5   ,   5   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

        private static int[] RookTable = new int[] {
                9   ,   9   ,   11  ,   10  ,   11  ,   9   ,   9   ,   9   ,
                4   ,   6   ,   7   ,   9   ,   9   ,   7   ,   6   ,   4   ,
                9   ,   10  ,   10  ,   11  ,   11  ,   10  ,   10  ,   9   ,
                8   ,   8   ,   8   ,   9   ,   9   ,   8   ,   8   ,   8   ,
                6   ,   6   ,   5   ,   6   ,   6   ,   5   ,   6   ,   6   ,
                4   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   4   ,
                3   ,   4   ,   4   ,   6   ,   6   ,   4   ,   4   ,   3  ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

        private static int[] KingTableO = new int[] {
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,
                };

        private static int[] KingTableE = new int[] {
                -50 ,   -10 ,   0   ,   0   ,   0   ,   0   ,   -10 ,   -50 ,
                -10 ,    0  ,   10  ,   10  ,   10  ,   10  ,   0   ,   -10 ,
                0   ,   10  ,   20  ,   20  ,   20  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   40  ,   40  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   40  ,   40  ,   20  ,   10  ,   0   ,
                0   ,   10  ,   20  ,   20  ,   20  ,   20  ,   10  ,   0   ,
                -10,    0   ,   10  ,   10  ,   10  ,   10  ,   0   ,   -10 ,
                -50 ,   -10 ,   0   ,   0   ,   0   ,   0   ,   -10 ,   -50
                };

        private static int[] QueenTable = new int[] {
                2   ,   3   ,   4   ,   3   ,   4   ,   3   ,   3   ,   2   ,
                2   ,   3   ,   4   ,   4   ,   4   ,   4   ,   3   ,   2   ,
                3   ,   4   ,   4   ,   4   ,   4   ,   4   ,   4   ,   3   ,
                3   ,   3   ,   4   ,   4   ,   4   ,   4   ,   3   ,   3   ,
                2   ,   3   ,   3   ,   4   ,   4   ,   3   ,   3   ,   2   ,
                2   ,   2   ,   2   ,   3   ,   3   ,   2   ,   2   ,   2   ,
                2   ,   2   ,   2   ,   2   ,   2   ,   2   ,   2   ,   2   ,
                0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0
                };

        private static int[] Mirror64 = new int[] {
                56  ,   57  ,   58  ,   59  ,   60  ,   61  ,   62  ,   63  ,
                48  ,   49  ,   50  ,   51  ,   52  ,   53  ,   54  ,   55  ,
                40  ,   41  ,   42  ,   43  ,   44  ,   45  ,   46  ,   47  ,
                32  ,   33  ,   34  ,   35  ,   36  ,   37  ,   38  ,   39  ,
                24  ,   25  ,   26  ,   27  ,   28  ,   29  ,   30  ,   31  ,
                16  ,   17  ,   18  ,   19  ,   20  ,   21  ,   22  ,   23  ,
                8   ,   9   ,   10  ,   11  ,   12  ,   13  ,   14  ,   15  ,
                0   ,   1   ,   2   ,   3   ,   4   ,   5   ,   6   ,   7
                };

        private static ulong[] file = new ulong[]
        {
            0xf0f0f0f0f0f0f0f0,
            0x8080808080808080,
            0x4040404040404040,
            0x2020202020202020,
            0x1010101010101010,
            0x0f0f0f0f0f0f0f0f,
            0x0808080808080808,
            0x0404040404040404,
            0x0202020202020202,
            0x0101010101010101,
        };


        public ChessBoard()
        {
            createUsefullBitboards();
        }

        public ChessBoard(ulong WP, ulong WN, ulong WB, ulong WQ, ulong WR, ulong WK, ulong BP, ulong BN, ulong BB, ulong BQ, ulong BR, ulong BK)
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

            //this.player = currentPlayer;

            createUsefullBitboards();
        }

        public ChessBoard(ulong WP, ulong WN, ulong WB, ulong WQ, ulong WR, ulong WK, ulong BP, ulong BN, ulong BB, ulong BQ, ulong BR, ulong BK, Move previousMove)
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

            //this.player = currentPlayer;

            nextMove = previousMove;

            createUsefullBitboards();
        }


        private void createUsefullBitboards()
        {
            occupied = (WP | BP | WR | BR | WN | BN | WB | BB | WQ | BQ | WK | BK);
            empty = ~occupied;
            whitePieces = (WP | WR | WN | WB | WQ | WK);
            enemyOrEmpty = (empty | whitePieces);
        }

        //public void Copyboard(ulong WP, ulong WN, ulong WB, ulong WQ, ulong WR, ulong WK, ulong BP, ulong BN, ulong BB, ulong BQ, ulong BR, ulong BK)
        //{
        //    this.WP = WP;
        //    this.WN = WN;
        //    this.WB = WB;
        //    this.WQ = WQ;
        //    this.WR = WR;
        //    this.WK = WK;
        //    this.BP = BP;
        //    this.BN = BN;
        //    this.BB = BB;
        //    this.BQ = BQ;
        //    this.BR = BR;
        //    this.BK = BK;

        //    //createUsefullBitboards();
        //}

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
                    if (chessboard_revert[7 - row, 7 - col] == null || chessboard_revert[7 - row, 7 - col] == " ")
                        Console.Write("*");
                    else
                        Console.Write(chessboard_revert[7 - row, 7 - col]);
                }
                Console.WriteLine();
            }
        }

        // TO DO
        public int evaluateBoard(bool min_max)
        {
            int blackPoints = 0;
            int whitePoints = 0;

            // Evaluate pieces under threat
            MoveGenerator.setCurrentBitboards(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK);
            ArrayList moves;
            if (min_max)
            {
                moves = MoveGenerator.PossibleMovesMachine();
            }
            else
            {
                moves = MoveGenerator.PossibleMovesPlayer();
            }

            //differentiate between white and black
            foreach (Move m in moves)
            {
                if (m.cap_type != null)
                {
                    if (MoveGenerator.player_color)
                        blackPoints += 7;
                    else
                        whitePoints += 7;
                }
                else
                {
                    break;
                }
            }


            for (int i = 0; i < 64; i++)
            {
                // Evaluate positions of each piece on the board
                if (((occupied >> i) & 1) == 1)
                {
                    if (((whitePieces >> i) & 1) == 1)
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
                            if ((file[i % 8] & (WP & ((ulong)1<<(63-i))))>0)
                            {
                                whitePoints += -7;
                            }

                            //Evalution of pawn ramming
                            if (((BP >> (i + 8)) & 1) == 1 || ((BP >> (i - 8)) & 1) == 1)
                            {
                                whitePoints += -7;
                            }

                            //for (int f = 1; f < 8; f++)
                            //{
                            //    if ((f == 1 & ((BP >> (i + 8)) & 1) == 1))
                            //    {
                            //        whitePoints += -7;
                            //    }
                            //    if (((WP >> (i + 8 * f)) & 1) == 1)
                            //    {
                            //        whitePoints += -7;
                            //        break;
                            //    }
                            //}
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

                    }
                    else
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
                            if ((file[i % 8] & (WP & ((ulong)1 << (63 - i)))) > 0)
                            {
                                blackPoints += -7;
                            }

                            //Evalution of pawn ramming
                            if (((BP >> (i + 8)) & 1) == 1 || ((BP >> (i - 8)) & 1) == 1)
                            {
                                blackPoints += -7;
                            }

                            //Evaluation of Double Pawn
                            //for (int f = 1; f < 8; f++)
                            //{
                            //    if ((f == 1 & ((WP >> (i - 8)) & 1) == 1))
                            //    {
                            //        blackPoints += -7;
                            //    }

                            //    if (((BP >> (i - 8 * f)) & 1) == 1)
                            //    {
                            //        blackPoints += -7;
                            //        break;
                            //    }
                            //}
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
            if (MoveGenerator.player_color) //true = player use white
            {
                if (check() == 1) { blackPoints += 50; }
                return blackPoints - whitePoints;  //(machine point - player point)
            }
            else
            {
                if (check() == 2) { whitePoints += 50; }
                return whitePoints - blackPoints;
            }
        }

        // TO DO
        //public void updateBoard(Move move)
        //{
        //    throw new NotImplementedException();
        //}

        
        // TO DO
        public List<ChessBoard> generateChessBoards(bool min_max)
        {
            List<ChessBoard> theList = new List<ChessBoard>();
            if (min_max)
            {
                MoveGenerator.setCurrentBitboards(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK);
                
                ArrayList moves = MoveGenerator.PossibleMovesMachine();
                
                foreach (Move move in moves)
                {
                    ChessBoard cb = new ChessBoard(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK, move);
                    switch (move.cap_type)
                    {
                        case PieceType.King:
                            cb.WK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Queen:
                            cb.WQ &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Rook:
                            cb.WR &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Knight:
                            cb.WK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Bishop:
                            cb.WB &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Pawn:
                            cb.WP &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        default:
                            break;
                    }
                    switch (move.moved_type)
                    {
                        case PieceType.King:
                            cb.BK |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.BK &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Queen:
                            cb.BQ |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.BQ &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Rook:
                            cb.BR |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.BR &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Knight:
                            cb.BN |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.BN &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Bishop:
                            cb.BB |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.BB &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Pawn:
                            cb.BP |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.BP &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        default:
                            break;

                    }
                    // Switch case for special events! promotion, enpassant etc.

                    theList.Add(cb);
                }
            }
            else
            {
                MoveGenerator.setCurrentBitboards(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK);
                ArrayList moves = MoveGenerator.PossibleMovesPlayer();
                foreach (Move move in moves)
                {
                    ChessBoard cb = new ChessBoard(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK, move);
                    switch (move.cap_type)
                    {
                        case PieceType.King:
                            cb.BK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Queen:
                            cb.BQ &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Rook:
                            cb.BR &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Knight:
                            cb.BK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Bishop:
                            cb.BB &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        case PieceType.Pawn:
                            cb.BP &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
                            break;
                        default:
                            break;
                    }
                    switch (move.moved_type)
                    {
                        case PieceType.King:
                            cb.WK |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.WK &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Queen:
                            cb.WQ |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.WQ &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Rook:
                            cb.WR |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.WR &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Knight:
                            cb.WN |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.WN &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Bishop:
                            cb.WB |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.WB &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        case PieceType.Pawn:
                            cb.WP |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
                            cb.WP &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
                            break;
                        default:
                            break;

                    }
                    // switch for special events
                    theList.Add(cb);
                }
            }
            return theList;
        }

        // TO DO
        public int check()
        {
            return 0;
        }

        public int AlphaBetaSearch(int alpha, int beta, int layer, bool min_max)
        {
            if (layer == 0 || check() != 0)
            {
                return evaluateBoard(min_max);
            }
            else if (min_max)
            {
                List<ChessBoard> chessboards = generateChessBoards(min_max);
                foreach (ChessBoard CB in chessboards)
                {
                    int result = CB.AlphaBetaSearch(alpha, beta, layer - 1, !min_max);
                    if (result > alpha)
                    {
                        alpha = result;
                        bestState = CB;
                    }

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return alpha;
            }
            else
            {
                List<ChessBoard> chessboards = generateChessBoards(min_max);
                foreach (ChessBoard CB in chessboards)
                {
                    int result = CB.AlphaBetaSearch(alpha, beta, layer - 1, !min_max);
                    if (result < beta)
                    {
                        beta = result;
                        bestState = CB;
                    }

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return beta;
            }
        }
    }

    public enum Player
    {
        White,
        Black
    }
}

