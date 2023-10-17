using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Transaction.ADO
{
    internal class PopupItemStatusAdo
    {
        internal bool TemporaryLockStt { get; set; }
        internal bool LockStt { get; set; }
        internal bool TransactionAllStt { get; set; }

        internal bool TransactionDepositStt { get; set; }
    }
}
