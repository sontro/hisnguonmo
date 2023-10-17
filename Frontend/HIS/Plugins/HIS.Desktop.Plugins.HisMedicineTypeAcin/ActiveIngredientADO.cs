using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicineTypeAcin
{
    public class ActiveIngredientADO : HIS_ACTIVE_INGREDIENT
    {
        public ActiveIngredientADO() { }
        public ActiveIngredientADO(HIS_ACTIVE_INGREDIENT data)
        {
            if (data != null)
            {
                this.ID = data.ID;
                this.ACTIVE_INGREDIENT_CODE = data.ACTIVE_INGREDIENT_CODE;
                this.ACTIVE_INGREDIENT_NAME = data.ACTIVE_INGREDIENT_NAME;
                this.APP_CREATOR = data.APP_CREATOR;
                this.APP_MODIFIER = data.APP_MODIFIER;
                this.CREATE_TIME = data.CREATE_TIME;
                this.CREATOR = data.CREATOR;
                this.GROUP_CODE = data.GROUP_CODE;
                this.IS_ACTIVE = data.IS_ACTIVE;
                this.IS_DELETE = data.IS_DELETE;
                this.MODIFIER = data.MODIFIER;
                this.MODIFY_TIME = data.MODIFY_TIME;
            }
        }

        public bool check2 { get; set; }
    }
}
