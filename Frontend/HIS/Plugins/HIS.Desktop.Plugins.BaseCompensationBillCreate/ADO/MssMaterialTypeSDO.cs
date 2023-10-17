using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ADO
{
    public class MssMaterialTypeSDO : MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE
    {
        public long? MATERIAL_ID { get; set; }
        public double IdRow { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal? PRICE { get; set; }
        public decimal? VAT { get; set; }
    }
}
