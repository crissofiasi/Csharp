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
        private int? _value;
        public int? Value {
            get
            {
                return _value;
            }
             set
            {
                if (value == _value)
                    return;
                _value = value;
                propChanged(nameof(Value));
            }
        }

        public int? obs;
        public bool IsReadOnly { get; set; }
        public Cell()
        {
            this.Value = 0;
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
