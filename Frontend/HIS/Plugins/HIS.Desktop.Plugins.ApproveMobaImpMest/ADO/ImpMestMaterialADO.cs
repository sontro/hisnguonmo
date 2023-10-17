using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveMobaImpMest.ADO
{
    class ImpMestMaterialADO : V_HIS_IMP_MEST_MATERIAL
    {
        public decimal? YCD_AMOUNT { get; set; }
        public string NOTE { get; set; }

        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorYcdAmount { get; set; }
        public string ErrorMessageYcdAmount { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorNote { get; set; }
        public string ErrorMessageNote { get; set; }
    }
}
