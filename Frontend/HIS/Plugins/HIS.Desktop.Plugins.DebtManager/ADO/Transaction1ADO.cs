using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebtManager.ADO
{
    public class Transaction1ADO : MOS.EFMODEL.DataModels.V_HIS_TRANSACTION_1
    {
        public bool IsCheck { get; set; }
    }
}
