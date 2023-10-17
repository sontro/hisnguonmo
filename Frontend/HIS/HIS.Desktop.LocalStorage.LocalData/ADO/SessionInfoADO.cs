using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.Global.ADO
{
    public class SessionInfoADO
    {
        public V_HIS_ACCOUNT_BOOK DepositAccountBook { get; set; }
        public HIS_PAY_FORM PayForm { get; set; }
        public long? CurrentDepositNumOrder { get; set; }
        public long? NextDepositNumOrder { get; set; }
        public long? CashierWorkingRoomId { get; set; }
    }
}
