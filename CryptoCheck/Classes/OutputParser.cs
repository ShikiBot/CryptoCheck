using System;

namespace CryptoCheck.Classes
{
    class OutputParser : ErrorParser
    {
        public string Password { get; }
        public string Message { get; }
        public string Filein { get; }
        public string Fileout { get; }
        public ArgsType Mode { get; }
        public OutputParser(string[] args) : base(args)
        {
            Password = ArgsList.ContainsKey(ArgsType.PASSWORD) ? ArgsList[ArgsType.PASSWORD].Parameter : "";
            Message = ArgsList.ContainsKey(ArgsType.INPUTCONSOLE) ? ArgsList[ArgsType.INPUTCONSOLE].Parameter : "";
            Filein = ArgsList.ContainsKey(ArgsType.INPUTFILE) ? ArgsList[ArgsType.INPUTFILE].Parameter : "";
            Fileout = ArgsList.ContainsKey(ArgsType.OUTPUTFILE) ? ArgsList[ArgsType.OUTPUTFILE].Parameter : "";
            Mode = ArgsList.ContainsKey(ArgsType.DECRYPT) ?
                ArgsList[ArgsType.DECRYPT].Arg == ArgsType.DECRYPT ?
                ArgsType.DECRYPT : ArgsType.ENCRYPT : ArgsType.ENCRYPT;
        }
        ~OutputParser() { }
        public Object OutputClass()
        {
            if (ArgsConflict == null & ArgsOoR == null & ToManyArgs == null & ArgsUnknown == null)
            {
                if (ArgsList.ContainsKey(ArgsType.HELP)) return new HelpInfo();
                else if (ArgsList.ContainsKey(ArgsType.INPUTCONSOLE) & ArgsList.ContainsKey(ArgsType.OUTPUTCONSOLE))
                    return new ConsoleInOut(Message, Password, Mode);
                else if (ArgsList.ContainsKey(ArgsType.INPUTCONSOLE) & ArgsList.ContainsKey(ArgsType.OUTPUTFILE))
                    return new ConsoleInFileOut(Message, Fileout, Password, Mode);
                else if (ArgsList.ContainsKey(ArgsType.INPUTFILE) & ArgsList.ContainsKey(ArgsType.OUTPUTCONSOLE))
                    return new FileInConsoleOut(Filein, Password, Mode);
                else if (ArgsList.ContainsKey(ArgsType.INPUTFILE) & ArgsList.ContainsKey(ArgsType.OUTPUTFILE))
                    return new FileInOut(Filein, Fileout, Password, Mode);
            }
            else if (ArgsConflict != null) return ArgsConflict;
            else if (ArgsOoR != null) return ArgsOoR;
            else if (ToManyArgs != null) return ToManyArgs;            
            return ArgsUnknown;
        }
    }
}
