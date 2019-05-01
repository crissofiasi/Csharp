using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApp2
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private Board _board;

        public Board SudokuBoard
        {
            get { return _board; }
            set
            {
                if (_board == value)
                {
                    return;
                }
                _board = value;
                propChanged(nameof(SudokuBoard));
            }
        }

        private ICommand _DrowBrd;

        public ICommand DrowBrd
        {
            get {
                if (_DrowBrd == null)
                {
                    _DrowBrd = new RelayCommand(
                        param => this.SudokuBoard.generateRandomBoard()
                       
                    );
                }
                return _DrowBrd; ;
            }
            
        }


        private int _Size;

        public int Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                propChanged(nameof(Size));
                DrowNewBoard();
            }
        }


        public MainWindowViewModel()
        {
            this.Size = 3;
            DrowNewBoard();
        }

        public void DrowNewBoard()
        {
            SudokuBoard = new Board(this.Size);
        }
        #region inotify

        public event PropertyChangedEventHandler PropertyChanged = (sender,e)=> { };
        private void propChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
