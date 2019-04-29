using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Cell
    {
        public int Value { get; set; }
        public bool IsReadOnly { get; set; }
        public Cell()
        {
            this.Value = 9;
            this.IsReadOnly = false;
        }
    }
}
