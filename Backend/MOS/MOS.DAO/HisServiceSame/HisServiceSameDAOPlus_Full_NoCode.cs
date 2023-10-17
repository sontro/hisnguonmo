using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceSame
{
    public partial class HisServiceSameDAO : EntityBase
    {
        public List<V_HIS_SERVICE_SAME> GetView(HisServiceSameSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_SAME> result = new List<V_HIS_SERVICE_SAME>();
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

        public V_HIS_SERVICE_SAME GetViewById(long id, HisServiceSameSO search)
        {
            V_HIS_SERVICE_SAME result = null;

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
