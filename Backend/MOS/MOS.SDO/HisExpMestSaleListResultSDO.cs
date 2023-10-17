using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestSaleListResultSDO
    {
        public HIS_TRANSACTION Transaction { get; set; }
        public List<HIS_BILL_GOODS> BillGoods { get; set; }
        public List<HisExpMestSaleResultSDO> ExpMestSdos { get; set; }
    }
}
