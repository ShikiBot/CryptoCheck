using System;
using System.Diagnostics;
using CryptoCheck.Classes;

namespace CryptoCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            OutputParser parser = new OutputParser(args);
            Object answer = parser.OutputClass();
            if (answer is HelpInfo help) help.Output();
            sw.Start();
            if (answer is ConsoleInOut conInOut) conInOut.Output();
            if (answer is ConsoleInFileOut conInFilOut) conInFilOut.Output();
            if (answer is FileInConsoleOut filInConOut) filInConOut.Output();
            if (answer is FileInOut filInOut) filInOut.Output();
            sw.Stop();
            if (answer is Exception exept) Console.Write(exept.Message);
            if (sw.ElapsedMilliseconds > 0) Console.Write($"\nпотрачено времени на преобразование данных: {sw.ElapsedMilliseconds} мс.");
        }
    }
}
