using ChessBoardUI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardUI.AIAlgorithm
{
    public class ChessBoard
    {
        //public ulong WP = 0x000000000000ff00, WN = 0x0000000000000042, WB = 0x0000000000000024, WQ = 0x0000000000000008, WR = 0x0000000000000081, WK = 0x0000000000000010,
        //              BP = 0x00ff000000000000, BN = 0x4200000000000000, BB = 0x2400000000000000, BQ = 0x0800000000000000, BR = 0x8100000000000000, BK = 0x1000000000000000;

        public ulong WP , WN , WB , WQ, WR , WK ,
                      BP , BN , BB, BQ , BR , BK ;




        public ulong occupied, empty, whitePieces, enemyOrEmpty;

        public Move move; // the move taken to this board state 
       
        public ChessBoard bestState;

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


        public ChessBoard(ulong WP, ulong WN, ulong WB, ulong WQ, ulong WR, ulong WK, ulong BP, ulong BN, ulong BB, ulong BQ, ulong BR, ulong BK, Move move=null)
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
            this.move = move;
            //this.player = currentPlayer;

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
        public int evaluateBoard(bool min_max, ChessBoard leaf_chessboard)
        {
            int Machine_Points = 0;
            int Player_Points = 0;

            // Evaluate pieces under threat
            MoveGenerator.setCurrentBitboards(leaf_chessboard.BP, leaf_chessboard.BR, leaf_chessboard.BN, leaf_chessboard.BB, leaf_chessboard.BQ, leaf_chessboard.BK, leaf_chessboard.WP, leaf_chessboard.WR, leaf_chessboard.WN, leaf_chessboard.WB, leaf_chessboard.WQ, leaf_chessboard.WK);
            ArrayList moves;
            if (min_max)
            {
                moves = MoveGenerator.PossibleMovesMachine();
            }
            else
            {
                moves = MoveGenerator.PossibleMovesPlayer();
            }

            moves.Sort(new MoveCompare()); // sort the list so it get captured move first;
            //differentiate between white and black

            foreach (Move leaf_move in moves)
            {
                if (leaf_move.cap_type != null)
                {
                    if (min_max) //if it is a max leaf node, add vallue to machine's value;
                        Machine_Points += 7;
                    else
                        Player_Points += 7;
                }
                else
                {
                    break;
                }
            }


            for (int i = 0; i < 64; i++)
            {
                // Evaluate positions of each piece on the board
                if (((leaf_chessboard.occupied >> i) & 1) == 1)
                {
                    if (MoveGenerator.player_color) //if player plays white
                    {
                        if (((leaf_chessboard.WB >> i) & 1) == 1)
                        {
                            //Evaluation of White Bishop
                            Player_Points += (Constants.Constants.BISHOP_WEIGHT+ BishopTable[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WK >> i) & 1) == 1)
                        {
                            //Evaluation of White King
                            Player_Points += (Constants.Constants.KING_WEIGHT+ KingTableO[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WN >> i) & 1) == 1)
                        {
                            //Evaluation of White Knight 
                            Player_Points += (Constants.Constants.KNIGHT_WEIGHT+ KnightTable[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WP >> i) & 1) == 1)
                        {
                            //Evaluation of White Pawns 
                            Player_Points += (Constants.Constants.PAWN_WEIGHT+ PawnTable[(8 * (7 - i / 8) + i % 8)]);

                            //evaluate of dobble pawn weakness
                            for (int rank= (7- i/8); rank >=0 ; rank--)
                            if (((leaf_chessboard.WP >> (i+8*rank)) & 1) ==1)
                            {
                                Player_Points += -7;
                            }
                        }
                        else if (((leaf_chessboard.WQ >> i) & 1) == 1)
                        {
                            //Evaluation of White Queen 
                            Player_Points += (Constants.Constants.QUEEN_WEIGHT+ QueenTable[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WR >> i) & 1) == 1)
                        {
                            //Evaluation of White Rook 
                            Player_Points += (Constants.Constants.ROOK_WEIGHT+ RookTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.BB >> i) & 1) == 1)
                        {
                            //Evaluation of Black Bishop 
                            Machine_Points += (Constants.Constants.BISHOP_WEIGHT+ BishopTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BK >> i) & 1) == 1)
                        {
                            //Evaluation of Black King 
                            Machine_Points += (Constants.Constants.KING_WEIGHT + KingTableO[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BN >> i) & 1) == 1)
                        {
                            //Evaluation of Black Knight 
                            Machine_Points += (Constants.Constants.KNIGHT_WEIGHT + KnightTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BP >> i) & 1) == 1)
                        {
                            //Evaluation of Black Pawns 
                            Machine_Points += (Constants.Constants.PAWN_WEIGHT+ PawnTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                            //evaluate of dobble pawn weakness
                            for (int rank = (7 - i / 8); rank >= 0; rank--)
                                if (((leaf_chessboard.WP >> (i + 8 * rank)) & 1) == 1)
                                {
                                    Machine_Points += -7;
                                }
                        }
                        else if (((leaf_chessboard.BQ >> i) & 1) == 1)
                        {
                            //Evaluation of Black Queen 
                            Machine_Points += (Constants.Constants.QUEEN_WEIGHT+ QueenTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BR >> i) & 1) == 1)
                        {
                            //Evaluation of Black Rook 
                            Machine_Points += (Constants.Constants.ROOK_WEIGHT + RookTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                    }


                    else
                    {
                        if (((leaf_chessboard.WB >> i) & 1) == 1)
                        {
                            //Evaluation of White Bishop
                            Machine_Points += (Constants.Constants.BISHOP_WEIGHT+ BishopTable[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WK >> i) & 1) == 1)
                        {
                            //Evaluation of White King
                            Machine_Points += (Constants.Constants.KING_WEIGHT+ KingTableO[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WN >> i) & 1) == 1)
                        {
                            //Evaluation of White Knight 
                            Machine_Points += (Constants.Constants.KNIGHT_WEIGHT+ KnightTable[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WP >> i) & 1) == 1)
                        {
                            //Evaluation of White Pawns 
                            Machine_Points += (Constants.Constants.PAWN_WEIGHT+ PawnTable[(8 * (7 - i / 8) + i % 8)]);

                            //evaluate of dobble pawn weakness
                            for (int rank = (7 - i / 8); rank >= 0; rank--)
                                if (((leaf_chessboard.WP >> (i + 8 * rank)) & 1) == 1)
                                {
                                    Machine_Points += -7;
                                }
                        }
                        else if (((leaf_chessboard.WQ >> i) & 1) == 1)
                        {
                            //Evaluation of White Queen 
                            Machine_Points +=(Constants.Constants.QUEEN_WEIGHT+ QueenTable[(8 * (7 - i / 8) + i % 8)]);

                        }
                        else if (((leaf_chessboard.WR >> i) & 1) == 1)
                        {
                            //Evaluation of White Rook 
                            Machine_Points += (Constants.Constants.ROOK_WEIGHT+ RookTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.BB >> i) & 1) == 1)
                        {
                            //Evaluation of Black Bishop 
                            Player_Points += (Constants.Constants.BISHOP_WEIGHT+ BishopTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BK >> i) & 1) == 1)
                        {
                            //Evaluation of Black King 
                            Player_Points += (Constants.Constants.KING_WEIGHT+ KingTableO[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BN >> i) & 1) == 1)
                        {
                            //Evaluation of Black Knight 
                            Player_Points += (Constants.Constants.KNIGHT_WEIGHT+ KnightTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BP >> i) & 1) == 1)
                        {
                            //Evaluation of Black Pawns 
                            Player_Points += (Constants.Constants.PAWN_WEIGHT+ PawnTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                            //evaluate of dobble pawn weakness
                            for (int rank = (7 - i / 8); rank >= 0; rank--)
                                if (((leaf_chessboard.WP >> (i + 8 * rank)) & 1) == 1)
                                {
                                    Player_Points += -7;
                                }
                        }
                        else if (((leaf_chessboard.BQ >> i) & 1) == 1)
                        {
                            //Evaluation of Black Queen 
                            Player_Points +=(Constants.Constants.QUEEN_WEIGHT+ QueenTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);

                        }
                        else if (((leaf_chessboard.BR >> i) & 1) == 1)
                        {
                            //Evaluation of Black Rook 
                            Player_Points += (Constants.Constants.ROOK_WEIGHT+ RookTable[Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                    }
                }

            }
                //if (check() == 1) { blackPoints += 50; }
                return Machine_Points - Player_Points;  //(machine point - player point)
        }


        // TO DO
        //public List<ChessBoard> generateChessBoards(bool min_max)
        //{
        //    List<ChessBoard> theList = new List<ChessBoard>();
        //    if (min_max)
        //    {
        //        MoveGenerator.setCurrentBitboards(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK);
                

        //        ArrayList moves = MoveGenerator.PossibleMovesMachine();

        //        foreach (Move move in moves)
        //        {
        //            ChessBoard cb = new ChessBoard(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK, move);
        //            switch (move.cap_type)
        //            {
        //                case PieceType.King:
        //                    cb.WK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Queen:
        //                    cb.WQ &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Rook:
        //                    cb.WR &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Knight:
        //                    cb.WK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Bishop:
        //                    cb.WB &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Pawn:
        //                    cb.WP &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                default:
        //                    break;
        //            }
        //            switch (move.moved_type)
        //            {
        //                case PieceType.King:
        //                    cb.BK |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.BK &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Queen:
        //                    cb.BQ |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.BQ &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Rook:
        //                    cb.BR |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.BR &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Knight:
        //                    cb.BN |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.BN &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Bishop:
        //                    cb.BB |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.BB &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Pawn:
        //                    cb.BP |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.BP &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                default:
        //                    break;

        //            }
        //            // Switch case for special events! promotion, enpassant etc.

        //            theList.Add(cb);
        //        }
        //    }
        //    else
        //    {
        //        MoveGenerator.setCurrentBitboards(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK);
        //        ArrayList moves = MoveGenerator.PossibleMovesPlayer();
        //        foreach (Move move in moves)
        //        {
        //            ChessBoard cb = new ChessBoard(BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK, move);
        //            switch (move.cap_type)
        //            {
        //                case PieceType.King:
        //                    cb.BK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Queen:
        //                    cb.BQ &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Rook:
        //                    cb.BR &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Knight:
        //                    cb.BK &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Bishop:
        //                    cb.BB &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                case PieceType.Pawn:
        //                    cb.BP &= ~((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    break;
        //                default:
        //                    break;
        //            }
        //            switch (move.moved_type)
        //            {
        //                case PieceType.King:
        //                    cb.WK |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.WK &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Queen:
        //                    cb.WQ |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.WQ &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Rook:
        //                    cb.WR |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.WR &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Knight:
        //                    cb.WN |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.WN &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Bishop:
        //                    cb.WB |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.WB &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                case PieceType.Pawn:
        //                    cb.WP |= ((ulong)1 << (move.to_rank * 8 + move.to_file));
        //                    cb.WP &= ~((ulong)1 << (move.from_rank * 8 + move.from_file));
        //                    break;
        //                default:
        //                    break;

        //            }
        //            // switch for special events
        //            theList.Add(cb);
        //        }
        //    }
        //    return theList;
        //}

        // TO DO
        //public int check()
        //{
        //    throw new NotImplementedException();
        //}

        public int AlphaBetaSearch(int alpha, int beta, int layer, bool min_max)
        {
            if (layer == 0)
            {
                return evaluateBoard(min_max, this);
            }
            else if (min_max)
            {
                List<ChessBoard> chessboards = MoveGenerator.generateChessBoards(min_max, BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK);
                foreach (ChessBoard CB in chessboards)
                {
                    //Console.WriteLine("Chessboard item "+Convert.ToString((long)CB.occupied, 2));
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
                List<ChessBoard> chessboards = MoveGenerator.generateChessBoards(min_max, BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK);
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
}
