using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult
{
    public class DebugLog
    {
        public static string log
        {
            get
            {
                return "LogFile";
            }
        }
    }
    public class Logger
    {
        public static string LogFilename
        {
            get
            {
                string str = DateTime.Now.Day.ToString("D2");
                string str1 = DateTime.Now.Month.ToString("D2");
                int year = DateTime.Now.Year;
                string str2 = string.Format("{0}-{1}-{2}", str, str1, year.ToString());
                return string.Format("{0}\\Logs{1}.log", AppDomain.CurrentDomain.BaseDirectory + @"\App_Data", str2);
            }
        }
        public static bool writeErrorLog(string text, bool overwrite = false)
        {
            string strObject = Environment.NewLine + "[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "]: " + text;

            bool flag;

            try
            {
                if (!Directory.Exists((new FileInfo(LogFilename)).DirectoryName))
                {
                    Directory.CreateDirectory((new FileInfo(LogFilename)).DirectoryName);
                }
                lock (DebugLog.log)
                {
                    if (!overwrite)
                    {
                        File.AppendAllText(LogFilename, strObject);
                    }
                    else
                    {
                        File.WriteAllText(LogFilename, strObject);
                    }

                    Console.WriteLine(strObject);

                    flag = true;
                }
            }
            catch (Exception exception)
            {
                flag = false;
            }

            return flag;
        }
    }
}
