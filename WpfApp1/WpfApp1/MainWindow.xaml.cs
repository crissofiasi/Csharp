using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        public class DataLists
        {
            public List<SubItem> listLeft;
            public List<SubItem> listRight;
        }

        private DataLists dataLists = new DataLists();
        private string SelectedSide;
        public List<SubItem> GetFileList(string DirectoryPath)
        {
            List<SubItem> list = new List<SubItem>();
            DirectoryInfo currentDir = new DirectoryInfo(DirectoryPath);
            if (!currentDir.Exists)
            {
                return null;
            }

            try
            {
                currentDir.GetDirectories();
            }
            catch
            {
                return null;
            }


            if (currentDir.Parent != null)
            {
                SubItem first = new SubItem(currentDir.Parent, "..");

                list.Add(first);

            }
            foreach (DirectoryInfo dir in currentDir.GetDirectories())
            {
                list.Add(new SubItem(dir));
            }

            foreach (FileInfo file in currentDir.GetFiles())
            {
                list.Add(new SubItem(file));
            }

            return list;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            PopulateDriveComboBoxes();

            dgLeft.ItemsSource = dataLists.listLeft;
            dgRight.ItemsSource = dataLists.listRight;


            PopulateDataGrid(CmbLeftDrive);
            PopulateDataGrid(CmbRightDrive);

        }

        private void ClearWindow()
        {
            foreach (TextBlock tb in GrdMain.Children.OfType<TextBlock>())
            {
                tb.Text = "";
            }

            foreach (TextBox txt in GrdMain.Children.OfType<TextBox>())
            {
                txt.Text = "";
            }
        }

        private void PopulateDriveComboBoxes()
        {

            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                foreach (ComboBox cb in GrdMain.Children.OfType<ComboBox>())
                {
                    cb.Items.Add(di);
                }
            }

            foreach (ComboBox cb in GrdMain.Children.OfType<ComboBox>())
            {
                foreach (object cbi in cb.Items)
                {
                    if (((DriveInfo)cbi).IsReady)
                    {
                        cb.SelectedItem = cbi;
                        break;
                    }
                }
            }
        }

        private void CmbDrive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (!((DriveInfo)cb.SelectedItem).IsReady)
            {
                MessageBox.Show("Drive Not Redy! Please chose onother");
                cb.SelectedItem = e.RemovedItems[0];
                return;

            }

            DisplayDriveVolumeLabel(sender);
            // DisplayPath(sender);
            PopulateDataGrid(sender);
        }







        private void PopulateDataGrid(object sender)
        {
            ComboBox cb = (ComboBox)sender;
            DataGrid dg = SelectFilelistDataGrid(sender, ((ComboBox)sender).Name);
            dg.ItemsSource = GetFileList(((DriveInfo)cb.SelectedItem).RootDirectory.FullName);
            FocusOnDatagridCelll(dg, 0, 0);
        }

        private static void FocusOnDatagridCelll(DataGrid dg, int row, int column)
        {
            dg.Focus();
            dg.SelectedIndex = row;
            if ((dg.Items.Count > row) && (dg.Columns.Count > column))
            {
                dg.CurrentCell = new DataGridCellInfo(dg.Items[row], dg.Columns[column]);

            }
        }

        private void PopulateDataGrid(object sender, string path)
        {
            Control cb = (Control)sender;
            DataGrid dg = SelectFilelistDataGrid(sender, ((Control)sender).Name);
            List<SubItem> fileList = GetFileList(path);
            if (fileList == null)
            {
                MessageBox.Show("Acess Denied!");
                return;
            }
            dg.ItemsSource = fileList;
            FocusOnDatagridCelll(dg, 0, 0);

            //   DisplayPath(sender);

        }



        private void DisplayPath(object sender)
        {
            DataGrid dg = SelectFilelistDataGrid(sender, ((Control)sender).Name);


            TextBlock tb = SelectPathTextBlock(sender, ((Control)sender).Name);
            dg.SelectedItem = dg.Items[0];
            SubItem sb = (SubItem)dg.SelectedItem;
            tb.Text = sb.Directory.FullName;
        }

        private void DisplayDriveVolumeLabel(object sender)
        {
            TextBlock tb = SelectVolumeLabelTextBlock(sender, ((ComboBox)sender).Name);
            ComboBox cb = (ComboBox)sender;
            tb.Text = ((DriveInfo)cb.SelectedItem).VolumeLabel.ToString();
        }

        private DataGrid SelectFilelistDataGrid(object sender, string name)
        {
            string nm = name.Replace("Cmb", "").Replace("Drive", "").Replace("dg", "");
            //foreach (DataGrid dg in ((Grid)(((Control)sender).Parent)).Children.OfType<DataGrid>())
            foreach (DataGrid dg in  GrdMain.Children.OfType<DataGrid>())
            {
                if (dg.Name.ToLower().Contains(nm.ToLower()))
                {
                    return dg;
                }
            }

            return null;
        }

        private TextBlock SelectVolumeLabelTextBlock(object sender, string name)
        {
            string nm = name.Replace("Cmb", "").Replace("Drive", "");
            foreach (TextBlock tbl in ((Grid)(((Control)sender).Parent)).Children.OfType<TextBlock>())
            {
                if (tbl.Name.Contains(nm) && tbl.Name.ToLower().Contains("name"))
                {
                    return tbl;
                }
            }

            return null;
        }

        private TextBlock SelectPathTextBlock(object sender, string name)
        {
            string nm = name.Replace("Cmb", "").Replace("Drive", "").Replace("dg", "");
            foreach (TextBlock tbl in ((Grid)(((Control)sender).Parent)).Children.OfType<TextBlock>())
            {
                if (tbl.Name.Contains(nm) && tbl.Name.ToLower().Contains("path"))
                {
                    return tbl;
                }
            }

            return null;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }

        private void dg_GotFocus(object sender, RoutedEventArgs e)
        {
            //TblComandPath.Text = SelectPathTextBlock(sender, ((DataGrid)sender).Name).Text;
            //((Control)sender).Focus();
            this.SelectedSide = ((Control)sender).Name.Replace("dg", "");
        }

        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HandleDblClickOrEnterPressed(sender);
        }

        private void HandleDblClickOrEnterPressed(object sender)
        {
            SubItem selectedItem = ((SubItem)(((DataGrid)sender).SelectedItem));
            if (selectedItem.IsDirectory())
            {
                PopulateDataGrid(sender, selectedItem.FullName);

                //((DataGrid)sender).Focus();
                ((DataGrid)sender).SelectedIndex = 0;
            }
            else
            {
                Process.Start(selectedItem.FullName.ToString());
            }
        }



        private void dgKeyDn(object sender, KeyEventArgs e)
        {


            if (e.Key.Equals(Key.Return))
            {
                HandleDblClickOrEnterPressed(sender);
                e.Handled = true;
                return;

            }

            if (e.Key.Equals(Key.Tab))
            {
                HandleTabPressedInDatagrid(sender);

            }


        }

        

        private void HandleTabPressedInDatagrid(object sender)
        {
            DataGrid opositeDataGrid;
            string side = ((DataGrid)sender).Name.Replace("dg", "");
            string opositeSide = GetOpositeSide(side);
            opositeDataGrid = SelectFilelistDataGrid(sender, opositeSide);
            int row=opositeDataGrid.SelectedIndex;
            if (row < 0)
                row = 0;
            FocusOnDatagridCelll(opositeDataGrid, row, 0);
        }

        private static string GetOpositeSide(string side)
        {
            string opositeSide;
            switch (side.ToLower())
            {
                case "left":
                    opositeSide = "right";
                    break;
                case "right":
                    opositeSide = "left";
                    break;
                default:
                    opositeSide = "";
                    break;


            }

            return opositeSide;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DataGrid dg = SelectFilelistDataGrid(sender, SelectedSide);
            string name = ((SubItem)dg.SelectedItem).Name;
            NewFolderWindow dlg = new NewFolderWindow(name);

            // Configure the dialog box
            dlg.Owner = this;
            // Open the dialog box modally 
            dlg.ShowDialog();
            

        }

        private static void ResizeColumn(double proportionToTheAvaiableSpace, ColumnDefinition gridColumn)
        {
            GridLength gl = new GridLength(proportionToTheAvaiableSpace, GridUnitType.Star);
            gridColumn.Width = gl;
        }
    }

}
