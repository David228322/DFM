using System.IO;

namespace DFMLib
{
    public interface IFileManager
    {
        string PathName { get; }
        void ChangeDirectory(string newRoofPath);
        void CreateNewDirectory(string dirName);
        void DeleteDirectory(string delDir);
        string GetFileOrDirAttribute(FileAttributes dirOrFileName);
        void ListDirectoryContent(string strPath);
        public string ToString() => PathName.ToString() + ">";
        void RenameDirectory(string[] renDir);
        void DeleteFile(string delFile);
        void CreateFile(string newFile);
    }
}