using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PackingMaterial.ADO
{
    public class PackingMatyMatyADO
    {
        public long PREPA_MATERIAL_TYPE_ID { get; set; }
        public string MATERIAL_TYPE_CODE { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public bool IsNotAvaliable { get; set; }

        public decimal? OLD_AMOUNT { get; set; }
        public decimal? CFG_AMOUNT { get; set; }

    }
}
