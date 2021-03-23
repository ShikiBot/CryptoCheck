namespace CryptoCheck
{
    struct Args
    {
        public enum ArgsType
        {
            HELP,
            INPUTCONSOLE,
            INPUTFILE,
            OUTPUTCONSOLE,
            OUTPUTFILE,
            PASSWORD,
            ENCRYPT,
            DECRYPT
        }

        private int count;
        public ArgsType Arg { get; }
        public int Count { get { return count; } }
        public string Parameter { get; }

        public Args(ArgsType arg, string parameter)
        {
            Arg = arg;
            count = 1;
            Parameter = parameter;
        }

        public Args(ArgsType arg)
        {
            Arg = arg;
            count = 1;
            Parameter = null;
        }

        public void AddCount()
        {
            count++;
        }
    }
}