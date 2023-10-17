using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ADO
{
    public class MssExpMestMediMateForPrintSDO : MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_MEDICINE
    {
        public string EXPIRED_DATE_STR { get; set; }
        public string EXP_TIME_STR { get; set; }
        public decimal VAT_RATIO_100 { get; set; }
        public decimal? IMP_VAT_RATIO_100 { get; set; }
        public string MATERIAL_TYPE_CODE { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
        public string MEDI_MATE_TYPE_CODE { get; set; }
        public string MEDI_MATE_TYPE_NAME { get; set; }
        public decimal SUM_TOTAL_IMP_PRICE { get; set; }
        public decimal SUM_TOTAL_PRICE { get; set; }
    }
}
