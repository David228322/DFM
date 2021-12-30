using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace DFMLib
{
    public class FoldersManager : IFileManager
    {
        private string roofPath;
        private ManagerHelper _managerHelper;
        public FoldersManager(string name)
        {
            this.roofPath = name;
            _managerHelper = new ManagerHelper(name);
        }
        public string PathName => roofPath;

        public override string ToString() => PathName.ToString() + ">";

        public void ListDirectoryContent(string strPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(PathName);
            // Shows all directories in path
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            foreach (var dir in dirs)
            {
                Console.WriteLine($"{dir.CreationTimeUtc,-30} {GetFileOrDirAttribute(dir.Attributes),-20} {dir.Name,-10}");
            }

            // Shows all files in path
            FileInfo[] files = dirInfo.GetFiles("*.*");
            //files.OrderByDescending(file => file.Length);
            foreach (var file in files)
            {
                Console.WriteLine($"{file.CreationTimeUtc,-30} {GetFileOrDirAttribute(file.Attributes),-20} {file.Name,-10}");
            }
        }

        public void ChangeDirectory(string newRoofPath)
        {
            if (_managerHelper.FileOrDirectoryExists(newRoofPath, PathName, FileAttributes.Directory))
            {
                roofPath = newRoofPath;
                _managerHelper.PathName = newRoofPath;
            }
            else
            {
                Console.WriteLine("System can't find the path specified.");
            }
        }

        public void CreateNewDirectory(string dirName)
        {
            if (_managerHelper.DirectoryHasNotIllegalChar(dirName))

            {
                dirName.Split(" ").ToList()
                  .ForEach(x => Directory.CreateDirectory(Path.Combine(PathName, x)));
            }
            else
            {
                Console.WriteLine("You're trying to create a directory using illegal characters.");
            }
        }

        public void DeleteDirectory(string delDir)
        {
            bool delAll = default;
            if (delDir.Contains("/q"))
            {
                int qPosition = delDir.IndexOf("/q");
                delDir = delDir.Remove(qPosition, "/q".Length).Insert(qPosition, "").Trim();
                int sPosition = delDir.IndexOf("/s");
                delDir = delDir.Remove(qPosition, "/s".Length).Insert(qPosition, "").Trim();
                delAll = true;
            }
            else if (delDir.Contains("/s"))
            {
                delDir = delDir.Replace("/s", "").Trim();
                while (delAll != true)
                {
                    Console.Write($"{delDir}, Are you sure [Y(yes)/N(no)]? ");
                    string confirmation = Console.ReadLine();
                    if (confirmation == "Y")
                    {
                        delAll = true;
                    }
                    else if (confirmation == "N")
                    {
                        return;
                    }
                }
            }
            if (delDir.Split(" ").All
                (x => Directory.Exists(Path.Combine(PathName, x))
                    && (!Directory.GetFileSystemEntries(Path.Combine(PathName, x)).Any() || delAll)))
            {
                delDir.Split(" ").ToList().ForEach(x => Directory.Delete(Path.Combine(PathName, x), delAll));
            }
            else
            {
                Console.WriteLine("Yoг're trying to remove a directory that doesn't exist or folder isn't empty");
            }
        }

        public string GetFileOrDirAttribute(FileAttributes dirOrFileName)
        {
            StringBuilder sb = new StringBuilder();
            CustomDictionary<FileAttributes, String> dictionary = new CustomDictionary<FileAttributes, string>
            {
                { FileAttributes.Archive,"A" },
                {FileAttributes.Directory,"D" },
                {FileAttributes.Hidden,"H"},
                { FileAttributes.NotContentIndexed,"I"},
                { FileAttributes.ReparsePoint,"L"},
                { FileAttributes.ReadOnly,"R"},
                { FileAttributes.System,"S"}
            };
            foreach (var atr in dictionary)
            {
                if (dirOrFileName.HasFlag(atr.Key))
                {
                    sb.Append(atr.Value);
                }
                else
                {
                    sb.Append("-");
                }
            }
            return sb.ToString();
        }

        public void RenameDirectory(string[] renDir)
        {
            if (_managerHelper.FileOrDirectoryExists(renDir[0], PathName))
            {
                if (_managerHelper.GetFileAttributes(renDir[0], PathName) == FileAttributes.Directory)
                {
                    Directory.Move(Path.Combine(PathName, renDir[0]), Path.Combine(PathName, renDir[1]));
                }
                else
                {
                    File.Move(Path.Combine(PathName, renDir[0]), Path.Combine(PathName, renDir[1]));
                }
            }
        }

        public void DeleteFile(string fileToDel)
        {
            if (_managerHelper.FileOrDirectoryExists(fileToDel, PathName, FileAttributes.Normal))
            {
                File.Delete(Path.Combine(PathName, fileToDel));
            }
            else
            {
                Console.WriteLine("Yoг're trying to remove a file that doesn't exist");
            }
        }

        public void CreateFile(string fileToCreate)
        {

            FileInfo fi = new FileInfo(Path.Combine(PathName, fileToCreate));
            using (FileStream fs = fi.Create())
            {
                Byte[] info =
                new UTF8Encoding(true).GetBytes("");
                fs.Write(info, 0, info.Length);

            }
        }

    }
}
