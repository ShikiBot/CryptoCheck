using System;
using CryptoCheck.Classes;

namespace CryptoCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = "-t 12 -p password".Split(' ');
            //! добавить ошибку про неизвестные аргументы и проверить ошибку на противоречие или отсутствие аргументов (в том числе и взаимодействие с help)
            OutputParser parser = new OutputParser(args);
            Object answer = parser.OutputClass();
            if (answer is HelpInfo help) help.ReturnHelp();
            if (answer is ConsoleInOut conInOut) conInOut.Output();
            if (answer is ConsoleInFileOut conInFilOut) conInFilOut.Output();
            if (answer is FileInConsoleOut filInConOut) filInConOut.Output();
            if (answer is FileInFileOut filInOut) filInOut.Output();
            if (answer is Exception exept) Console.Write(exept.Message);
            //Console.ReadKey();
        }
    }
}
