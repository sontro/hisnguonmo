using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExportMestMedicine
{
    class ExpMestADO
    {
        public string LoginName { get; set; }
        public V_HIS_MEDI_STOCK MediStock { get; set; }
        public List<ACS.EFMODEL.DataModels.ACS_CONTROL> controlAcs { get; set; }
        public List<HIS_IMP_MEST> listImpMest { get; set; }
    }
}
