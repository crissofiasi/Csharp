using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{


    public class Board : INotifyPropertyChanged
    {
        ObservableCollection<ObservableCollection<InnerGrid>> Rows;
        public ObservableCollection<ObservableCollection<InnerGrid>> GridRows
        {
            get
            {
                return Rows;
            }
            set { }
        }
        bool isValidValue = true;
        public bool IsValid
        {
            get
            {
                return isValidValue;
            }
            set { }
        }

        public Board(int size)
        {
            Rows = new ObservableCollection<ObservableCollection<InnerGrid>>();
            for (int i = 0; i < size; i++)
            {
                ObservableCollection<InnerGrid> Col = new ObservableCollection<InnerGrid>();
                for (int j = 0; j < size; j++)
                {
                    InnerGrid c = new InnerGrid(size);
                    c.PropertyChanged += new PropertyChangedEventHandler(c_PropertyChanged);
                    Col.Add(c);
                }
                Rows.Add(Col);
            }
        }


        public Board()
        {
            int size=3;
            Rows = new ObservableCollection<ObservableCollection<InnerGrid>>();
            for (int i = 0; i < size; i++)
            {
                ObservableCollection<InnerGrid> Col = new ObservableCollection<InnerGrid>();
                for (int j = 0; j < size; j++)
                {
                    InnerGrid c = new InnerGrid(size);
                    c.PropertyChanged += new PropertyChangedEventHandler(c_PropertyChanged);
                    Col.Add(c);
                }
                Rows.Add(Col);
            }
        }

        void c_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                // bool valid = CheckIsValid();

                //foreach (ObservableCollection<InnerGrid> r in Rows)
                //{
                //    foreach (InnerGrid c in r)
                //    {
                //        c.IsValid = valid;
                //    }
                //}

                //isValidValue = valid;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsValid"));
            }

        }
        //private bool CheckIsValid()
        //{
        //    bool[] used = new bool[Rows.Count * Rows.Count];
        //    foreach (ObservableCollection<Cell> r in Rows)
        //    {
        //        foreach (Cell c in r)
        //        {
        //            if (c.Value.HasValue)
        //            {
        //                if (used[c.Value.Value - 1])
        //                {
        //                    return false; //this is a duplicate
        //                }
        //                else
        //                {
        //                    used[c.Value.Value - 1] = true;
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}
        public InnerGrid this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Rows.Count)
                    throw new ArgumentOutOfRangeException("row", row, "Invalid Row Index");
                if (col < 0 || col >= Rows.Count)
                    throw new ArgumentOutOfRangeException("col", col, "Invalid Column Index");
                return Rows[row][col];
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}




