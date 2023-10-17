using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAwareness
{
    public partial class HisAwarenessDAO : EntityBase
    {
        public List<V_HIS_AWARENESS> GetView(HisAwarenessSO search, CommonParam param)
        {
            List<V_HIS_AWARENESS> result = new List<V_HIS_AWARENESS>();
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

        public V_HIS_AWARENESS GetViewById(long id, HisAwarenessSO search)
        {
            V_HIS_AWARENESS result = null;

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
