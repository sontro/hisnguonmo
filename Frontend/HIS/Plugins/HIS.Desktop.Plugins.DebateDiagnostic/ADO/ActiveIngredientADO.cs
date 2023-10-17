using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebateDiagnostic.ADO
{
    internal class ActiveIngredientADO : MOS.EFMODEL.DataModels.HIS_ACTIVE_INGREDIENT
    {
        public bool IsChecked { get; set; }
        public string ACTIVE_INGREDIENT_NAME__UNSIGN { get; set; }

        public ActiveIngredientADO(MOS.EFMODEL.DataModels.HIS_ACTIVE_INGREDIENT item)
        {
            Mapper.CreateMap<MOS.EFMODEL.DataModels.HIS_ACTIVE_INGREDIENT, ActiveIngredientADO>();
            Mapper.Map<MOS.EFMODEL.DataModels.HIS_ACTIVE_INGREDIENT, ActiveIngredientADO>(item, this);
            this.ACTIVE_INGREDIENT_NAME__UNSIGN = Inventec.Common.String.Convert.UnSignVNese(this.ACTIVE_INGREDIENT_NAME);
        }
    }
}
