using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisDrugStoreDebtResultSDO
    {
        public V_HIS_TRANSACTION Debt { get; set; }
        public List<HIS_DEBT_GOODS> DebtGoods { get; set; }
    }
}
