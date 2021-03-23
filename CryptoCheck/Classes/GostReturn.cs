namespace CryptoCheck.Classes
{
    class GostReturn: Gost28147_89
    {
        public byte[] Data { get; set; }
        public byte[] Key { get; }
        public Args.ArgsType Mode { get; }
        public GostReturn(string password, Args.ArgsType mode)
        {
            Key = GetKey(password);
            Mode = mode;
        }
        ~GostReturn() { }
        public void Output()
        {
            
        }
    }
}
