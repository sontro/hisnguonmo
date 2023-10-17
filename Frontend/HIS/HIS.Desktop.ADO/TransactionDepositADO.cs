using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TransactionDepositADO
    {
        public MOS.EFMODEL.DataModels.V_HIS_TREATMENT_FEE Treatment { get; set; }
        public long CashierRoomId { get; set; }

        public TransactionDepositADO(MOS.EFMODEL.DataModels.V_HIS_TREATMENT_FEE treatment, long cashierRoomId)
        {
            this.CashierRoomId = cashierRoomId;
            this.Treatment = treatment;
        }
    }
}
