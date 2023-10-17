using MOS.EFMODEL.DataModels;
//using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccountBookListImport.ADO
{
    class HisAccountBookListImportADO : HIS_ACCOUNT_BOOK
    {
        public string ROOM_CODE { get; set; }
        public long ROOM_TYPE_ID { get; set; }
        //public string ROOM_TYPE_CODE { get; set; }
        public string ERROR { get; set; }
        public decimal? TOTAL_STR { get; set; }
        public decimal? TOTAL_STR_ { get; set; }
        public decimal? FROM_NUM_ORDER_STR { get; set; }
        public decimal? FROM_NUM_ORDER_STR_ { get; set; }
        public string RELEASE_TIME_STR { get; set; }
        public string WORKING_SHIFT_ID_STR { get; set; }
        public string EINVOICE_TYPE_ID_STR { get; set; }
        public string WORKING_SHIFT_ID_STR_ { get; set; }
        public string EINVOICE_TYPE_ID_STR_ { get; set; }

        public string IS_NOT_GEN_TRANSACTION_ORDER_STR { get; set; }
        public string IS_FOR_BILL_STR { get; set; }
        public string IS_FOR_DEPOSIT_STR { get; set; }
        public string IS_FOR_DEBT_STR { get; set; }
        public string IS_FOR_OTHER_SALE_STR { get; set; }
        public string IS_FOR_REPAY_STR { get; set; }
    }
}
