﻿using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class TakeMedicineBeanByMedicineListResultSDO
    {
        public TakeBeanByMameSDO Request { get; set; }
        public List<HIS_MEDICINE_BEAN> Result { get; set; }
    }
}
