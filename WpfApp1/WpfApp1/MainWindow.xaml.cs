using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HandleWindowLoaded();
        }
        private void HandleWindowLoaded()
        {
            ClearWindow();
            PopulateDriveComboBoxes();
            dgLeft.ItemsSource = dataLists.listLeft;
            dgRight.ItemsSource = dataLists.listRight;
            PopulateDataGrid(CmbLeftDrive, CmbLeftDrive.Text);
            PopulateDataGrid(CmbRightDrive, CmbLeftDrive.Text);
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
            HandleCmbSelectionChanged(sender, e);
        }
        private void HandleCmbSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (!((DriveInfo)cb.SelectedItem).IsReady)
            {
                MessageBox.Show("Drive Not Redy! Please chose onother");
                cb.SelectedItem = e.RemovedItems[0];
                return;
            }
            DisplayDriveVolumeLabel(sender);
            string path = ((DriveInfo)cb.SelectedItem).RootDirectory.FullName;
            DisplayPath(sender, path);
            PopulateDataGrid(sender, path);
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
            List<SubItem> fileList = IO_Utils.GetFileList(path);
            if (fileList == null)
            {
                MessageBox.Show("Acess Denied!");
                return;
            }
            dg.ItemsSource = fileList;
            FocusOnDatagridCelll(dg, 0, 0);
            DisplayPath(sender, path);
        }
        private void PopulateDataGrid(DataGrid dg, string path)
        {
            List<SubItem> fileList = IO_Utils.GetFileList(path);
            if (fileList == null)
            {
                MessageBox.Show("Acess Denied!");
                return;
            }
            dg.ItemsSource = fileList;
            FocusOnDatagridCelll(dg, 0, 0);
        }
        private void DisplayPath(object sender,string path)
        {
            TextBlock tb = SelectPathTextBlock(sender, ((Control)sender).Name);
            tb.Text = path ;
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
            foreach (DataGrid dg in GrdMain.Children.OfType<DataGrid>())
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
            foreach (TextBlock tbl in GrdMain.Children.OfType<TextBlock>())
            {
                if (tbl.Name.ToLower().Contains(nm.ToLower()) && tbl.Name.ToLower().Contains("path"))
                {
                    return tbl;
                }
            }
            return null;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            HandleExit();
        }

        private void HandleExit()
        {
            mainWindow.Close();
        }

        private void dg_GotFocus(object sender, RoutedEventArgs e)
        {
              this.SelectedSide = ((Control)sender).Name.Replace("dg", "");
        }

        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HandleDblClickOrEnterPressed(sender);
        }

        private void HandleDblClickOrEnterPressed(object sender)
        {
            SubItem selectedItem = ((SubItem)(((DataGrid)sender).SelectedItem));
            string path = selectedItem.FullName;
            if (selectedItem.IsDirectory())
            {   
                PopulateDataGrid(sender, path);
                ((DataGrid)sender).SelectedIndex = 0;
            }
            else
            {
                Process.Start(selectedItem.FullName.ToString());
            }
        }
        private void dgKeyDn(object sender, KeyEventArgs e)
        {
            HandleKeyDownOnDataGrid(sender, e);
        }

        private void HandleKeyDownOnDataGrid(object sender, KeyEventArgs e)
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
            int row = opositeDataGrid.SelectedIndex;
            if (row < 0)
            {
                row = 0;
            }
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
        private static void ResizeColumn(double proportionToTheAvaiableSpace, ColumnDefinition gridColumn)
        {
            GridLength gl = new GridLength(proportionToTheAvaiableSpace, GridUnitType.Star);
            gridColumn.Width = gl;
        }
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            HandleCopy(sender);
        }
        private void HandleCopy(object sender)
        {
            string source;
            string destination;
            GetSourceAndDestinationDirectory(sender, out source, out destination);
            SubItem itemToCopy = ((SubItem)SelectFilelistDataGrid(sender, this.SelectedSide).SelectedItem);
            if (itemToCopy.Name != itemToCopy.DisplayName)
            {
                MessageBox.Show("Eroare: Nu Poti Copia Directorul Parinte!");
                return;
            }
            string sourceItem = source + itemToCopy.Name;
            string destinationItem = destination + itemToCopy.Name;
            CopyOrMoveWindow dlg = new CopyOrMoveWindow("Copy", sourceItem, destinationItem);
            dlg.Owner = this;
            dlg.ShowDialog();
            PopulateDataGrid(dgLeft, TblPathLeft.Text);
            PopulateDataGrid(dgRight, TblPathRight.Text);
        }

        private void GetSourceAndDestinationDirectory(object sender, out string source, out string destination)
        {
            source = SelectPathTextBlock(sender, this.SelectedSide).Text.Trim();
            destination = SelectPathTextBlock(sender, GetOpositeSide(this.SelectedSide)).Text.Trim();
            if (!source.EndsWith("\\"))
            {
                source += "\\";
            }
            if (!destination.EndsWith("\\"))
            {
                destination += "\\";
            }
        }

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(sender);
        }

        private void HandleMove(object sender)
        {
            string source;
            string destination;
            GetSourceAndDestinationDirectory(sender, out source, out destination);
            SubItem itemToMove = ((SubItem)SelectFilelistDataGrid(sender, this.SelectedSide).SelectedItem);
            if (itemToMove.Name != itemToMove.DisplayName)
            {
                MessageBox.Show("Eroare: Nu Poti Muta Directorul Parinte!");
                return;
            }
            source += itemToMove.Name;
            destination += itemToMove.Name;
            CopyOrMoveWindow dlg = new CopyOrMoveWindow("Move", source, destination);
            dlg.Owner = this;
            dlg.ShowDialog();
            PopulateDataGrid(dgLeft, TblPathLeft.Text);
            PopulateDataGrid(dgRight, TblPathRight.Text);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            HandleEdit(sender);
        }

        private void HandleEdit(object sender)
        {
            string source;
            string destination;
            GetSourceAndDestinationDirectory(sender, out source, out destination);
            SubItem selectedItem = ((SubItem)SelectFilelistDataGrid(sender, this.SelectedSide).SelectedItem);
            if (!selectedItem.IsDirectory())
            {
                Process.Start("notepad.exe", selectedItem.FullName.ToString());
            }
        }

        private void MnuExit_Click(object sender, RoutedEventArgs e)
        {
            HandleExit();
        }

        private void MnuEdit_Click(object sender, RoutedEventArgs e)
        {
            HandleEdit(sender);
        }

        private void MnView_Click(object sender, RoutedEventArgs e)
        {
            HandleEdit(sender);
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            HandleNewFolder(sender);
        }

        private void HandleNewFolder(object sender)
        {
            DataGrid dg = SelectFilelistDataGrid(sender, SelectedSide);
            string name = ((SubItem)dg.SelectedItem).Name;
            string displayName = ((SubItem)dg.SelectedItem).DisplayName;
            string fullname = ((SubItem)dg.SelectedItem).CurrentDirectory.FullName;
            string path = fullname;
            if (name == displayName)
                path = fullname.Replace(name, "");
            else
                name = "";
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            NewFolderWindow dlg = new NewFolderWindow(path, name);
            // Configure the dialog box
            dlg.Owner = this;
            // Open the dialog box modally 
            dlg.ShowDialog();
            PopulateDataGrid(dg, path);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            HandleDelete(sender);
        }

        private void HandleDelete(object sender)
        {
            string source;
            string destination;
            GetSourceAndDestinationDirectory(sender, out source, out destination);
            SubItem itemToDelete = ((SubItem)SelectFilelistDataGrid(sender, this.SelectedSide).SelectedItem);
            if (itemToDelete.Name != itemToDelete.DisplayName)
            {
                MessageBox.Show("Eroare: Nu Poti Sterge Directorul Parinte!");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Siguri vrei sa stergi "+itemToDelete.Name.Trim()+"?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                itemToDelete.Delete();
            }
            PopulateDataGrid(dgLeft, TblPathLeft.Text);
            PopulateDataGrid(dgRight, TblPathRight.Text);
        }
    }

}
