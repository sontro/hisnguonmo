using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class MobaImpMestListADO
    {
        public long MODULE_TYPE { get; set; }
        public String ExpMestCode { get; set; }
        public String TreatmentCode { get; set; }

        public MobaImpMestListADO(long moduleTpye, string expMestCode, string treatmentCode)
        {
            this.MODULE_TYPE = moduleTpye;
            this.ExpMestCode = expMestCode;
            this.TreatmentCode = treatmentCode;
        }
    }
}
