using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ListPresBloodConfirm.Base
{
    public class HisCommonImpMestTypeInfo
    {
        // thong tin chung cho cac loai nhap
        public string DELIVERER { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal? DISCOUNT { get; set; }
        public long? BID_ID { get; set; }
        public decimal? DISCOUNT_RATIO { get; set; }
        public long? DOCUMENT_DATE { get; set; }
        public string DOCUMENT_NUMBER { get; set; }
        public long ID { get; set; }
        public long IMP_MEST_ID { get; set; }
        public long SUPPLIER_ID { get; set; }
    }
}
