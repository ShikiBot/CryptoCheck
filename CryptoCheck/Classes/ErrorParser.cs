using System;

namespace CryptoCheck.Classes
{
    class ErrorParser: InputParser
    {
        public Exception ArgsConflict { get; }
        public Exception ArgsOoR { get; }
        public Exception ArgsUnknown { get; }
        public Exception ToManyArgs { get; }
        public ErrorParser(string[] args) : base(args)
        {
            if (ArgsList.ContainsKey(ArgsType.HELP) ^
                !((ArgsList.ContainsKey(ArgsType.INPUTFILE) ^ ArgsList.ContainsKey(ArgsType.INPUTCONSOLE)) &
                (ArgsList.ContainsKey(ArgsType.ENCRYPT) ^ ArgsList.ContainsKey(ArgsType.DECRYPT))))            
                ArgsConflict = new Exception("Противоречие или отсутствие аргументов");
            if (Outofrange)
                ArgsOoR = new Exception("Отсутсвие параметров для аргумента или их неявное задание");
            if (UnunknownArg)
                ArgsUnknown = new Exception("Присутсвует неизвестный аргумент");
            foreach (ArgsType x in ArgsList.Keys)
            {
                if (ArgsList[x].Count > 1) ToManyArgs = new Exception("Нерациональное количество одинаковых аргументов");
            }
        }
        ~ErrorParser() { }
    }
}
