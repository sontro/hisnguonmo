using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class SwapServiceADO
    {
        public V_HIS_SERVICE_REQ serviceReq { get; set; }
        public V_HIS_SERE_SERV currentSereServ { get; set; }
        public DelegateSelectData delegateSwapService;

        public SwapServiceADO(V_HIS_SERVICE_REQ _serviceReq, V_HIS_SERE_SERV _currentSereServ)
        {
            this.serviceReq = _serviceReq;
            this.currentSereServ = _currentSereServ;
        }
    }
}
