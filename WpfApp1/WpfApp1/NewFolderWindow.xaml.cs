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
    /// Interaction logic for NewFolderWindow.xaml
    /// </summary>
    public partial class NewFolderWindow : Window
    {
        private string InitialName;
        private string Path;
        public NewFolderWindow()
        {
            InitializeComponent();
            this.InitialName = "";
        }

        public NewFolderWindow(string path, string name):this()
        {
            this.InitialName = name;
            this.Path = path;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {

            //MessageBox.Show("Folder " + this.Path+TxtFolderName.Text.Trim() + " will be created!");

            if (!IO_Utils.NewFolder(this.Path, TxtFolderName.Text.Trim()))
            {
                MessageBox.Show("eroare la crearea folderului " + this.Path + this.Name + " !!!");
            }

            this.Close();
        }

        private void NewFolder_Loaded(object sender, RoutedEventArgs e)
        {
            TxtFolderName.Text = this.InitialName;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
