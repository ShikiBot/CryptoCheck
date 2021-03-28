using CryptoCheck.Classes;

namespace CryptoCheck
{
    class Args
    {   
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