using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    public class RocheHl7AntibioticData
    {
        public string AntibioticCode { get; set; }
        public string AntibioticName { get; set; }
        public string Result { get; set; }
        public string SRI { get; set; } //Luan giai. R: kháng. I: trung gian; S: nhậy
    }

    public class RocheHl7MicroBioticResultRecordData
    {
        public string TestIndexCode { get; set; }
        public string MachineCode { get; set; }

        public string BacteriaCode { get; set; }
        public string BacteriaName { get; set; }
        public string BacteriaNote { get; set; }
        public string BacteriaAmount { get; set; }

        public List<RocheHl7AntibioticData> Antibiotics { get; set; }

        public RocheHl7MicroBioticResultRecordData()
        {
        }
    }
}
