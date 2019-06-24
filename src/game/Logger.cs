using System;
using System.IO;

namespace csrogue
{
    public static class Logger
    {
        private static StreamWriter log;

        public static IDisposable Open()
        {
            log = new StreamWriter("log.txt");

            return new LogDisposer();
        }

        public static void Close()
        {
            if (log != null)
            {
                log.Close();
                log = null;
            }
        }

        public static void WriteLine(string format, params object[] args)
        {
            if (log == null)
            {
                throw new InvalidOperationException("Attempt to write to the log before opening the log.");
            }

            string message = string.Format(format, args);

            log.WriteLine(message);
        }

        private class LogDisposer : IDisposable
        {
            public void Dispose()
            {
                Logger.Close();
            }
        }
    }
}
