using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication_Shared_Services.Contracts
{
    public interface ILogHeaders
    {
        void WriteLog(string msg);
        void WriteLogComplete();
        void setFileLog(string filepath);

    }
}
