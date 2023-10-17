using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ReturnMicrobiologicalResults.ADO
{
    public class LisResultADO : V_LIS_RESULT
    {
        public long? ANTIBIOTIC_ORDER { get; set; }
    }
}
