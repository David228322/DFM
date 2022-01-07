using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DFMLib
{
    public class FileManager : IFileManager
    {
        private string roofPath;
        private ManagerHelper managerHelper;

        public FileManager(string name)
        {
            this.roofPath = name;
            this.managerHelper = new ManagerHelper();
        }

        public string PathName => this.roofPath;

        public override string ToString() => $"{this.PathName}>";

        public void ListDirectoryContent(string sortingFlag)
        {
            var sortResult = this.managerHelper.SortingFiles(sortingFlag, this.PathName);
            var files = sortResult.Item1.ToArray();
            var directories = sortResult.Item2.ToArray();
            foreach (var dir in directories)
            {
                Console.WriteLine($"{dir.CreationTimeUtc,-30} {this.managerHelper.GetFileOrDirAttribute(dir.Attributes),-20} {dir.Name,-10}");
            }

            // files.OrderByDescending(file => file.Length);
            foreach (var file in files)
            {
                Console.WriteLine($"{file.CreationTimeUtc,-30} {this.managerHelper.GetFileOrDirAttribute(file.Attributes),-20} {file.Name,-10}");
            }
        }

        public void ChangeDirectory(string newRoofPath)
        {
            if (this.managerHelper.FileOrDirectoryExists(newRoofPath, string.Empty, ManagerHelper.ExpectedAttributes.Directory))
            {
                this.roofPath = newRoofPath;
            }
            else
            {
                Console.WriteLine("System can't find the path specified.");
            }
        }

        public void CreateNewDirectory(string dirName)
        {
            if (this.managerHelper.DirectoryHasNotIllegalChar(dirName))
            {
                dirName.Split(" ").ToList()
                  .ForEach(x => Directory.CreateDirectory(Path.Combine(this.PathName, x)));
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
                delDir = delDir.Remove(qPosition, "/q".Length).Insert(qPosition, string.Empty).Trim();
                int sPosition = delDir.IndexOf("/s");
                delDir = sPosition == -1 ? delDir
                    : delDir.Remove(sPosition, "/s".Length).Insert(sPosition, string.Empty).Trim();

                delAll = true;
            }
            else if (delDir.Contains("/s"))
            {
                delDir = delDir.Replace("/s", string.Empty).Trim();
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

            if (delDir.Split(" ").All(x => Directory.Exists(Path.Combine(this.PathName, x))
                    && (!Directory.GetFileSystemEntries(Path.Combine(this.PathName, x)).Any() || delAll)))
            {
                delDir.Split(" ").ToList().ForEach(x => Directory.Delete(Path.Combine(this.PathName, x), delAll));
            }
            else
            {
                Console.WriteLine("Yoг're trying to remove a directory that doesn't exist or folder isn't empty");
            }
        }

        public void RenameDirectory(string oldDirectoryName, string newDirectoryName)
        {
            if (this.managerHelper.FileOrDirectoryExists(oldDirectoryName, this.PathName) && this.managerHelper.DirectoryHasNotIllegalChar(newDirectoryName))
            {
                if (this.managerHelper.GetFileAttributes(oldDirectoryName, this.PathName).HasFlag(FileAttributes.Directory))
                {
                    Directory.Move(Path.Combine(this.PathName, oldDirectoryName), Path.Combine(this.PathName, newDirectoryName));
                }
                else
                {
                    File.Move(Path.Combine(this.PathName, oldDirectoryName), Path.Combine(this.PathName, newDirectoryName));
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist or you're trying use illegal chars for new file name.");
            }
        }

        public void DeleteFile(string fileToDel)
        {
            if (this.managerHelper.FileOrDirectoryExists(fileToDel, this.PathName, ManagerHelper.ExpectedAttributes.All))
            {
                File.Delete(Path.Combine(this.PathName, fileToDel));
            }
            else
            {
                Console.WriteLine("Yoг're trying to remove a file that doesn't exist");
            }
        }

        public void CreateFile(string fileToCreate)
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(this.PathName, fileToCreate));
            using (FileStream fileStream = fileInfo.Create())
            {
                byte[] info = new UTF8Encoding(true).GetBytes(string.Empty);
                fileStream.Write(info, 0, info.Length);
            }
        }

        public string FileReading(string file)
        {
            if (this.managerHelper.FileOrDirectoryExists(file, this.PathName, ManagerHelper.ExpectedAttributes.File))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(this.PathName, file)))
                {
                    string result = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(result))
                    {
                        return "File doesn't containts strings";
                    }

                    if (result.Length < 200)
                    {
                        return result;
                    }
                    else
                    {
                        return "To many symbols in file";
                    }
                }
            }
            else
            {
                return "This file doesn't exist.";
            }
        }

        public string FindStringInFile(string file, string findString)
        {
            if (string.IsNullOrWhiteSpace(findString))
            {
                return "Choose correct substring.";
            }

            if (this.managerHelper.FileOrDirectoryExists(file, this.PathName, ManagerHelper.ExpectedAttributes.File))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(this.PathName, file)))
                {
                    string stringToCheck = sr.ReadToEnd();
                    var subCon = stringToCheck.Contains(findString);
                    return subCon ? findString : "File doesn't contains this string";
                }
            }
            else
            {
                return "File doesn't exist.";
            }
        }
    }
}
