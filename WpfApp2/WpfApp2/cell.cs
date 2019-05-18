using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfApp2
{
    public class Cell : INotifyPropertyChanged
    {
        private int? _MyPrivateValue;
        public int? CellValue {
            get
            {
                return _MyPrivateValue;
            }
             set
            {
                if (value == _MyPrivateValue)
                    return;
                _MyPrivateValue = value < 1 ? null:value ;
                propChanged(nameof(CellValue));
            }
        }

        public int? obs;
        public bool IsReadOnly { get; set; }
        public Cell()
        {
            this.CellValue = null;
            this.IsReadOnly = false;
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
