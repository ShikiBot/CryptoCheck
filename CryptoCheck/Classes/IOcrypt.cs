namespace CryptoCheck.Classes
{
    class IOcrypt: IObase
    {
        public byte[] Data { get; set; }
        public byte[] Key { get; }
        public ArgsType Mode { get; }
        public IOcrypt(string password, ArgsType mode)
        {
            Key = Gost28147_89.GetKey(password);
            Mode = mode;
        }
        ~IOcrypt() { }        
    }
}
