using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniFinance.Common
{
    public class LogManager
    {
        private static bool isWriteLog = true;
        private static string logPath = null;

        public static bool IsWriteLog
        {
            get { return isWriteLog; }
            set { isWriteLog = value; }
        }

        public static string LogPath
        {
            get { return logPath; }
            set { logPath = value; }
        }

        public static string GetTrackId()
        {
            try
            {
                return String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void WriteLog(string text)
        {
            try
            {
                if (isWriteLog)
                {
                    text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ") + text + Environment.NewLine;
                    System.IO.File.AppendAllText(logPath + "Log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt", text);
                }
            }
            catch
            {

            }

        }

        public static void WriteLog(string text, string logPath)
        {
            try
            {
                if (isWriteLog)
                {
                    text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ") + text + Environment.NewLine;
                    System.IO.File.AppendAllText(logPath + "Log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt", text);
                }
            }
            catch
            {

            }

        }

        public static void WriteException(Exception ex)
        {
            try
            {
                WriteLog(ex.Message);
                WriteLog(ex.StackTrace);
            }
            catch
            {

            }

        }
    }
}
