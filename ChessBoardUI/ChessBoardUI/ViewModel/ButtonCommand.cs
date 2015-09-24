using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChessBoardUI.ViewModel
{
    class ButtonCommand : ViewModelBase
    {
        public RelayCommand PlaySetupCommand { get;  set; }

        public ButtonCommand()
        {
            PlaySetupCommand = new RelayCommand(Hallo);
        }

        private String level;
        public String Level
        {
            get { return this.level; }
            set { this.level = value; RaisePropertyChanged(() => this.level); }
        }

        private Player chose_player;

        public Player Player
        {
            get { return this.chose_player; }
            set { this.chose_player = value; RaisePropertyChanged(() => this.chose_player); }
        }

        private void Hallo()
        {
            
        }
    }
}
