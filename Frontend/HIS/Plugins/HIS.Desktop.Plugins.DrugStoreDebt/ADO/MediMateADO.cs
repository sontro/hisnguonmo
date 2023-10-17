using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DrugStoreDebt.ADO
{
    public class MediMateADO : D_HIS_EXP_MEST_DETAIL_1
    {
        public decimal? TOTAL_PRICE { get; set; }
        public bool IsCheck { get; set; }
        public string EXP_MEST_CODE_PLUS { get; set; }
    }
}
