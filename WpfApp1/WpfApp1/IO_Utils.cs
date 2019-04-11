using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace WpfApp1
{
    class IO_Utils
    {
        public static bool NewFolder(string path, string name)
        {
            bool rt = true;
            DirectoryInfo dir = new DirectoryInfo(path+name);
            if (dir.Exists)
                return false;
            try { 
            dir.Create();
            }
            catch
            {
                MessageBox.Show("erroare la crearea folderului "+path+name);
                
            }
            return true;
        }

    }
}
