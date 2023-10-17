using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergenic
{
    partial class HisAllergenicGet : BusinessBase
    {
        internal HisAllergenicGet()
            : base()
        {

        }

        internal HisAllergenicGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ALLERGENIC> Get(HisAllergenicFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAllergenicDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ALLERGENIC GetById(long id)
        {
            try
            {
                return GetById(id, new HisAllergenicFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ALLERGENIC GetById(long id, HisAllergenicFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAllergenicDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ALLERGENIC> GetByAllergyCardId(long allergyCardId)
        {
            try
            {
                HisAllergenicFilterQuery filter = new HisAllergenicFilterQuery();
                filter.ALLERGY_CARD_ID = allergyCardId;
                return this.Get(filter);
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
