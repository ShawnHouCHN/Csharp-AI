using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessBoardUI.ViewModel
{
    class SPCapturedViewModel:ViewModelBase
    {
        private ObservableCollection<Image> _cap_piece_collection;
        public ObservableCollection<Image> CapturedPiecesCollection
        {
            get
            {
                return _cap_piece_collection;
            }
            set
            {
                _cap_piece_collection = value;
                RaisePropertyChanged(() => this.CapturedPiecesCollection);
            }
        }
    }
}
