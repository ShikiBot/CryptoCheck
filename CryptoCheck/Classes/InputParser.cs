using System.Collections.Generic;

namespace CryptoCheck.Classes
{
    class InputParser
    {
        private bool outofrange;
        public bool Outofrange { get { return outofrange; } }
        private bool ununknownArg;
        public bool UnunknownArg { get { return ununknownArg; } }
        readonly private Dictionary<Args.ArgsType, Args> argsList;        
        public Dictionary<Args.ArgsType, Args> ArgsList { get { return argsList; } }
        public InputParser(string [] args)
        {
            argsList = new Dictionary<Args.ArgsType, Args>();
            Parse(args);
            if (!argsList.ContainsKey(Args.ArgsType.OUTPUTFILE)) argsList.Add(Args.ArgsType.OUTPUTCONSOLE, new Args(Args.ArgsType.OUTPUTCONSOLE));
        }
        ~InputParser(){ }

        private void Parse(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    string k = "";
                    if (args[i].ToLower() == "-h" | args[i].ToLower() == "--help")
                        if (!argsList.ContainsKey(Args.ArgsType.HELP))
                            argsList.Add(Args.ArgsType.HELP, new Args(Args.ArgsType.HELP));
                        else argsList[Args.ArgsType.HELP].AddCount();
                    else if (args[i].ToLower() == "-e")
                        if (!argsList.ContainsKey(Args.ArgsType.ENCRYPT))
                            argsList.Add(Args.ArgsType.ENCRYPT, new Args(Args.ArgsType.ENCRYPT));
                        else argsList[Args.ArgsType.ENCRYPT].AddCount();
                    else if (args[i].ToLower() == "-d")
                        if (!argsList.ContainsKey(Args.ArgsType.DECRYPT))
                            argsList.Add(Args.ArgsType.DECRYPT, new Args(Args.ArgsType.DECRYPT));
                        else argsList[Args.ArgsType.DECRYPT].AddCount();
                    else if (args[i].ToLower() == "-t" | args[i].ToLower() == "--text")
                        if (!argsList.ContainsKey(Args.ArgsType.INPUTCONSOLE))
                        {
                            k = args[i + 1][0] == '\\' ? args[i + 1].Remove(0, 1) : args[i + 1];
                            argsList.Add(Args.ArgsType.INPUTCONSOLE, new Args(Args.ArgsType.INPUTCONSOLE, k));
                        }
                        else argsList[Args.ArgsType.INPUTCONSOLE].AddCount();
                    else if (args[i].ToLower() == "-in")
                        if (!argsList.ContainsKey(Args.ArgsType.INPUTFILE))
                            argsList.Add(Args.ArgsType.INPUTFILE, new Args(Args.ArgsType.INPUTFILE, args[i + 1]));
                        else argsList[Args.ArgsType.INPUTFILE].AddCount();
                    else if (args[i].ToLower() == "-out")
                        if (!argsList.ContainsKey(Args.ArgsType.OUTPUTFILE))
                            argsList.Add(Args.ArgsType.OUTPUTFILE, new Args(Args.ArgsType.OUTPUTFILE, args[i + 1]));
                        else argsList[Args.ArgsType.OUTPUTFILE].AddCount();
                    else if (args[i].ToLower() == "-p" | args[i].ToLower() == "--password")
                        if (!argsList.ContainsKey(Args.ArgsType.PASSWORD))
                        {
                            k = args[i + 1][0] == '\\' ? args[i + 1].Remove(0, 1) : args[i + 1];
                            argsList.Add(Args.ArgsType.PASSWORD, new Args(Args.ArgsType.PASSWORD, k));
                        }
                        else argsList[Args.ArgsType.PASSWORD].AddCount();
                    else if (args[i][0] == '-') ununknownArg = true; 
                }
                catch { outofrange = true; }
            }
        }
    }
}
