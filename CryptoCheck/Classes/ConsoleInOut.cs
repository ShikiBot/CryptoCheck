using System;
using System.Text;

namespace CryptoCheck.Classes
{
    class ConsoleInOut: GostReturn
    {
        public ConsoleInOut(string message, string password, Args.ArgsType mode) : base(password, mode)
        {
            Data = Encoding.Default.GetBytes(message);
        }
        ~ConsoleInOut() { }
        public new void Output()
        {
            if (Mode == Args.ArgsType.ENCRYPT) Console.Write(Encoding.Default.GetString(Encrypt(Data, Key)));
            else Console.Write(Encoding.Default.GetString(Decrypt(Data, Key)));
        }
    }
}
