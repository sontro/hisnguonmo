using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportLisBacterium.ADO
{
    public class BacteriumADO : LIS.EFMODEL.DataModels.LIS_BACTERIUM
    {
        public string BACTERIUM_FAMILY_CODE { get; set; }
        public string ERROR { get; set; }
    }
}
