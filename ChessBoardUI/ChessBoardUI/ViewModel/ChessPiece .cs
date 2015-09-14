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

        public Point Pos
        {
            get { return this._Pos; }
            set { this._Pos = value; RaisePropertyChanged(() => this.Pos); }
        }

        public PieceType Type
        {
            get { return this._Type; }
            set { this._Type = value; RaisePropertyChanged(() => this.Type); }
        }

        public Player Player
        {
            get { return this._Player; }
            set { this._Player = value; RaisePropertyChanged(() => this.Player); }
        }



    }
}
