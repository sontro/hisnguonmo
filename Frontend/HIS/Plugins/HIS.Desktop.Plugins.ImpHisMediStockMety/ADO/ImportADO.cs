using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpHisMediStockMety.ADO
{
    class ImportADO : MOS.EFMODEL.DataModels.HIS_MEDI_STOCK_METY
    {

        public string MEDI_STOCK_CODE { get; set; }
        public string MEDI_STOCK_NAME { get; set; }
        public string ERROR { get; set; }
        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
        public string EXP_MEDI_STOCK_CODE { get; set; }
        public string EXP_MEDI_STOCK_NAME { get; set; }
        public string IS_PREVENT_MAX_STR { get; set; }
        public string IS_PREVENT_EXP_STR { get; set; }
        public string IS_GOODS_RESTRICT_STR { get; set; }


        public string ErrorDesc { get; set; }
    }
}
