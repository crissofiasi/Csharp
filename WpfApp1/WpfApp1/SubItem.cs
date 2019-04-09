using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace WpfApp1
{
    public class SubItem
    {
        //        { get; set; }
        public string DisplayName { get; set; }
        public string Extension { get; set; } // Gets the string representing the extension part of the file.

        public DateTime CreationTime { get; set; }  //creation time of the current file or directory
        public DirectoryInfo Directory { get; set; }  //  Gets an instance of the parent directory.
        public string DirectoryName { get; set; } // Gets a string representing the directory's full path.
        public string FullName { get; set; } // Gets the full path of the directory or file.
        public DateTime LastAccessTime { get; set; } //  Gets the time the current file or directory was last accessed.
        public DateTime LastWriteTime { get; set; } // Gets the time when the current file or directory was last written to.
        public long Length { get; set; } // Gets the size, in bytes, of the current file.
        public string Name { get; set; } // Gets the name of the file.
        private bool IsDir { get; set; }
        public Icon SistemIcon { get; set; }

        public SubItem(FileInfo s)
        {
            this.DisplayName = this.Name = s.Name;
            this.CreationTime = s.CreationTime;
            this.Directory = s.Directory;
            this.DirectoryName = s.DirectoryName;
            this.Extension = s.Extension;
            this.FullName = s.FullName;
            this.LastAccessTime = s.LastAccessTime;
            this.LastWriteTime = s.LastWriteTime;
            this.Length = s.Length;
            this.IsDir = false;
            this.SistemIcon = Icon.ExtractAssociatedIcon(this.FullName);
        }

        public SubItem(DirectoryInfo s)
        {
            this.DisplayName = this.Name = s.Name;
            this.CreationTime = s.CreationTime;
            this.Directory = s;
            this.DirectoryName = s.FullName;
            this.Extension = s.Extension;
            this.FullName = s.FullName;
            this.LastAccessTime = s.LastAccessTime;
            this.LastWriteTime = s.LastWriteTime;
            this.Length = 0;
            this.IsDir = true;
          //  this.SistemIcon = Icon.ExtractAssociatedIcon(this.FullName);
        }

        public SubItem(DirectoryInfo s, string DirectoryDisplayName)
        {
            this.Name = s.Name;
            this.CreationTime = s.CreationTime;
            this.Directory = s;
            if (this.Directory!=null)
                this.DirectoryName = s.FullName;
            this.Extension = s.Extension;
            this.FullName = s.FullName;
            this.LastAccessTime = s.LastAccessTime;
            this.LastWriteTime = s.LastWriteTime;
            this.Length = 0;
            this.DisplayName = DirectoryDisplayName;
            this.IsDir = true;
            //this.SistemIcon = Icon.ExtractAssociatedIcon(this.FullName);
        }

        public SubItem(FileInfo s, string FileDisplayName)
        {
            this.DisplayName = FileDisplayName;
            this.Name = s.Name;
            this.CreationTime = s.CreationTime;
            this.Directory = s.Directory;
            this.DirectoryName = s.DirectoryName;
            this.Extension = s.Extension;
            this.FullName = s.FullName;
            this.LastAccessTime = s.LastAccessTime;
            this.LastWriteTime = s.LastWriteTime;
            this.Length = s.Length;
            this.IsDir = false;
            this.SistemIcon = Icon.ExtractAssociatedIcon(this.FullName);
        }

        public bool IsDirectory()
        {
            return this.IsDir;
        }


    }
}
