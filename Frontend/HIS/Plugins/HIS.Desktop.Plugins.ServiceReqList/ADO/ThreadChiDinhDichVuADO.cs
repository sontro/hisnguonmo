using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.ADO
{
    class ThreadChiDinhDichVuADO
    {
        public HIS_TREATMENT hisTreatment { get; set; }
        public List<V_HIS_SERE_SERV> listVHisSereServ { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER vHisPatientTypeAlter { get; set; }
        public ADO.ServiceReqADO vHisServiceReq2Print { get; set; }

        public List<HIS_SERE_SERV_DEPOSIT> ListSereServDeposit { get; set; }
        public List<HIS_SERE_SERV_BILL> ListSereServBill { get; set; }

        public ThreadChiDinhDichVuADO() { }

        public ThreadChiDinhDichVuADO(ADO.ServiceReqADO data)
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
