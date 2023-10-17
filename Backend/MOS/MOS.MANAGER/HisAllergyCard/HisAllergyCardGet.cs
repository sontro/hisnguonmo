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
        internal HisAllergyCardGet()
            : base()
        {

        }

        internal HisAllergyCardGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ALLERGY_CARD> Get(HisAllergyCardFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAllergyCardDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ALLERGY_CARD GetById(long id)
        {
            try
            {
                return GetById(id, new HisAllergyCardFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ALLERGY_CARD GetById(long id, HisAllergyCardFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAllergyCardDAO.GetById(id, filter.Query());
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
