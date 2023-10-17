using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    public class AdviseFormADO 
    {
        public AdviseFormADO() { }
        public bool? IncludeMaterial { get; set; }
        public bool? AutoGetHomePres { get; set; }
        public List<long> MedicineUseFormIds { get; set; }
        public List<long> ExpMestTypeIds { get; set; }
        public string AdviseResult { get; set; }
    }
}
