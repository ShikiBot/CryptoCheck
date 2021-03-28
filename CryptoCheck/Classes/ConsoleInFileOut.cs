namespace CryptoCheck.Classes
{
    class ConsoleInFileOut: FileStreamer
    {
        public ConsoleInFileOut(string message, string file, string password, ArgsType mode) : base("", file, password, mode)
        {
            Data = System.Text.Encoding.Default.GetBytes(message);
        }
        ~ConsoleInFileOut() { }
        public new void Output()
        {
            if (Mode == ArgsType.ENCRYPT) FileWrite(Gost28147_89.Encrypt(Data, Key));
            else FileWrite(Gost28147_89.Decrypt(Data, Key));
        }
    }
}
