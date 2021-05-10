namespace CryptoCheck.Classes
{
    class FileInOut: FileStreamer
    {
        public FileInOut(string fileIN, string fileOUT, string password, ArgsType mode) : base(fileIN, fileOUT, password, mode)
        {
            Data = FileRead();
        }
        ~FileInOut() { }

        public override void Output()
        {
            if (Mode == ArgsType.ENCRYPT) FileWrite(Gost28147_89.Encrypt(Data, Key));
            else FileWrite(Gost28147_89.Decrypt(Data, Key));
        }
    }
}
