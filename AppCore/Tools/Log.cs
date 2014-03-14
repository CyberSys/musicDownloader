using AppCore.AppTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Tools
{
    /// <summary>
    /// Provide export of logs in txt file.
    /// </summary>
    internal static class Log
    {
        private static volatile object logAccesSync = new object();

        /// <summary>
        /// Writes an entry in log file.
        /// </summary>
        /// <param name="message">Entry message.</param>
        /// <param name="errorCode">Entry error code or info codes.</param>
        /// <param name="stackTrace">Stacktrace (if it's exists)</param>
        internal static void WriteLog(String message, ErrorCodes errorCode = ErrorCodes.GeneralException, String stackTrace = "")
        {
            lock (logAccesSync)
            {
                try
                {
                    using(var file = File.Open("log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        file.Seek(0, SeekOrigin.End);
                        message = String.Format("[{0}]\r\n     Code - 0x{2}\r\n     Message - {1}\r\n", DateTime.UtcNow.AddHours(2).ToString(), message, ((Int32)errorCode).ToString("X4"));
                        if(!String.IsNullOrEmpty(stackTrace))
                        {
                            message += String.Format("     Stacktrace - {0}\r\n", stackTrace);
                        }
                        var bytes = System.Text.Encoding.UTF8.GetBytes(message);
                        file.Write(bytes, 0, bytes.Length);
                    }
                }
                catch
                {
                }
            }
        }
    }
}
