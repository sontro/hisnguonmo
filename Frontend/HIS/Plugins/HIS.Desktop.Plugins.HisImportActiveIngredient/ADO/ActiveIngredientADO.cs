using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportActiveIngredient.ADO
{
    public class ActiveIngredientADO : MOS.EFMODEL.DataModels.HIS_ACTIVE_INGREDIENT
    {
        public string IS_CONSULTATION_REQUIRED_STR { get; set; }
        public string ERROR { get; set; }
    }
}
