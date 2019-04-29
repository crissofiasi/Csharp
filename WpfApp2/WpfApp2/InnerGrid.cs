using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace WpfApp2
{
    public class InnerGrid
    {
        public ObservableCollection<ObservableCollection<Cell>> Rows {get;set;}
        public int Size { get; set; }
        public InnerGrid(int size)
        {
            this.Size = size;
            Rows = new ObservableCollection<ObservableCollection<Cell>>();
            for(int i=0;i<this.Size;i++)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int j=0;j<this.Size;j++)
                {
                    Cell c = new Cell();
                    row.Add(c);
                
                }
                Rows.Add(row);
            }
            
        }
    }
}
