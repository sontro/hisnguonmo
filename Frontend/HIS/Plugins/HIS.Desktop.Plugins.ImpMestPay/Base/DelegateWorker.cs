using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestPay.Base
{
    public class DelegateWorker
    {
        public delegate bool UpdatePatientType(MOS.EFMODEL.DataModels.HIS_SERE_SERV hisSereServ,ref CommonParam param);
    }
}
