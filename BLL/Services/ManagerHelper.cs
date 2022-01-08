using System.IO;
using System.Linq;
using System.Text;

namespace DFMLib
{
    public class ManagerHelper
    {
        public enum ExpectedAttributes
        {
            /// <summary>
            /// expected recieve directory checking
            /// </summary>
            Directory = 1,

            /// <summary>
            /// expected recieve file checking
            /// </summary>
            File = 2,

            /// <summary>
            /// doesn't matter what to recieve
            /// </summary>
            All = 3,
        }

        public bool DirectoryHasNotIllegalChar(string dirName) => dirName.IndexOfAny("<>:\"/\\|?*".ToCharArray()) == -1;

        public bool EntriesInDirectory(string dirName, string pathName)
            => Directory.GetFileSystemEntries(Path.Combine(pathName, dirName)).Any();

        public FileAttributes GetFileAttributes(string fileName, string pathName)
            => File.GetAttributes(Path.Combine(pathName, fileName));

        public bool FileOrDirectoryExists(string resultFile, string pathName, ExpectedAttributes expected = ExpectedAttributes.All)
        {
            if (expected == ExpectedAttributes.Directory)
            {
                return Directory.Exists(Path.Combine(pathName, resultFile));
            }
            else if (expected == ExpectedAttributes.File)
            {
                return File.Exists(Path.Combine(pathName, resultFile));
            }

            return File.Exists(Path.Combine(pathName, resultFile)) || Directory.Exists(Path.Combine(pathName, resultFile));
        }

        public string GetFileOrDirAttribute(FileAttributes dirOrFileName)
        {
            StringBuilder sb = new StringBuilder();
            FileAttributes[] fileAttributes =
            {
                FileAttributes.Archive,
                FileAttributes.Directory,
                FileAttributes.Hidden,
                FileAttributes.NotContentIndexed,
                FileAttributes.ReparsePoint,
                FileAttributes.ReadOnly,
                FileAttributes.System,
            };

            foreach (var attribute in fileAttributes)
            {
                if (dirOrFileName.HasFlag(attribute))
                {
                    string attributeToString = attribute.ToString();
                    string result = attributeToString[0].ToString();
                    sb.Append(result);
                }
                else
                {
                    sb.Append("-");
                }
            }

            return sb.ToString();
        }

        public (FileInfo[], DirectoryInfo[]) SortingFiles(string inputFlags, string path)
        {
            // Shows all directories in path
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] directories = dirInfo.GetDirectories();

            // Shows all files in path
            FileInfo[] files = dirInfo.GetFiles();

            if (inputFlags.Contains("/D"))
            {
                files = files.OrderByDescending(x => x.CreationTimeUtc).ToArray();
                directories = directories.OrderByDescending(dir => dir.CreationTimeUtc).ToArray();
            }
            else if (inputFlags.Contains("/E"))
            {
                files = files.OrderByDescending(x => x.Extension).ToArray();
                directories = directories.OrderByDescending(dir => dir.Extension).ToArray();
            }
            else if (inputFlags.Contains("/N"))
            {
                files = files.OrderByDescending(x => x.Name).ToArray();
                directories = directories.OrderByDescending(dir => dir.Name).ToArray();
            }
            else if (inputFlags.Contains("/S"))
            {
                files = files.OrderByDescending(x => x.Length).ToArray();
                directories = directories.OrderByDescending(dir => dir.Name).ToArray();
            }
            else
            {
                files = files.OrderByDescending(x => x.Name).ToArray();
                directories = directories.OrderByDescending(dir => dir.Name).ToArray();
            }

            var result = (Files: files, Directories: directories);
            return result;
        }
    }
}