using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergyCard
{
    partial class HisAllergyCardGet : BusinessBase
    {
        internal List<V_HIS_ALLERGY_CARD> GetView(HisAllergyCardViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAllergyCardDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ALLERGY_CARD GetViewById(long id)
        {
            try
            {
                return DAOWorker.HisAllergyCardDAO.GetViewById(id, new HisAllergyCardViewFilterQuery().Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

    }
}
