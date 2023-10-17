using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAllergyCard.SDO.Create
{
    class AllergyCardProcessor : BusinessBase
    {
        private HisAllergyCardCreate hisAllergyCardCreate;

        internal AllergyCardProcessor()
            : base()
        {
            this.hisAllergyCardCreate = new HisAllergyCardCreate(param);
        }

        internal AllergyCardProcessor(CommonParam param)
            : base(param)
        {
            this.hisAllergyCardCreate = new HisAllergyCardCreate(param);
        }

        internal bool Run(HIS_ALLERGY_CARD allergyCard)
        {
            bool result = false;
            try
            {
                if (allergyCard!=null)
                {
                    if (!this.hisAllergyCardCreate.Create(allergyCard))
                    {
                        throw new Exception("Tao HIS_ALLERGY_CARD that bai. Ket thuc nghiep vu");
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
                this.hisAllergyCardCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
