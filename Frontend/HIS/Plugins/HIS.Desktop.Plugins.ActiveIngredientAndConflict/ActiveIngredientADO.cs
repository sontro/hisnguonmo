using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ActiveIngredientAndConflict
{
    public class ActiveIngredientADO: HIS_ACTIVE_INGREDIENT
    {
        public ActiveIngredientADO() { }
        public ActiveIngredientADO(HIS.UC.ConflictActiveIngredient.ConflictActiveIngredientADO data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ActiveIngredientADO>(this, data);
                this.ACTIVE_INGREDIENT_CODE_XD = data.ACTIVE_INGREDIENT_CODE;
                this.ACTIVE_INGREDIENT_NAME_XD = data.ACTIVE_INGREDIENT_NAME;
            }
        }
        public ActiveIngredientADO(HIS.UC.ActiveIngredent.ActiveIngredentADO data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ActiveIngredientADO>(this, data);
            }
        }
        public string ACTIVE_INGREDIENT_CODE_XD { get; set; }
        public string ACTIVE_INGREDIENT_NAME_XD { get; set; }
        public bool check2 { get; set; }
        public bool isKeyChoose1 { get; set; }
        public bool radio2 { get; set; }
        public string DESCRIPTION { get; set; }
        public string INSTRUCTION { get; set; }
        public long? INTERACTIVE_GRADE_ID { get; set; }
        public string CONSEQUENCE { get; set; }
        public string MECHANISM { get; set; }
    }
}
