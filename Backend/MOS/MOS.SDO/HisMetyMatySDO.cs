﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMetyMatySDO
    {
        public long MedicineTypeId { get; set; }
        public List<HisMatyAmoutSDO> MaterialTypeSDOs { get; set; }
    }
}
