using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisDispenseResultSDO
    {
        public HIS_DISPENSE HisDispense { get; set; }
        public HIS_EXP_MEST HisExpMest { get; set; }
        public HIS_IMP_MEST HisImpMest { get; set; }
    }
}
