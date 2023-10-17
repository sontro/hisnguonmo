using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExportMestMedicine.Base
{
    public class AmountADO
    {
        public decimal Amount { get; set; }
        public decimal? Dd_Amount { get; set; }

        public AmountADO()
        {
        }

        public AmountADO(HIS_EXP_MEST_MATY_REQ data)
        {
            this.Amount = data.AMOUNT;
            this.Dd_Amount = data.DD_AMOUNT;
        }

        public AmountADO(HIS_EXP_MEST_METY_REQ data)
        {
            this.Amount = data.AMOUNT;
            this.Dd_Amount = data.DD_AMOUNT;
        }
    }
}
