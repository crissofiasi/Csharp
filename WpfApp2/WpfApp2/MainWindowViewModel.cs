using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace WpfApp2
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private int _Width;
        public int Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
                propChanged(nameof(Width));
                propChanged(nameof(Left));
            }
        }
        private int _Height;
        public int Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
                propChanged(nameof(Height));
                propChanged(nameof(Top));

            }
        }
        public int Left
        {
            get { return (int)SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2; }
            set { }
        }
        public int Top
        {
            get { return (int)SystemParameters.PrimaryScreenHeight / 2 - this.Height / 2; }
            set { }
        }
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
            get
            {
                if (_DrowBrd == null)
                {
                    _DrowBrd = new RelayCommand(
                        param => this.SudokuBoard.generateRandomBoard()

                    );
                }
                return _DrowBrd; ;
            }

        }
        private ICommand _ValidatBrd;
        public ICommand ValidateBrd
        {
            get
            {
                if (_ValidatBrd == null)
                {
                    _ValidatBrd = new RelayCommand(
                        param => this.ValidateBoard()
                    );
                }
                return _ValidatBrd; ;
            }

        }

        private void ValidateBoard()
        {
            if (this.SudokuBoard.ValidateBoard())
                if (this.SudokuBoard.IsComplete())
                    MessageBox.Show("AiCastigat!!!!");
                else
                    MessageBox.Show("Corect pana aici! Succes in continuare!");
            else
                MessageBox.Show("Gresit!");

        }
        private ICommand _SaveBrd;
        public ICommand SaveBrd
        {
            get
            {
                if (_SaveBrd == null)
                {
                    _SaveBrd = new RelayCommand(
                        param => this.SudokuBoard.SaveBoard()

                    );
                }
                return _SaveBrd; ;
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
            switch (this.Size)
            {
                case 2:
                    this.Width = 324;
                    this.Height = 222;

                    break;
                case 3:
                    this.Width = 525;
                    this.Height = 428;

                    break;
                case 4:
                    this.Width = 808;
                    this.Height = 712;

                    break;
                default:
                    this.Width = 900;
                    this.Height = 900;
                    break;
            }
        }
        #region inotify
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        private void propChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
