using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList
{
    public class RequestUri
    {
        public const string TransactionTypeCode__Deposit = "DBCODE.HIS_RS.HIS_TRANSACTION_TYPE.TRANSACTION_TYPE_CODE.DEPOSIT";
        public const string HIS_SESE_DEPO_REPAY_GET = "/api/HisSeseDepoRepay/Get";
        public const string HIS_TRANSACTION_DELETE = "api/HisTransaction/Delete";
    }
}
