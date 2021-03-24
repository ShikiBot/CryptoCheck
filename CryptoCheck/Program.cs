using System;
using CryptoCheck.Classes;

namespace CryptoCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputParser parser = new OutputParser(args);
            Object answer = parser.OutputClass();
            if (answer is HelpInfo help) help.ReturnHelp();
            if (answer is ConsoleInOut conInOut) conInOut.Output();
            if (answer is ConsoleInFileOut conInFilOut) conInFilOut.Output();
            if (answer is FileInConsoleOut filInConOut) filInConOut.Output();
            if (answer is FileInFileOut filInOut) filInOut.Output();
            if (answer is Exception exept) Console.Write(exept.Message);
        }
    }
}
