using System;
using System.Text;

namespace CryptoCheck.Classes
{
    class ConsoleInOut: IObase
    {
        public ConsoleInOut(string message, string password, ArgsType mode) : base(password, mode)
        {
            Data = Encoding.Default.GetBytes(message);
        }
        ~ConsoleInOut() { }
        public new void Output()
        {
            if (Mode == ArgsType.ENCRYPT) Console.Write(Encoding.Default.GetString(Gost28147_89.Encrypt(Data, Key)));
            else Console.Write(Encoding.Default.GetString(Gost28147_89.Decrypt(Data, Key)));
        }
    }
}
