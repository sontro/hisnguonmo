using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCard
{
    public partial class HisCardDAO : EntityBase
    {
        public List<V_HIS_CARD> GetView(HisCardSO search, CommonParam param)
        {
            List<V_HIS_CARD> result = new List<V_HIS_CARD>();
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

        public V_HIS_CARD GetViewById(long id, HisCardSO search)
        {
            V_HIS_CARD result = null;

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
