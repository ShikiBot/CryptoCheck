using System;

namespace CryptoCheck.Classes
{
    class HelpInfo
    {
        ~HelpInfo() { }
        public void ReturnHelp()
        {
            string help =
                "\n\t\t    Gost28147-89\n\n" +
                " -e \t\t\t\tпараметр шифрования\n" +
                " -d \t\t\t\tпараметр дефрования\n" +
                " -h (--help)\t\t\tсправка\n" +
                " -t (--text)\t\t\tшифруемый текст\n"+
                " -p (--password)\t\tпароль\n" +
                " -in *имя входного файла*\n"+
                " -out *имя выходного файла*\n";
            Console.Write(help);
        }
    }
}
