using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;


namespace ChessBoardUI.ViewModel
{
    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    public enum Player
    {
        White,
        Black
    }

    class ChessPiece : ViewModelBase 
    {
        private Point _Pos;
        private PieceType _Type;
        private Player _Player;

        public ChessPiece(){
         
            }

        public Point Pos
        {
            get { return this._Pos; }
            set { value.X = value.X * Constants.CELL_EDGE_LENGTH; value.Y=value.Y* Constants.CELL_EDGE_LENGTH; this._Pos = value; RaisePropertyChanged(() => this.Pos); }
        }

        public PieceType Type
        {
            get { return this._Type; }
            set { this._Type = value; RaisePropertyChanged(() => this.Type); }  //RaisePropertyChanged Called from a property setter to notify the framework that an Entity member has changed.
        }

        public Player Player
        {
            get { return this._Player; }
            set { this._Player = value; RaisePropertyChanged(() => this.Player); }
        }



    }
}
