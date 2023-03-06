using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace GymHome
{
    public static class Logger
    {
        static StreamWriter writer = null;
        public static void Init(string filePath)
        {
            if(!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            writer = new StreamWriter(filePath);
        }

        public static void Close() 
        {
            if(writer != null) 
                writer.Close();

            writer = null;
        }

        public static void Log(string message) 
        {
            LogMessage($"- Log: {message}");
        }

        public static void Error(string message)
        {
            LogMessage($"- Error: {message}");
        }

        public static void Info(string message) 
        {
            LogMessage($"- Info: {message}");
        }

        public static void Warning(string message)
        {
            LogMessage($"- Warning: {message}");
        }

        private static void LogMessage(string message) 
        {
            if (writer == null)
                return;

            writer.WriteLine($"[{DateTime.Now}] {message}");
            writer.Flush();
        }
    }
}
