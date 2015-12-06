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

        public ulong WP , WN , WB , WQ, WR , WK ,
                      BP , BN , BB, BQ , BR , BK ;

        public ulong occupied, empty, whitePieces, enemyOrEmpty, blackPieces;

        public Move move; // the move taken to this board state 
       
        public ChessBoard bestState=null;
        public bool MKC = true;
        public bool MQC = true;
        public bool PKC = true;
        public bool PQC = true;
        public bool PC_DONE = false;
        public bool MC_DONE = false;

        public int eva;

        public ChessBoard(ulong WP, ulong WN, ulong WB, ulong WQ, ulong WR, ulong WK, ulong BP, ulong BN, ulong BB, ulong BQ, ulong BR, ulong BK, Move move , bool MKC, bool MQC, bool PKC, bool PQC, bool PC_DONE, bool MC_DONE)
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
            this.MKC = MKC;
            this.MQC = MQC;
            this.PKC = PKC;
            this.PQC = PQC;
            this.PC_DONE = PC_DONE;
            this.MC_DONE = MC_DONE;

            createUsefullBitboards();
        }



        private void createUsefullBitboards()
        {
            occupied = (WP | BP | WR | BR | WN | BN | WB | BB | WQ | BQ | WK | BK);
            empty = ~occupied;
            whitePieces = (WP | WR | WN | WB | WQ | WK);
            blackPieces = (BP | BR | BN | BB | BQ | BK);
            enemyOrEmpty = (empty | whitePieces);
        }



        
        public int evaluateBoard(bool min_max, ChessBoard leaf_chessboard)
        {
            //MoveGenerator.states += 1;
            int Machine_Points = 0;
            int Player_Points = 0;

            // Evaluate pieces under threat
            ulong[] bitboards = MoveGenerator.getCurrentBitboards();
            MoveGenerator.setCurrentBitboards(leaf_chessboard.BP, leaf_chessboard.BR, leaf_chessboard.BN, leaf_chessboard.BB, leaf_chessboard.BQ, leaf_chessboard.BK, leaf_chessboard.WP, leaf_chessboard.WR, leaf_chessboard.WN, leaf_chessboard.WB, leaf_chessboard.WQ, leaf_chessboard.WK);

            ArrayList moves;

            if (leaf_chessboard.PC_DONE)
            {
                Player_Points += 100;
            }
            if (leaf_chessboard.MC_DONE)
            { 
                Machine_Points += 100;
            }


            if (min_max)
            {
                moves = MoveGenerator.PossibleMovesMachine();
            }
            else
            {
                moves = MoveGenerator.PossibleMovesPlayer();
            }

            moves.Sort(new MoveCompare()); // sort the list so it get captured move first;
            Nullable<PieceType> piece_cap = null;
            foreach (Move leaf_move in moves)
            {
                if (leaf_move.cap_type != piece_cap)
                {
                    if (min_max) //if it is a max leaf node, add value to machine's value;
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
                            Player_Points += (Constants.Constants.BISHOP_WEIGHT+ Constants.Constants.BishopTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.WK >> i) & 1) == 1)
                        {
                            //Evaluation of White King
                            Player_Points += (Constants.Constants.KING_WEIGHT+ Constants.Constants.KingTableO[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.WN >> i) & 1) == 1)
                        {
                            //Evaluation of White Knight 
                            Player_Points += (Constants.Constants.KNIGHT_WEIGHT+ Constants.Constants.KnightTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.WP >> i) & 1) == 1)
                        {
                            //Evaluation of White Pawns 
                            Player_Points += (Constants.Constants.PAWN_WEIGHT+ Constants.Constants.PawnTable[(8 * (7 - i / 8) + i % 8)]);

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
                            Player_Points += (Constants.Constants.QUEEN_WEIGHT+ Constants.Constants.QueenTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.WR >> i) & 1) == 1)
                        {
                            //Evaluation of White Rook 
                            Player_Points += (Constants.Constants.ROOK_WEIGHT+ Constants.Constants.RookTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.BB >> i) & 1) == 1)
                        {
                            //Evaluation of Black Bishop 
                            Machine_Points += (Constants.Constants.BISHOP_WEIGHT+ Constants.Constants.BishopTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.BK >> i) & 1) == 1)
                        {
                            //Evaluation of Black King 
                            Machine_Points += (Constants.Constants.KING_WEIGHT + Constants.Constants.KingTableO[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.BN >> i) & 1) == 1)
                        {
                            //Evaluation of Black Knight 
                            Machine_Points += (Constants.Constants.KNIGHT_WEIGHT + Constants.Constants.KnightTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.BP >> i) & 1) == 1)
                        {
                            //Evaluation of Black Pawns 
                            Machine_Points += (Constants.Constants.PAWN_WEIGHT+ Constants.Constants.PawnTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);

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
                            Machine_Points += (Constants.Constants.QUEEN_WEIGHT+ Constants.Constants.QueenTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.BR >> i) & 1) == 1)
                        {
                            //Evaluation of Black Rook 
                            Machine_Points += (Constants.Constants.ROOK_WEIGHT + Constants.Constants.RookTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                    }


                    else //player plays black
                    {
                        if (((leaf_chessboard.WB >> i) & 1) == 1)
                        {
                            //Evaluation of White Bishop
                            Machine_Points += (Constants.Constants.BISHOP_WEIGHT+ Constants.Constants.BishopTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.WK >> i) & 1) == 1)
                        {
                            //Evaluation of White King
                            Machine_Points += (Constants.Constants.KING_WEIGHT+ Constants.Constants.KingTableO[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.WN >> i) & 1) == 1)
                        {
                            //Evaluation of White Knight 
                            Machine_Points += (Constants.Constants.KNIGHT_WEIGHT+ Constants.Constants.KnightTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.WP >> i) & 1) == 1)
                        {
                            //Evaluation of White Pawns 
                            Machine_Points += (Constants.Constants.PAWN_WEIGHT+ Constants.Constants.PawnTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);

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
                            Machine_Points +=(Constants.Constants.QUEEN_WEIGHT+ Constants.Constants.QueenTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.WR >> i) & 1) == 1)
                        {
                            //Evaluation of White Rook 
                            Machine_Points += (Constants.Constants.ROOK_WEIGHT+ Constants.Constants.RookTable[Constants.Constants.Mirror64[(8 * (7 - i / 8) + i % 8)]]);
                        }
                        else if (((leaf_chessboard.BB >> i) & 1) == 1)
                        {
                            //Evaluation of Black Bishop 
                            Player_Points += (Constants.Constants.BISHOP_WEIGHT+ Constants.Constants.BishopTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.BK >> i) & 1) == 1)
                        {
                            //Evaluation of Black King 
                            Player_Points += (Constants.Constants.KING_WEIGHT+ Constants.Constants.KingTableO[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.BN >> i) & 1) == 1)
                        {
                            //Evaluation of Black Knight 
                            Player_Points += (Constants.Constants.KNIGHT_WEIGHT+ Constants.Constants.KnightTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.BP >> i) & 1) == 1)
                        {
                            //Evaluation of Black Pawns 
                            Player_Points += (Constants.Constants.PAWN_WEIGHT+ Constants.Constants.PawnTable[(8 * (7 - i / 8) + i % 8)]);

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
                            Player_Points +=(Constants.Constants.QUEEN_WEIGHT+ Constants.Constants.QueenTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                        else if (((leaf_chessboard.BR >> i) & 1) == 1)
                        {
                            //Evaluation of Black Rook 
                            Player_Points += (Constants.Constants.ROOK_WEIGHT+ Constants.Constants.RookTable[(8 * (7 - i / 8) + i % 8)]);
                        }
                    }
                }

            }
            MoveGenerator.setCurrentBitboards(bitboards[0], bitboards[1], bitboards[2], bitboards[3], bitboards[4], bitboards[5], bitboards[6], bitboards[7], bitboards[8], bitboards[9], bitboards[10], bitboards[11]);
            return Machine_Points - Player_Points;  //(machine point - player point)
        }
         
        public int AlphaBetaSearch(int alpha, int beta, int layer, bool min_max)
        {
            if (move == null)
            {
                move = new Move(0, 0, 0, 0);
            }

            if (layer == 0)
            {             
                return evaluateBoard(min_max, this);
            }
            else if (min_max)  //max node
            {

                List<ChessBoard> chessboards = MoveGenerator.generateChessBoards(min_max, BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK, this.move, this.MKC, this.MQC, this.PKC, this.PQC, this.PC_DONE, this.MC_DONE);
                if (chessboards.Count == 0)
                {
                    if (MoveGenerator.isKingInCheck(min_max))
                    {
                        //Console.WriteLine("Game over machine lost");
                        bestState = null;
                        return int.MinValue+1;
                    }
                    else
                    {
                        //Console.WriteLine("Game over draw (max) 1");
                        bestState = null;
                        return 0;
                    }
                }


                foreach (ChessBoard CB in chessboards)
                {
                    if (DateTime.Compare(DateTime.Now, MoveGenerator.end_time) > 0)
                    {
                        return int.MaxValue-1;
                    }
                    if (CB.move.cap_type == PieceType.King)
                    {
                        bestState = CB;
                        return int.MaxValue-1;  //just change it to min, it was max
                    }

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
                if (MoveGenerator.isKingInCheck(min_max) && alpha == int.MinValue)
                {
                    Console.WriteLine("Game over machine lost");
                    bestState = null;
                    return int.MinValue+1;
                }
                else if(!MoveGenerator.isKingInCheck(min_max) && alpha == int.MinValue)
                {
                    Console.WriteLine("Game over draw (max) 2");
                    bestState = null;
                    return 0;
                }

                return alpha;
            }
            else
            {
                List<ChessBoard> chessboards = MoveGenerator.generateChessBoards(min_max, BP, BR, BN, BB, BQ, BK, WP, WR, WN, WB, WQ, WK, this.move, this.MKC, this.MQC, this.PKC, this.PQC, this.PC_DONE, this.MC_DONE);
                if (chessboards.Count == 0)
                {
                    if (MoveGenerator.isKingInCheck(min_max))
                    {
                        //Console.WriteLine("Game over player lost");
                        bestState = null;
                        return int.MaxValue-1;
                    }
                    else
                    {
                        //Console.WriteLine("Game over draw (min) 1");
                        bestState = null;
                        return 0;
                    }
                }
               
                foreach (ChessBoard CB in chessboards)
                {
                    if (DateTime.Compare(DateTime.Now, MoveGenerator.end_time) > 0)
                    {
                        return int.MinValue;
                    }

                    if (CB.move.cap_type == PieceType.King)
                    {
                        bestState = CB;
                        return int.MinValue+1;
                    }
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

                if (MoveGenerator.isKingInCheck(min_max) && beta == int.MaxValue)
                {
                    //Console.WriteLine("Game over player lost");
                    bestState = null;
                    return int.MaxValue-1;
                }
                else if (!MoveGenerator.isKingInCheck(min_max) && beta == int.MaxValue)
                {
                    //Console.WriteLine("Game over draw (min) 2");
                    bestState = null;
                    return 0;
                }

                return beta;
            }
        }
    }
}
