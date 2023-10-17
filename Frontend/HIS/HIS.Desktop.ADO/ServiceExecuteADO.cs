using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ServiceExecuteADO
    {
        public DelegateRefresh RefreshData { get; set; }
        public V_HIS_SERVICE_REQ ServiceReq { get; set; }
        public bool? IsExecuter { get; set; }
        public bool? IsReadResult { get; set; }

        public ServiceExecuteADO(V_HIS_SERVICE_REQ ServiceReq, DelegateRefresh RefreshData)
        {
            this.ServiceReq = ServiceReq;
            this.RefreshData = RefreshData;
        }
    }
}
