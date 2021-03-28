using System.IO;

namespace CryptoCheck.Classes
{
    class FileStreamer: IObase
    {
        public FileStream StreamIN { get; }
        public FileStream StreamOUT { get; }
        public FileStreamer(string fileIN, string fileOUT, string password, ArgsType mode) : base(password, mode)
        {
            StreamIN = fileIN != "" ? new FileStream(fileIN, FileMode.OpenOrCreate, FileAccess.Read) : null;
            StreamOUT = fileOUT != "" ? new FileStream(fileOUT, FileMode.OpenOrCreate, FileAccess.Write) : null;
        }
        ~FileStreamer()
        {
            if (StreamIN != null)
            {
                StreamIN.Dispose();
                StreamIN.Close();
            }
            if (StreamOUT != null)
            {
                StreamOUT.Dispose();
                StreamOUT.Close();
            }
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
