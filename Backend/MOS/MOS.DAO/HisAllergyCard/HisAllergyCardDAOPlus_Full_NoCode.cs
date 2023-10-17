using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAllergyCard
{
    public partial class HisAllergyCardDAO : EntityBase
    {
        public List<V_HIS_ALLERGY_CARD> GetView(HisAllergyCardSO search, CommonParam param)
        {
            List<V_HIS_ALLERGY_CARD> result = new List<V_HIS_ALLERGY_CARD>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_ALLERGY_CARD GetViewById(long id, HisAllergyCardSO search)
        {
            V_HIS_ALLERGY_CARD result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
