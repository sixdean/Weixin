using System;
using System.IO;

namespace Weixin.Common
{
    public   class TextLogHelper
    {
        public static void WriteLog(string msg)
        {
            var logFileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Log\\Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            var sr = new StreamWriter(logFileName, true);
            sr.WriteLine(DateTime.Now + ":" + msg);
            sr.Close();
        }
    }
}