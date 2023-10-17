using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPatientForKioskSDO : HisPatientSDO
    {
        public List<V_HIS_PATIENT_TYPE_ALTER> PatientTypeAlters { get; set; }
        public List<V_HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<V_HIS_SERE_SERV> SereServs { get; set; }
        public List<HIS_SERE_SERV_DEPOSIT> SereServDeposits { get; set; }
        public List<HIS_SERE_SERV_BILL> SereServBills { get; set; }
        public List<V_HIS_TRANSACTION> Transactions { get; set; }
        public decimal? Balance { get; set; }
    }
}
