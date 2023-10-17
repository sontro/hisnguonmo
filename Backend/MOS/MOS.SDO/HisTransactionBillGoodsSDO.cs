using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionBillGoodsSDO
    {
        public List<long> ExpMestIds { get; set; }
        public HIS_TRANSACTION HisTransaction { get; set; }
        public List<HIS_BILL_GOODS> HisBillGoods { get; set; }
    }
}
