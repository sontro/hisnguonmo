
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisImpMestProposeViewFilter : FilterBase
    {
        public List<long> PROPOSE_ROOM_IDs { get; set; }
        public List<long> PROPOSE_DEPARTMENT_IDs { get; set; }
        public List<long> SUPPLIER_IDs { get; set; }

        public string IMP_MEST_PROPOSE_CODE__EXACT { get; set; }
        public long? PROPOSE_ROOM_ID { get; set; }
        public long? PROPOSE_DEPARTMENT_ID { get; set; }
        public long? SUPPLIER_ID { get; set; }

        /// <summary>
        /// Trang thai thanh toan
        /// 1 - Chua thanh toan
        /// 2 - Dang thanh toan
        /// 3 - Da thanh toan
        /// </summary>
        public short? PAY_STATUS { get; set; }

        public HisImpMestProposeViewFilter()
            : base()
        {
        }
    }
}
