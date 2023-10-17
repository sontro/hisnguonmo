using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAllergyCard
{
    partial class HisAllergyCardDelete : BusinessBase
    {
        internal HisAllergyCardDelete()
            : base()
        {

        }

        internal HisAllergyCardDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ALLERGY_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ALLERGY_CARD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAllergyCardDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_ALLERGY_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                List<HIS_ALLERGY_CARD> listRaw = new List<HIS_ALLERGY_CARD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAllergyCardDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
