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
            DirectoryInfo dir = new DirectoryInfo(path + name);
            if (dir.Exists)
                return false;
            try
            {
                dir.Create();
            }
            catch
            {
                MessageBox.Show("erroare la crearea folderului " + path + name);

            }
            return true;
        }

        public static bool CopyFileOrFolder(string source, string destination)
        {
            bool IsFile = true;

            FileAttributes attr = File.GetAttributes(source);

            if (attr.HasFlag(FileAttributes.Directory))
                IsFile = false;

            if (IsFile)
            {
                FileInfo fl = new FileInfo(source);
                if (!fl.Exists)
                {
                    return false;
                }
                try
                {
                    fl.CopyTo(destination);
                }
                catch
                {
                    MessageBox.Show(" Copy error !!!");
                }
            }
            else
            {
                DirectoryInfo dr = new DirectoryInfo(source);
                if (!dr.Exists)
                {
                    return false;
                }
                try
                {
                   
                    foreach (string dirPath in Directory.GetDirectories(source, "*",  SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(source, destination));

                    //Copy all the files & Replaces any files with the same name
                    foreach (string newPath in Directory.GetFiles(source, "*.*",SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(source, destination), true);

                }
                catch
                {
                    MessageBox.Show(" Copy error !!!");
                }
            }

            return true;

        }

        internal static bool MoveFileOrFolder(string source, string destination)
        {
            bool IsFile = true;

            FileAttributes attr = File.GetAttributes(source);

            if (attr.HasFlag(FileAttributes.Directory))
                IsFile = false;

            if (IsFile)
            {
                FileInfo fl = new FileInfo(source);
                if (!fl.Exists)
                {
                    return false;
                }
                try
                {
                    fl.MoveTo(destination);
                }
                catch
                {
                    MessageBox.Show(" Move error !!!");
                }
            }
            else
            {
                DirectoryInfo dr = new DirectoryInfo(source);
                if (!dr.Exists)
                {
                    return false;
                }
                try
                {

                    dr.MoveTo(destination);
                }
                catch
                {
                    MessageBox.Show(" Move error !!!");
                }
            }

            return true;
        }
    }
}
