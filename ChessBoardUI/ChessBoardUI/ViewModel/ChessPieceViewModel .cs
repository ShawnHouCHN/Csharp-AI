using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using GalaSoft.MvvmLight.Messaging;

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
        private bool _Ownership;
        private RelayCommand _PieceClickCommand;
        private RelayCommand _PieceMoveCommand;
        private bool _chose = false;
        private int priv_coor_x;
        private int priv_coor_y;


        public int Coor_X
        {
            get { return ((int)this._Pos.X / Constants.Constants.CELL_EDGE_LENGTH); }
        }

        public int Coor_Y
        {
            get { return ((int)this._Pos.Y / Constants.Constants.CELL_EDGE_LENGTH); }
        }

        public Point Pos
        {
            get { return this._Pos; }
            set { value.X = value.X * Constants.Constants.CELL_EDGE_LENGTH; value.Y=value.Y* Constants.Constants.CELL_EDGE_LENGTH; this._Pos = value;  RaisePropertyChanged(() => this.Pos);

                //Console.Write("inital pos value is {0} , {1}", this._Pos.X, this._Pos.Y);
            }
        }

        public double Pos_X
        {
            get { return this._Pos.X; }
            set { this._Pos.X = value* Constants.Constants.CELL_EDGE_LENGTH; RaisePropertyChanged(() => this.Pos); }
        }

        public double Pos_Y
        {
            get { return this._Pos.Y; }
            set { this._Pos.Y = value * Constants.Constants.CELL_EDGE_LENGTH; RaisePropertyChanged(() => this.Pos); }
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

        public bool Ownership
        {
            get { return this._Ownership; }
            set { this._Ownership = value; RaisePropertyChanged(() => this.Ownership); }
        }

        public bool Chose
        {
            get { return this._chose; }
            set {this._chose = value; RaisePropertyChanged(() => this.Chose); }
        }

        public RelayCommand PieceClickCommand
        {
            get { return this._PieceClickCommand; }
            set { this._PieceClickCommand = new RelayCommand(this.selectPiece) ;}
        }

        public RelayCommand PieceMoveCommand
        {
            get { return this._PieceMoveCommand; }
            set { this._PieceMoveCommand = new RelayCommand(this.dragPiece); }
        }


        public void selectPiece()
        {
            if (this._Ownership)
            {
                if (!this.Chose)
                {
                    this.Chose = true;
                    this.Pos_X = ((int)Mouse.GetPosition(null).X - Constants.Constants.CANVAS_MARGIN_LEFT) / Constants.Constants.CELL_EDGE_LENGTH;
                    this.Pos_Y = ((int)Mouse.GetPosition(null).Y - Constants.Constants.CANVAS_MARGIN_TOP) / Constants.Constants.CELL_EDGE_LENGTH;
                    //Console.Write("current pos value is {0} , {1}", this.Pos_X, this.Pos.Y);
                    //Console.Write("current coor position is {0} , {1}", this.Coor_X, this.Coor_Y);
                    //Console.Write("current mouse value is {0} , {1}", Mouse.GetPosition(null).X-Constants.Constants.CANVAS_MARGIN_LEFT, Mouse.GetPosition(null).Y - Constants.Constants.CANVAS_MARGIN_TOP);
                    this.priv_coor_x = this.Coor_X;
                    this.priv_coor_y = this.Coor_Y;
                }
                else
                {
                    this.Pos_X = ((int)Mouse.GetPosition(null).X - Constants.Constants.CANVAS_MARGIN_LEFT) / Constants.Constants.CELL_EDGE_LENGTH;
                    this.Pos_Y = ((int)Mouse.GetPosition(null).Y - Constants.Constants.CANVAS_MARGIN_TOP) / Constants.Constants.CELL_EDGE_LENGTH;
                    this.Chose = false;

                    Console.WriteLine("new COOR value is {0} , {1}", this.Coor_X, this.Coor_Y);

                    //if(this.priv_coor_x!=this.Coor_X || this.priv_coor_y!=this.Coor_Y ) check whether use has moved a piece to a new place or not. 
                    Messenger.Default.Send(new HumanMoveMessage { FromPoint = new Point(this.priv_coor_x, this.priv_coor_y), ToPoint = new Point(this.Coor_X, this.Coor_Y) });

                }

            }
        }

        public void dragPiece()
        {
            
           
            double real_x = Mouse.GetPosition(null).X - Constants.Constants.CANVAS_MARGIN_LEFT;
            double real_y = Mouse.GetPosition(null).Y - Constants.Constants.CANVAS_MARGIN_TOP;


            if (this.Chose&&UIHelper.RestrictImageMove(real_x, real_y))
            {
                this.Pos_X = (Mouse.GetPosition(null).X - Constants.Constants.CANVAS_MARGIN_LEFT - Constants.Constants.IMAGE_CENTER_TO_LEFTTOP) / Constants.Constants.CELL_EDGE_LENGTH;
                this.Pos_Y = (Mouse.GetPosition(null).Y - Constants.Constants.CANVAS_MARGIN_TOP - Constants.Constants.IMAGE_CENTER_TO_LEFTTOP) / Constants.Constants.CELL_EDGE_LENGTH;
                RaisePropertyChanged(() => this.Pos);
            }
        }

    }
}
