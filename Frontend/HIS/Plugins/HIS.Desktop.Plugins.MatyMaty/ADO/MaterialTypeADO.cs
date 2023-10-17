using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MatyMaty.ADO
{
    public class MaterialTypeADO : HIS_MATERIAL_TYPE
    {
        public string SERVICE_UNIT_NAME { get; set; }

        public MaterialTypeADO(HIS_MATERIAL_TYPE m)
        {
            this.ID = m.ID;
            this.MATERIAL_TYPE_CODE = m.MATERIAL_TYPE_CODE;
            this.MATERIAL_TYPE_NAME = m.MATERIAL_TYPE_NAME;
            this.Check = false;
        }
        public bool Check { get; set; }
        public decimal AMOUNT { get; set; }
    }
}
