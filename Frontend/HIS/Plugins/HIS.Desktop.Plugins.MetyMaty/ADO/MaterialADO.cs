using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MetyMaty.ADO
{
    public class MaterialADO : HIS_MATERIAL_TYPE
    {
        public Boolean check { get; set; }
        public decimal amount { get; set; }
        public Boolean checkMetyMety { get; set; }
    }
}
