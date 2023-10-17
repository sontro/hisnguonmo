using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAntibioticRequestResultSDO
    {
        public V_HIS_ANTIBIOTIC_REQUEST AntibioticRequest { get; set; }
        public List<HIS_ANTIBIOTIC_MICROBI> AntibioticMicrobis { get; set; }
        public List<V_HIS_ANTIBIOTIC_NEW_REG> AntibioticNewRegs { get; set; }
        public List<HIS_ANTIBIOTIC_OLD_REG> AntibioticOldRegs { get; set; }
    }
}
