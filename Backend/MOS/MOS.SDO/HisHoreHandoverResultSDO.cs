using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisHoreHandoverResultSDO
    {
        public V_HIS_HORE_HANDOVER HoreHandover { get; set; }
        public List<V_HIS_HORE_HOHA> HoreHohas { get; set; }
    }
}
