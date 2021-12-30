using DFMLib;
using System;
using System.IO;
using System.Text;

namespace Pl
{
    class InputCommand
    {
        public void UserInput()
        {
            IFileManager fileManager = AddSampleData();
            bool isRunning = true;
            string inputCommand = "";
            string inputResult = "";
            while (isRunning)
            {
                Console.Write(fileManager.ToString());
                string inputLine = Console.ReadLine();
                if (inputLine.Contains(" "))
                {
                    inputResult = inputLine.Substring(inputLine.IndexOf(" ") + 1);
                    inputCommand = inputLine.Substring(0, inputLine.IndexOf(" "));
                }
                else
                {
                    inputCommand = inputLine;
                }

                switch (inputCommand)
                {
                    case "exit":
                        isRunning = false;
                        break;
                    case "cls":
                        Console.Clear();
                        break;
                    case "dir":
                        var stringPath = inputCommand;
                        fileManager.ListDirectoryContent(stringPath.ToString());
                        break;
                    case "cd":
                        var newPath = inputResult;
                        fileManager.ChangeDirectory(newPath.ToString());
                        break;
                    case "mkdir":
                        var newDir = inputResult;
                        fileManager.CreateNewDirectory(newDir.ToString());
                        break;
                    case "fsutil":
                        var newFile = inputResult;
                        fileManager.CreateFile(newFile.ToString());
                        break;
                    case "rmdir":
                        var delDir = inputResult;
                        fileManager.DeleteDirectory(delDir.ToString());
                        break;
                    case "del":
                        var delFile = inputResult;
                        fileManager.DeleteFile(delFile.ToString());
                        break;
                    default:
                        Console.WriteLine("Can't find necessary command");
                        break;
                }
            }
        }
        private static IFileManager AddSampleData()
        {
            var output = new FileManager(Directory.GetCurrentDirectory());
            return output;
        }
    }
}
