using HIS.Desktop.LocalStorage.BackendData.ADO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    public class ServiceReq1ADOWorker
    {
        private static ServiceReq1ADO serviceReq1ADO;

        public static ServiceReq1ADO ServiceReq1ADO
        {
            get
            {
                if (serviceReq1ADO == null)
                {
                    serviceReq1ADO = new ServiceReq1ADO();
                }
                lock (serviceReq1ADO) ;
                return serviceReq1ADO;
            }
            set
            {
                lock (serviceReq1ADO) ;
                serviceReq1ADO = value;
            }
        }
    }
}
