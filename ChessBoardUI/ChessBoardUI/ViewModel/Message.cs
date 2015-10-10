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
    class MoveMessage
    {
        private Point from_point;
        private Point to_point;

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
    }

    
}
