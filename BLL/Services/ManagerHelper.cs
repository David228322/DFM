using System.IO;
using System.Linq;

namespace DFMLib
{
    public class ManagerHelper
    {
        public string PathName { get; set; }
        public ManagerHelper(string pathName)
        {
            PathName = pathName;
        }
        public bool DirectoryHasNotIllegalChar(string dirName) => dirName.IndexOfAny("<>:\"/\\|?*".ToCharArray()) == -1;

        public bool EntriesInDirectory(string dirName, string pathName)
            => !Directory.GetFileSystemEntries(Path.Combine(pathName, dirName)).Any();

        public FileAttributes GetFileAttributes(string FileAttribute, string PathName)
            => File.GetAttributes(Path.Combine(PathName, FileAttribute));

        public bool FileOrDirectoryExists(string resultFile, string pathName, FileAttributes expected = FileAttributes.ReadOnly)
        {
            if (expected == FileAttributes.Directory)
            {
                return Directory.Exists(Path.Combine(pathName, resultFile));
            }
            else if (expected == FileAttributes.Normal)
            {
                return File.Exists(Path.Combine(pathName, resultFile));

            }
            return File.Exists(Path.Combine(pathName, resultFile)) || Directory.Exists(Path.Combine(pathName, resultFile));
        }
    }
}
