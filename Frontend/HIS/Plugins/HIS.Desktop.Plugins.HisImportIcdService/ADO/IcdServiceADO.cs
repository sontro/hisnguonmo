using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisImportIcdService.ADO
{
    class IcdServiceADO : HIS_ICD_SERVICE
    {
        public string ERROR { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string ACTIVE_INGREDIENT_CODE { get; set; }
        public string ACTIVE_INGREDIENT_NAME { get; set; }
        public string CONTRAINDICATION_CONTENT { get; set; }
    }
}
