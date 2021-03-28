using System.Collections.Generic;

namespace CryptoCheck.Classes
{
    class InputParser
    {
        private bool outofrange;
        public bool Outofrange { get { return outofrange; } }
        private bool ununknownArg;
        public bool UnunknownArg { get { return ununknownArg; } }
        readonly private Dictionary<ArgsType, Args> argsList;        
        public Dictionary<ArgsType, Args> ArgsList { get { return argsList; } }
        public InputParser(string [] args)
        {
            argsList = new Dictionary<ArgsType, Args>();
            Parse(args);
            if (!argsList.ContainsKey(ArgsType.OUTPUTFILE)) argsList.Add(ArgsType.OUTPUTCONSOLE, new Args(ArgsType.OUTPUTCONSOLE));
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
                        if (!argsList.ContainsKey(ArgsType.HELP))
                            argsList.Add(ArgsType.HELP, new Args(ArgsType.HELP));
                        else argsList[ArgsType.HELP].AddCount();
                    else if (args[i].ToLower() == "-e")
                        if (!argsList.ContainsKey(ArgsType.ENCRYPT))
                            argsList.Add(ArgsType.ENCRYPT, new Args(ArgsType.ENCRYPT));
                        else argsList[ArgsType.ENCRYPT].AddCount();
                    else if (args[i].ToLower() == "-d")
                        if (!argsList.ContainsKey(ArgsType.DECRYPT))
                            argsList.Add(ArgsType.DECRYPT, new Args(ArgsType.DECRYPT));
                        else argsList[ArgsType.DECRYPT].AddCount();
                    else if (args[i].ToLower() == "-t" | args[i].ToLower() == "--text")
                        if (!argsList.ContainsKey(ArgsType.INPUTCONSOLE))
                        {
                            k = args[i + 1][0] == '\\' ? args[i + 1].Remove(0, 1) : args[i + 1];
                            argsList.Add(ArgsType.INPUTCONSOLE, new Args(ArgsType.INPUTCONSOLE, k));
                        }
                        else argsList[ArgsType.INPUTCONSOLE].AddCount();
                    else if (args[i].ToLower() == "-in")
                        if (!argsList.ContainsKey(ArgsType.INPUTFILE))
                            argsList.Add(ArgsType.INPUTFILE, new Args(ArgsType.INPUTFILE, args[i + 1]));
                        else argsList[ArgsType.INPUTFILE].AddCount();
                    else if (args[i].ToLower() == "-out")
                        if (!argsList.ContainsKey(ArgsType.OUTPUTFILE))
                            argsList.Add(ArgsType.OUTPUTFILE, new Args(ArgsType.OUTPUTFILE, args[i + 1]));
                        else argsList[ArgsType.OUTPUTFILE].AddCount();
                    else if (args[i].ToLower() == "-p" | args[i].ToLower() == "--password")
                        if (!argsList.ContainsKey(ArgsType.PASSWORD))
                        {
                            k = args[i + 1][0] == '\\' ? args[i + 1].Remove(0, 1) : args[i + 1];
                            argsList.Add(ArgsType.PASSWORD, new Args(ArgsType.PASSWORD, k));
                        }
                        else argsList[ArgsType.PASSWORD].AddCount();
                    else if (args[i][0] == '-') ununknownArg = true; 
                }
                catch { outofrange = true; }
            }
        }
    }
}
