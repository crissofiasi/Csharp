using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;

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
            //saveBoard();

            loadBoard();
        }


        public void loadBoard()
        {
            DirectoryInfo GameDir =new DirectoryInfo( "..\\..\\Games\\" + this.Size.ToString().Trim());
           FileInfo[] files = GameDir.GetFiles();
            Random rand = new Random();

            int randomFileIndex = rand.Next(files.Length - 1);
            string filename = files[randomFileIndex].FullName.Trim();
            StringReader reader = new StringReader(filename);
            IEnumerable<string> lines = File.ReadLines(filename);
            int k = 0;
            for (int oi = 0; oi < this.Size; oi++)
                for (int oj = 0; oj < this.Size; oj++)
                    for (int ii = 0; ii < this.Size; ii++)
                        for (int ij = 0; ij < this.Size; ij++)
                        { 
                            Int32 v;
                            string strValue = lines.ElementAt<string>(k++);
                            Int32.TryParse(strValue,out v) ;
                            this.Rows[oi][oj].Rows[ii][ij].CellValue = v ;
                            if (v==0)
                                this.Rows[oi][oj].Rows[ii][ij].CellValue = null ;

                        }
            reader.Close();



        }

        public bool IsComplete()
        {
            for (int oi = 0; oi < this.Size; oi++)
                for (int oj = 0; oj < this.Size; oj++)
                    for (int ii = 0; ii < this.Size; ii++)
                        for (int ij = 0; ij < this.Size; ij++)
                            if (this.Rows[oi][oj].Rows[ii][ij].CellValue == null)
                                return false;
            return true;
        }

        public bool ValidateBoard()
        {
            for (int oi = 0; oi < this.Size; oi++)
                for (int oj = 0; oj < this.Size; oj++)
                    for (int ii = 0; ii < this.Size; ii++)
                        for (int ij = 0; ij < this.Size; ij++)
                            if (!ValidateCell(oi, oj, ii, ij))
                                return false;
            return true;
        }

        public void SaveBoard()
        {
            
            string dirName = "..\\..\\Games\\" + this.Size.ToString().Trim()+"\\";
            FileInfo file = new FileInfo(dirName + "a.txt");

            while (file.Exists)
            {
                string fileName = RandomName()+".txt";
                file = new FileInfo(dirName + fileName);
            }

            StreamWriter writer = new StreamWriter(file.FullName);

            for (int oi = 0; oi < this.Size; oi++)
                for (int oj = 0; oj < this.Size; oj++)
                    for (int ii = 0; ii < this.Size; ii++)
                        for (int ij = 0; ij < this.Size; ij++)
                            writer.WriteLine(this.Rows[oi][oj].Rows[ii][ij].CellValue.ToString().Trim().PadRight(2));
            writer.Close();
            MessageBox.Show("Saved");
        }

        private static string RandomName()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public bool ValidateCell(int oi, int oj, int ii, int ij)
        {
            if (this.Rows[oi][oj].Rows[ii][ij].CellValue == null)
                return true;
            if (this.Rows[oi][oj].Rows[ii][ij].CellValue > this.Size*this.Size)
                return false;


            bool condition = false;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    condition = this.Rows[oi][oj].Rows[ii][ij].CellValue == this.Rows[oi][oj].Rows[i][j].CellValue;
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

                    condition = this.Rows[oi][oj].Rows[ii][ij].CellValue == this.Rows[o_i][oj].Rows[i][ij].CellValue;
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

                    condition = this.Rows[oi][oj].Rows[ii][ij].CellValue == this.Rows[oi][o_j].Rows[ii][j].CellValue;
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
