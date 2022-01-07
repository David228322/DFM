using System;
using System.IO;
using DFMLib;

namespace Pl
{
    internal class InputCommand
    {
        public void UserInput()
        {
            IFileManager fileManager = AddSampleData();
            bool isRunning = true;
            string inputResult = string.Empty;
            while (isRunning)
            {
                Console.Write(fileManager.ToString());
                string inputLine = Console.ReadLine();
                string inputCommand;
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
                        string pathFlag = inputResult;
                        fileManager.ListDirectoryContent(pathFlag);
                        break;
                    case "cd":
                        var newPath = inputResult;
                        fileManager.ChangeDirectory(newPath);
                        break;
                    case "mkdir":
                        var newDir = inputResult;
                        fileManager.CreateNewDirectory(newDir);
                        break;
                    case "fsutil":
                        var newFile = inputResult;
                        fileManager.CreateFile(newFile);
                        break;
                    case "rmdir":
                        var delDir = inputResult;
                        fileManager.DeleteDirectory(delDir);
                        break;
                    case "del":
                        var delFile = inputResult;
                        fileManager.DeleteFile(delFile);
                        break;
                    case "ren":
                        var oldName = inputLine.Split(" ")[1];
                        var newName = inputLine.Split(" ")[2];
                        fileManager.RenameDirectory(oldName, newName);
                        break;
                    case "sub":
                        var fileName = inputResult.Substring(0, inputResult.IndexOf(" "));
                        var searchString = inputResult.Remove(0, fileName.Length + 1);
                        Console.WriteLine(fileManager.FindStringInFile(fileName, searchString));
                        break;
                    case "type":
                        var readFile = inputResult;
                        Console.WriteLine(fileManager.FileReading(readFile));
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
