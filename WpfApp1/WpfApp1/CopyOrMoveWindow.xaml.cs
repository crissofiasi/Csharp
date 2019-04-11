using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for CopyOrMoveWindow.xaml
    /// </summary>
    public partial class CopyOrMoveWindow : Window
    {
        private string source;
        private string destination;
        private string op;
        public CopyOrMoveWindow()
        {
            InitializeComponent();
        }

        public CopyOrMoveWindow(string operation ,string sourcePath,string destinationPath) : this()
        {
            this.op = operation;
            this.source = sourcePath;
            this.destination = destinationPath;
        }

        private void CopyOrMoveWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            txtDestination.Text = this.destination;
            string operation = op + " " + this.source + "  tp:";
            LblOperation.Content = operation;
            BtnOK.Content = op.ToUpperInvariant();

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.destination = txtDestination.Text.Trim();
            if(op.ToLower().Trim()=="copy")
            {
                if(!IO_Utils.CopyFileOrFolder(this.source, this.destination))
                {
                    MessageBox.Show("Eroare la coipere!");
                }
            }
            else
            {
                if (!IO_Utils.MoveFileOrFolder(this.source, this.destination))
                {
                    MessageBox.Show("Eroare la mutare!");
                }
            }

            this.Close();
        }
    }
}
