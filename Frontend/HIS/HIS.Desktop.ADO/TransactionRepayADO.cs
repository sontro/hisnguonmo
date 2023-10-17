using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TransactionRepayADO
    {
        public long TreatmentId { get; set; }
        public long CashierRoomId { get; set; }

        public V_HIS_TREATMENT_FEE Treatment { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }

        public TransactionRepayADO(long treatmentId, long cashierRoomId)
        {
            this.CashierRoomId = cashierRoomId;
            this.TreatmentId = treatmentId;
        }
    }
}
