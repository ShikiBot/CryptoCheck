namespace CryptoCheck.Classes
{
    class FileInFileOut: FileStreamer
    {
        public FileInFileOut(string fileIN, string fileOUT, string password, Args.ArgsType mode) : base(fileIN, fileOUT, password, mode)
        {
            Data = FileRead();
        }
        ~FileInFileOut() { }

        public new void Output()
        {
            if (Mode == Args.ArgsType.ENCRYPT) FileWrite(Encrypt(Data, Key));
            else FileWrite(Decrypt(Data, Key));
        }
    }
}
