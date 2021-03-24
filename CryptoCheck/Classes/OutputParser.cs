using System;

namespace CryptoCheck.Classes
{
    class OutputParser : ErrorParser
    {
        public string Password { get; }
        public string Message { get; }
        public string Filein { get; }
        public string Fileout { get; }
        public Args.ArgsType Mode { get; }
        public OutputParser(string[] args) : base(args)
        {
            Password = ArgsList.ContainsKey(Args.ArgsType.PASSWORD) ? ArgsList[Args.ArgsType.PASSWORD].Parameter : "";
            Message = ArgsList.ContainsKey(Args.ArgsType.INPUTCONSOLE) ? ArgsList[Args.ArgsType.INPUTCONSOLE].Parameter : "";
            Filein = ArgsList.ContainsKey(Args.ArgsType.INPUTFILE) ? ArgsList[Args.ArgsType.INPUTFILE].Parameter : "";
            Fileout = ArgsList.ContainsKey(Args.ArgsType.OUTPUTFILE) ? ArgsList[Args.ArgsType.OUTPUTFILE].Parameter : "";
            Mode = ArgsList.ContainsKey(Args.ArgsType.DECRYPT) ?
                ArgsList[Args.ArgsType.DECRYPT].Arg == Args.ArgsType.DECRYPT ?
                Args.ArgsType.DECRYPT : Args.ArgsType.ENCRYPT : Args.ArgsType.ENCRYPT;
        }
        ~OutputParser() { }
        public Object OutputClass()
        {
            if (ArgsConflict == null & ArgsOoR == null & ToManyArgs == null & ArgsUnknown == null)
            {
                if (ArgsList.ContainsKey(Args.ArgsType.HELP))
                {
                    return new HelpInfo();
                }
                else if (ArgsList.ContainsKey(Args.ArgsType.INPUTCONSOLE) & ArgsList.ContainsKey(Args.ArgsType.OUTPUTCONSOLE))
                {
                    return new ConsoleInOut(Message, Password, Mode);
                }
                else if (ArgsList.ContainsKey(Args.ArgsType.INPUTCONSOLE) & ArgsList.ContainsKey(Args.ArgsType.OUTPUTFILE))
                {
                    return new ConsoleInFileOut(Message, Fileout, Password, Mode);
                }
                else if (ArgsList.ContainsKey(Args.ArgsType.INPUTFILE) & ArgsList.ContainsKey(Args.ArgsType.OUTPUTCONSOLE))
                {
                    return new FileInConsoleOut(Filein, Password, Mode);
                }
                else if (ArgsList.ContainsKey(Args.ArgsType.INPUTFILE) & ArgsList.ContainsKey(Args.ArgsType.OUTPUTFILE))
                {
                    return new FileInFileOut(Filein, Fileout, Password, Mode);
                }
            }
            else if (ArgsConflict != null)
            {
                return ArgsConflict;
            }
            else if (ToManyArgs != null)
            {
                return ToManyArgs;
            }
            else if (ArgsUnknown != null)
            {
                return ArgsUnknown;
            }
            else 
            {
                return ArgsOoR;
            }
            return new object();
        }
    }
}
