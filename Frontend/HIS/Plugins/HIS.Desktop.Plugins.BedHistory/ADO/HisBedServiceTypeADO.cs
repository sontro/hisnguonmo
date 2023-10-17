using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedHistory.ADO
{
    class HisBedServiceTypeADO : MOS.EFMODEL.DataModels.V_HIS_BED_LOG
    {
        public decimal AMOUNT { get; set; }
        //public string PATIENT_TYPE_CODE { get; set; }
        //public long PATIENT_TYPE_ID { get; set; }
        //public string PATIENT_TYPE_NAME { get; set; }
        public bool? IsExpend { get; set; }
        public bool? IsKHBHYT { get; set; }
        public bool? IsOutKtcFee { get; set; }
        public decimal TotalPrice { get; set; }
        public long? AmmoutNamGhep { get; set; }
        public string BED_SERVICE_TYPE_NAME { get; set; }
        //public long? PRIMARY_PATIENT_TYPE_ID { get; set; }
        //public string PRIMARY_PATIENT_TYPE_NAME { get; set; }
        public long? BILL_PATIENT_TYPE_ID { get; set; }
        public DateTime IntructionTime { get; set; }
        public long? OTHER_PAY_SOURCE_ID { get; set; }
        public bool HasConfigOtherSourcePay { get; set; }
    }
}
