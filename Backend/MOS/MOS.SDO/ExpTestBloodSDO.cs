using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpTestBloodSDO
    {
        public long ExpMestBloodId { get; set; }
        public string PatientBloodAboCode { get; set; }
        public string PatientBloodRhCode { get; set; }
        public string Puc { get; set; }
        public string ScangelGelcard { get; set; }
        public string Coombs { get; set; }

        public string TestTube { get; set; }
        public long? SaltEnvironment { get; set; }
        public long? AntiGlobulinEnvironment { get; set; }

        public string TestTubeTwo { get; set; }
        public long? SaltEnvironmentTwo { get; set; }
        public long? AntiGlobulinEnvironmentTwo { get; set; }

        public decimal? AcSelfEnvidence { get; set; }
        public decimal? AcSelfEnvidenceSecond { get; set; }
    }
}
