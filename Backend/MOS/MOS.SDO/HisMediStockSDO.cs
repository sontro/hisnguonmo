using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisMediStockSDO
    {
        public HIS_MEDI_STOCK HisMediStock { get; set; }
        public HIS_ROOM HisRoom { get; set; }
        public List<HIS_MEDI_STOCK_IMTY> HisMediStockImtys { get; set; }
        public List<HIS_MEDI_STOCK_EXTY> HisMediStockExtys { get; set; }
    }
}
