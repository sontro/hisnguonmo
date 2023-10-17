using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMestInventorySDO
    {
        public long MediStockPeriodId { get; set; }
        public List<HIS_MEST_INVE_USER> MestInveUsers { get; set; }
    }
}
