using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAllergenic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAllergyCard.SDO.Create
{
    class AllergenicProcessor : BusinessBase
    {
        private HisAllergenicCreate hisAllergenicCreate;

        internal AllergenicProcessor()
            : base()
        {
            this.hisAllergenicCreate = new HisAllergenicCreate(param);
        }

        internal AllergenicProcessor(CommonParam param)
            : base(param)
        {
            this.hisAllergenicCreate = new HisAllergenicCreate(param);
        }

        internal bool Run(HIS_ALLERGY_CARD allergyCard, List<HIS_ALLERGENIC> allergenics)
        {
            bool result = false;
            try
            {
                if (allergyCard != null && IsNotNullOrEmpty(allergenics))
                {
                    allergenics.ForEach(o => o.ALLERGY_CARD_ID = allergyCard.ID);
                    if (!this.hisAllergenicCreate.CreateList(allergenics))
                    {
                        throw new Exception("Tao HIS_ALLERGENIC that bai. Ket thuc nghiep vu");
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
                this.hisAllergenicCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
