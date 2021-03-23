using System.IO;

namespace CryptoCheck.Classes
{
    class FileStreamer: GostReturn
    {
        public FileStream StreamIN { get; }
        public FileStream StreamOUT { get; }
        public FileStreamer(string fileIN, string fileOUT, string password, Args.ArgsType mode) : base(password, mode)
        {
            StreamIN = new FileStream(fileIN, FileMode.OpenOrCreate, FileAccess.Read);
            StreamOUT = fileOUT != "" ? new FileStream(fileOUT, FileMode.OpenOrCreate, FileAccess.Write) : null;
        }
        ~FileStreamer()
        {
            StreamIN.Dispose();
            StreamIN.Close();
            StreamOUT.Dispose();
            StreamOUT.Close();
        }
        public byte[] FileRead()
        {
            int len = (int)StreamIN.Length;
            byte[] message = new byte[len];
            StreamIN.Read(message, 0, len);
            return message;
        }
        public void FileWrite(byte[] message)
        {
            StreamOUT.Write(message, 0, message.Length);
        }
    }
}
