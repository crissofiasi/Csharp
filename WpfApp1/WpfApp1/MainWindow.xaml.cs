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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            PopulateDriveComboBoxes();
        }

        private void ClearWindow()
        {
            foreach (TextBlock tb in GrdMain.Children.OfType<TextBlock>())
                tb.Text = "";
            foreach (TextBox txt in GrdMain.Children.OfType<TextBox>())
                txt.Text = "";

        }

        private void PopulateDriveComboBoxes()
        {
            foreach (DriveInfo di in DriveInfo.GetDrives())
                foreach (ComboBox cb in GrdMain.Children.OfType<ComboBox>())
                    cb.Items.Add(di);
        }

        private void CmbDrive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayDriveVolumeLabel(sender);
            DisplayPath(sender);

        }

        private void DisplayPath(object sender)
        {
            ComboBox cb = (ComboBox)sender;
            TextBlock tb = SelectPathTextBlock(sender, ((ComboBox)sender).Name);
            tb.Text = ((DriveInfo)cb.SelectedItem).Name;
        }

        private void DisplayDriveVolumeLabel(object sender)
        {
            TextBlock tb = SelectVolumeLabelTextBlock(sender, ((ComboBox)sender).Name);
            ComboBox cb = (ComboBox)sender;
            tb.Text = ((DriveInfo)cb.SelectedItem).VolumeLabel.ToString();
        }

        TextBlock SelectVolumeLabelTextBlock(object sender,string name)
        {
            string nm = name.Replace("Cmb", "").Replace("Drive", "");
            foreach (TextBlock tbl in ((Grid)(((Control)sender).Parent)).Children.OfType<TextBlock>())
                if (tbl.Name.Contains(nm) && tbl.Name.ToLower().Contains("name"))
                    return tbl;
            return null;
        }

        TextBlock SelectPathTextBlock(object sender, string name)
        {
            string nm = name.Replace("Cmb", "").Replace("Drive", "");
            foreach (TextBlock tbl in ((Grid)(((Control)sender).Parent)).Children.OfType<TextBlock>())
                if (tbl.Name.Contains(nm) && tbl.Name.ToLower().Contains("path"))
                    return tbl;
            return null;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }

        private void dg_GotFocus(object sender, RoutedEventArgs e)
        {
            TblComandPath.Text = SelectPathTextBlock(sender, ((DataGrid)sender).Name).Text;
        }
    }
}
