using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using ChessBoardUI.ViewTreeHelper;
using ChessBoardUI.Constants;

namespace ChessBoardUI.ViewModel
{
    class HumanMoveMessage
    {
        private Point from_point;
        private Point to_point;
        private PieceType type;
        private bool castling;
        private bool an_passent;
        private bool promote;
        private bool turn;

        public bool Turn
        {
            get { return promote; }
            set { promote = value; }
        }

        public bool Promotion
        {
            get { return promote; }
            set { promote = value; }
        }

        public bool Castling
        {
            get { return castling; }
            set { castling = value; }
        }

        public bool AnPassent
        {
            get { return an_passent; }
            set { an_passent = value; }
        }

        public Point FromPoint
        {
            get { return from_point; }
            set { from_point = value; }
        }

        public Point ToPoint
        {
            get { return to_point; }
            set { to_point = value; }
        }

        public PieceType Type
        {
            get { return type; }
            set { type = value; }
        }

        }

    class MachineMoveMessage
        {
            private bool turn;


            public bool Turn
            {
                get { return turn; }
                set { turn = value; }
            }

        }
    }




