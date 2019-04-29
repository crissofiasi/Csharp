using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfApp2
{
    public class Board
    {
        public ObservableCollection<ObservableCollection<InnerGrid>> Rows { get; set; }
        public int Size { get; set; }
        public Board(int size)
        {
            this.Size = size;
            Rows = new ObservableCollection<ObservableCollection<InnerGrid>>();
            for (int i = 0; i < this.Size; i++)
            {
                ObservableCollection<InnerGrid> row = new ObservableCollection<InnerGrid>();
                for (int j = 0; j < this.Size; j++)
                {
                    InnerGrid c = new InnerGrid(this.Size);
                    row.Add(c);

                }
                Rows.Add(row);
            }

        }



    }
}
