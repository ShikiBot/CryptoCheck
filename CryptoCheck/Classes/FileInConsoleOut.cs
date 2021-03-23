using System;
using System.Text;

namespace CryptoCheck.Classes
{
    class FileInConsoleOut: FileStreamer
    {
        public FileInConsoleOut(string fileIN, string password, Args.ArgsType mode) : base(fileIN, "", password, mode)
        {
            Data = FileRead();
        }
        ~FileInConsoleOut() { }

        public new void Output()
        {
            if (Mode == Args.ArgsType.ENCRYPT) Console.Write(Encoding.Default.GetString(Encrypt(Data, Key)));
            else Console.Write(Encoding.Default.GetString(Decrypt(Data, Key)));
        }
    }
}
