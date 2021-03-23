using System;

namespace CryptoCheck.Classes
{
    class ErrorParser: InputParser
    {
        public Exception ArgsConflict { get; }
        public Exception ArgsOoR { get; }
        public ErrorParser(string[] args) : base(args)
        {
            if (!(ArgsList.ContainsKey(Args.ArgsType.INPUTCONSOLE) ^ ArgsList.ContainsKey(Args.ArgsType.INPUTFILE)) &
                 !(ArgsList.ContainsKey(Args.ArgsType.ENCRYPT) ^ ArgsList.ContainsKey(Args.ArgsType.DECRYPT)))
                ArgsConflict = new Exception("Противоречие или отсутствие аргументов");
            if (Outofrange)
                ArgsOoR = new Exception("Отсутсвие параметров для аргумента или их неявное задание");
        }
        ~ErrorParser() { }
    }
}
