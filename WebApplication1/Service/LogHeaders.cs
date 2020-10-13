using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Service
{
    public interface ILogHeaders
    {
        void WriteLog(string msg);
        void WriteLogComplete();
        void setFileLog(string filepath);

    }
    public  class LogHeaders: ILogHeaders
    {
        static string fileName = @"F:\Partha\Code\Emp\EmployeeWebAPI\WebApplication1\logs\logHeader_Each_Request_" + DateTime.Now.ToString("dd_MM_yy") + ".txt";
      
        public LogHeaders()
        {
            //Check if the file exists
            
        }
        public LogHeaders(string filepath)
        {
            fileName = filepath;
        }
        public void setFileLog(string filepath)
        {
            fileName = filepath;
        }
        public void WriteLog(string msg)
        {
            if (!File.Exists(fileName))
            {
                // Create the file and use streamWriter to write text to it.
                //If the file existence is not check, this will overwrite said file.
                //Use the using block so the file can close and vairable disposed correctly
                using (StreamWriter sw = File.CreateText(fileName))
                {//Write a line of text
                    sw.WriteLine(msg);
                }

            }
            else
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(msg);
                }
            }
        }

        public void WriteLogComplete()
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }
        }
    }
}
