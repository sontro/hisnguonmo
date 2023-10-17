using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPrepareResultSDO
    {
        public HIS_PREPARE HisPrepare { get; set; }
        public List<HIS_PREPARE_MATY> HisPrepareMatys { get; set; }
        public List<HIS_PREPARE_METY> HisPrepareMetys { get; set; }
    }
}
