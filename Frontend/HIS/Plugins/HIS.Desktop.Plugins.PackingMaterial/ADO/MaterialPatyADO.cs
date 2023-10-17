using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PackingMaterial.ADO
{
    public class MaterialPatyADO : HIS_MATERIAL_PATY
    {
        public string  PATIENT_TYPE_NAME { get; set; }
        public decimal? VAT { get; set; }
        public decimal? PRICE { get; set; }
    }
}
