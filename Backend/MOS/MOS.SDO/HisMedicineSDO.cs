using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMedicineSDO
    {
        public HIS_MEDICINE HisMedicine { get; set; }
        public bool IsUpdateAll { get; set; }
        public bool IsUpdateUnlock { get; set; }
    }
}
