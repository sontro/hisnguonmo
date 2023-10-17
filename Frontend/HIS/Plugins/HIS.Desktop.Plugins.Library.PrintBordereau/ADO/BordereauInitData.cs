using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintBordereau.ADO
{
    public class BordereauInitData
    {
        public List<HIS_SERE_SERV> SereServs { get; set; }
        public List<V_HIS_DEPARTMENT_TRAN> DepartmentTrans { get; set; }
        public List<V_HIS_TREATMENT_FEE> TreatmentFees { get; set; }
        public V_HIS_TREATMENT Treatment { get; set; }
        public V_HIS_PATIENT Patient { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        public List<HIS_SERE_SERV_DEPOSIT> SereServDeposits { get; set; }
        public List<HIS_SESE_DEPO_REPAY> SeseDepoRepays { get; set; }
        public string UserNameReturnResult { get; set; }
        public long? CurrentDepartmentId { get; set; }
        public long RoomId { get; set; }
        public long RoomTypeId { get; set; }
    }
}
