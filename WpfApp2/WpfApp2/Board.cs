using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

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

        public Board()
        {
            this.Size = 3;
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

        public void generateRandomBoard()
        {
            for (int oi = 0; oi < this.Size; oi++)
            {
                for (int oj = 0; oj < this.Size; oj++)
                {

                    for (int ii = 0; ii < this.Size; ii++)
                    {
                        for (int ij = 0; ij < this.Size; ij++)
                        {
                            this.Rows[oi][oj].Rows[ii][ij].Value = -1;

                        }
                    }
                }
            }

            for (int oi = 0; oi < this.Size; oi++)
            {
                for (int oj = 0; oj < this.Size; oj++)
                {

                    for (int ii = 0; ii < this.Size; ii++)
                    {
                        for (int ij = 0; ij < this.Size; ij++)
                        {
                            Random rnd = new Random();
                            int? v = 0;// rnd.Next(1, this.Size * this.Size);
                            int k = 0;
                            this.Rows[oi][oj].Rows[ii][ij].obs = null;
                            this.Rows[oi][oj].Rows[ii][ij].Value = v;
                            do
                            {
                                v = this.Rows[oi][oj].Rows[ii][ij].Value;
                                v++;
                                v = (v == this.Size * this.Size + 1) ? 1 : v;
                                if ((++k > this.Size * this.Size) || (v == this.Rows[oi][oj].Rows[ii][ij].obs))
                                {

                                    this.Rows[oi][oj].Rows[ii][ij].Value = -1;
                                    k = 1;
                                    ij--;
                                    if (ij < 0) { ij = this.Size - 1; ii--; }
                                    if (ii < 0) { ii = this.Size - 1; oj--; }
                                    if (oj < 0) { oj = this.Size - 1; oi--; }
                                    if (oi < 0) { MessageBox.Show("WTF??"); return; }
                                    if (this.Rows[oi][oj].Rows[ii][ij].obs == null)
                                         this.Rows[oi][oj].Rows[ii][ij].obs = this.Rows[oi][oj].Rows[ii][ij].Value;
                                    v = this.Rows[oi][oj].Rows[ii][ij].Value;
                                    v++;
                                    v = (v == this.Size * this.Size + 1) ? 1 : v;

                                }


                                this.Rows[oi][oj].Rows[ii][ij].Value = v;
                                
                            
                            } while (!ValidateCell(oi, oj, ii, ij));

                        }
                    }
                }
            }
        }

        public bool ValidateCell(int oi, int oj, int ii, int ij)
        {
            if (this.Rows[oi][oj].Rows[ii][ij].Value == null)
                return true;

            bool condition = false;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    condition = this.Rows[oi][oj].Rows[ii][ij].Value == this.Rows[oi][oj].Rows[i][j].Value;
                    condition &= ((ii != i) || (ij != j));
                    if (condition)
                    {
                        return false;
                    }
                }
            }

            for (int o_i = 0; o_i < Size; o_i++)
            {
                for (int i = 0; i < Size; i++)
                {

                    condition = this.Rows[oi][oj].Rows[ii][ij].Value == this.Rows[o_i][oj].Rows[i][ij].Value;
                    condition &= ((ii != i) || (oi != o_i));
                    if (condition)
                    {
                        return false;
                    }
                }
            }

            for (int o_j = 0; o_j < Size; o_j++)
            {
                for (int j = 0; j < Size; j++)
                {

                    condition = this.Rows[oi][oj].Rows[ii][ij].Value == this.Rows[oi][o_j].Rows[ii][j].Value;
                    condition &= ((ij != j) || (oj != o_j));
                    if (condition)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


    }
}
