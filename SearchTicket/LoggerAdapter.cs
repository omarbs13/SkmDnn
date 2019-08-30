using System;
using System.IO;
using System.Reflection;

namespace TsystemsSearchTicket
{
    public class LoggerAdapter
    {
        public void LogError(string message, string type)
        {
            LogWrite(message, type);
        }

        private void LogWrite(string logMessage, string type)
        {
            var m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "logSkm.txt"))
                {
                    Log(logMessage, w, type);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter, string type)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry (" + type + ") : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}