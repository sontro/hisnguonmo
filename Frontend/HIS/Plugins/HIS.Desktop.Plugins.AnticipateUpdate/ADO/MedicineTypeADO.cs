using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AnticipateUpdate.ADO
{
    public class MedicineTypeADO : MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE
    {
        public double IdRow { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? AllowAmount { get; set; }
        public long Type { get; set; }
        public long? SUPPLIER_ID { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string BID_NUM_ORDER { get; set; }
        public string IS_MEDICINE { get; set; }
        public long? BID_ID { get; set; }
        public bool isDelete { get; set; }
        public bool isAmount { get; set; }

        public MedicineTypeADO() { }
    }
}
