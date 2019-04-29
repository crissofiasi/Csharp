using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Cell : INotifyPropertyChanged
    {
        bool readOnlyValue = false;
        public bool ReadOnly
        {
            get
            {
                return readOnlyValue;
            }
            set
            {
                if (readOnlyValue != value)
                {
                    readOnlyValue = value;
                    if (PropertyChanged != null) PropertyChanged(this,
                        new PropertyChangedEventArgs("ReadOnly"));
                }
            }
        }
        int? valueValue = null;
        public int? Value
        {
            get
            {
                return valueValue;
            }
            set
            {
                if (valueValue != value)
                {
                    valueValue = value;
                    if (PropertyChanged != null) PropertyChanged(this,
                        new PropertyChangedEventArgs("Value"));
                }
            }
        }
        bool isValidValue = true;
        public Cell()
        {
            this.valueValue = 9;
        }
        public bool IsValid
        {
            get
            {
                return isValidValue;
            }
            set
            {
                if (isValidValue != value)
                {
                    isValidValue = value;
                    if (PropertyChanged != null) PropertyChanged(this,
                        new PropertyChangedEventArgs("IsValid"));
                }
            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
