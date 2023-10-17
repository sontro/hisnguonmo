using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpHisMediStockMaty.ADO
{
    class ImportADO : MOS.EFMODEL.DataModels.HIS_MEDI_STOCK_MATY
    {
        public string MEDI_STOCK_CODE { get; set; }
        public string MEDI_STOCK_NAME { get; set; }
        public string ERROR { get; set; }
        public string MATERIAL_TYPE_CODE { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
        public string EXP_MEDI_STOCK_CODE { get; set; }
        public string EXP_MEDI_STOCK_NAME { get; set; }
        public string IS_PREVENT_MAX_STR { get; set; }
        public string IS_GOODS_RESTRICT_STR { get; set; }


        public string ErrorDesc { get; set; }
    }
}
