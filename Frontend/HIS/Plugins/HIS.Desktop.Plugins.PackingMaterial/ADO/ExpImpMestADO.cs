using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PackingMaterial.ADO
{
    public class ExpImpMestADO
    {
        public string TYPE_NAME { get; set; }
        public string ExpImpCode { get; set; }

        public ExpImpMestADO() { }

        public ExpImpMestADO(HIS_EXP_MEST expMest)
        {
            this.TYPE_NAME = "Phiếu xuất";
            this.ExpImpCode = expMest.EXP_MEST_CODE;
        }

        public ExpImpMestADO(HIS_IMP_MEST impMest)
        {
            this.TYPE_NAME = "Phiếu nhập";
            this.ExpImpCode = impMest.IMP_MEST_CODE;
        }
    }
}
