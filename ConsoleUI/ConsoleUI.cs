using System;
using System.IO;
using DFMLib;
using Microsoft.Extensions.DependencyInjection;
using Pl;

namespace PL
{
    class ConsoleUI
    {
        static void Main(string[] args)
        {
            AppStart();
        }
        private static void AppStart()
        {
            InputCommand inputCommand = new InputCommand();
            inputCommand.UserInput();
        }
    }
}
