using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCarerCard
{
    public partial class HisCarerCardDAO : EntityBase
    {
        public List<V_HIS_CARER_CARD> GetView(HisCarerCardSO search, CommonParam param)
        {
            List<V_HIS_CARER_CARD> result = new List<V_HIS_CARER_CARD>();
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

        public V_HIS_CARER_CARD GetViewById(long id, HisCarerCardSO search)
        {
            V_HIS_CARER_CARD result = null;

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
