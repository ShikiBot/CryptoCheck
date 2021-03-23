namespace CryptoCheck.Classes
{
    class ConsoleInFileOut: FileStreamer
    {
        public ConsoleInFileOut(string message, string file, string password, Args.ArgsType mode) : base("", file, password, mode)
        {
            Data = System.Text.Encoding.Default.GetBytes(message);
        }
        ~ConsoleInFileOut() { }
        public new void Output()
        {
            if (Mode == Args.ArgsType.ENCRYPT) FileWrite(Encrypt(Data, Key));
            else FileWrite(Decrypt(Data, Key));
        }
    }
}
