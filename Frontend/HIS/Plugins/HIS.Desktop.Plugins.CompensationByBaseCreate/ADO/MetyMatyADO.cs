using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CompensationByBaseCreate.ADO
{
    public class MetyMatyADO
    {
        public long METY_MATY_ID { get; set; }
        public long TYPE { get; set; }
        public string METY_MATY_CODE { get; set; }
        public string METY_MATY_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string CONCENTRA { get; set; }
        public string ACTIVE_INGR_BHYT_CODE { get; set; }
        public string ACTIVE_INGR_BHYT_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal BASE_AMOUNT { get; set; }
        public decimal IN_STOCK_AMOUNT { get; set; }
        public decimal COMPENSATION_AMOUNT { get; set; }
        public bool IsCheck { get; set; }
        public string MEDI_STOCK_NAME { get; set; }
        public decimal? AMOUT_EXP_MEDI_STOCK { get; set; }

    }
}
