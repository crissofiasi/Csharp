using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp1
{
    public class SubItem
    {


        DateTime CreationTime;  //creation time of the current file or directory
        DirectoryInfo Directory;  //  Gets an instance of the parent directory.
        string DirectoryName; // Gets a string representing the directory's full path.
        string Extension; // Gets the string representing the extension part of the file.
        string FullName; // Gets the full path of the directory or file.
        DateTime LastAccessTime; //  Gets the time the current file or directory was last accessed.
        DateTime LastWriteTime; // Gets the time when the current file or directory was last written to.
        long Length; // Gets the size, in bytes, of the current file.
        string Name; // Gets the name of the file.
        string DisplayName;

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
        }

        public SubItem(DirectoryInfo s)
        {
            this.DisplayName = this.Name = s.Name;
            this.CreationTime = s.CreationTime;
            this.Directory = s.Parent;
            this.DirectoryName = s.Parent.FullName;
            this.Extension = s.Extension;
            this.FullName = s.FullName;
            this.LastAccessTime = s.LastAccessTime;
            this.LastWriteTime = s.LastWriteTime;
            this.Length = 0;
        }

        public SubItem(DirectoryInfo s, string DirectoryDisplayName)
        {
            this.Name = s.Name;
            this.CreationTime = s.CreationTime;
            this.Directory = s.Parent;
            if (this.Directory!=null)
                this.DirectoryName = s.Parent.FullName;
            this.Extension = s.Extension;
            this.FullName = s.FullName;
            this.LastAccessTime = s.LastAccessTime;
            this.LastWriteTime = s.LastWriteTime;
            this.Length = 0;
            this.DisplayName = DirectoryDisplayName;
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
        }




    }
}
