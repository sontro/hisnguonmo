using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAllergyCard.SDO.Update
{
    class AllergyCardProcessor : BusinessBase
    {
        private HisAllergyCardUpdate hisAllergyCardUpdate;

        internal AllergyCardProcessor()
            : base()
        {
            this.hisAllergyCardUpdate = new  HisAllergyCardUpdate(param);
        }

        internal AllergyCardProcessor(CommonParam param)
            : base(param)
        {
            this.hisAllergyCardUpdate = new HisAllergyCardUpdate(param);
        }

        internal bool Run(HIS_ALLERGY_CARD allergyCard, HIS_ALLERGY_CARD before)
        {
            bool result = false;
            try
            {
                if (allergyCard != null)
                {
                    allergyCard.TREATMENT_ID = before.TREATMENT_ID;
                    if (!this.hisAllergyCardUpdate.Update(allergyCard, before))
                    {
                        throw new Exception("Update HIS_ALLERGY_CARD that bai. Ket thuc nghiep vu");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisAllergyCardUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
