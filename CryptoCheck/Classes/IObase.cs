namespace CryptoCheck.Classes
{
    class IObase
    {
        public byte[] Data { get; set; }
        public byte[] Key { get; }
        public ArgsType Mode { get; }
        public IObase(string password, ArgsType mode)
        {
            Key = Gost28147_89.GetKey(password);
            Mode = mode;
        }
        ~IObase() { }
        public void Output()
        {
            
        }
    }
}
