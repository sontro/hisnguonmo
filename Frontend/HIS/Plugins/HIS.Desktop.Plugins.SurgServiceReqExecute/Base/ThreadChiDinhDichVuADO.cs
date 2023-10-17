using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
    class ThreadChiDinhDichVuADO
    {
        public HIS_TREATMENT hisTreatment { get; set; }
        public List<V_HIS_SERE_SERV> listVHisSereServ { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER vHisPatientTypeAlter { get; set; }
        public V_HIS_SERVICE_REQ vHisServiceReq2Print { get; set; }

        public List<HIS_SERE_SERV_DEPOSIT> ListSereServDeposit { get; set; }
        public List<HIS_SERE_SERV_BILL> ListSereServBill { get; set; }

        public ThreadChiDinhDichVuADO() { }

        public ThreadChiDinhDichVuADO(V_HIS_SERVICE_REQ data)
        {
            try
            {
                if (data != null)
                {
                    this.vHisServiceReq2Print = data;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
