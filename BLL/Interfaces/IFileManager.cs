using System.IO;

namespace DFMLib
{
    public interface IFileManager
    {
        string PathName { get; }

        void ChangeDirectory(string newRoofPath);

        void CreateNewDirectory(string dirName);

        void DeleteDirectory(string delDir);

        void ListDirectoryContent(string sortingFlag);

        string ToString() => this.PathName.ToString() + ">";

        void RenameDirectory(string oldDirectoryName, string newDirectoryName);

        void DeleteFile(string delFile);

        void CreateFile(string newFile);

        public string FindStringInFile(string file, string searchString);

        public string FileReading(string file);
    }
}