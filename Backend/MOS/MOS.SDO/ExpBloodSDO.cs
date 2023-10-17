using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpBloodSDO
    {
        public long BloodId { get; set; }
        public long? ExpMestBltyReqId { get; set; }
        public long? NumOrder { get; set; }
        public decimal? Price { get; set; }
        public decimal? VatRatio { get; set; }
        public decimal? DiscountRatio { get; set; }
        public string Description { get; set; }
        public long? SaltEnvi { get; set; }
        public long? AntiGlobulinEnvi { get; set; }

        public decimal? AcSelfEnvidence { get; set; }
        public decimal? AcSelfEnvidenceSecond { get; set; }

        public long? SaltEnviTwo { get; set; }
        public long? AntiGlobulinEnviTwo { get; set; }
        public string PatientBloodAboCode { get; set; }
        public string PatientBloodRhCode { get; set; }
    }
}
