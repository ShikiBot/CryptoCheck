using System;
using System.Text;

namespace CryptoCheck.Classes
{
    class FileInConsoleOut: FileStreamer
    {
        public FileInConsoleOut(string fileIN, string password, ArgsType mode) : base(fileIN, "", password, mode)
        {
            Data = FileRead();
        }
        ~FileInConsoleOut() { }

        public override void Output()
        {
            if (Mode == ArgsType.ENCRYPT) Console.Write(Encoding.Default.GetString(Gost28147_89.Encrypt(Data, Key)));
            else Console.Write(Encoding.Default.GetString(Gost28147_89.Decrypt(Data, Key)));
        }
    }
}
