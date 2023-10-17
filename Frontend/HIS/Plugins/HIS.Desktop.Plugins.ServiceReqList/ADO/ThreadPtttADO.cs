using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.ADO
{
    class ThreadPtttADO
    {
        public V_HIS_PATIENT patient { get; set; }
        public V_HIS_TREATMENT vhisTreatment { get; set; }
        public V_HIS_DEPARTMENT_TRAN departmentTran { get; set; }
        public V_HIS_SERVICE_REQ serviceReq { get; set; }
        public List<V_HIS_EKIP_USER> ekipUsers { get; set; }
        public V_HIS_SERE_SERV_PTTT sereServPttts { get; set; }
        public V_HIS_SERE_SERV_5 sereServ5Print { get; set; }
        public HIS_SERE_SERV sereServPrint { get; set; }
        public List<HIS_SERE_SERV_FILE> sereServFile { get; set; }

        public ThreadPtttADO() { }

        public ThreadPtttADO(HIS_SERE_SERV data)
        {
            if (data != null)
            {
                this.sereServPrint = data;
            }
        }
    }
}
