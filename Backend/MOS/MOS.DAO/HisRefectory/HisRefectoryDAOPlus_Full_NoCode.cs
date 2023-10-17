using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRefectory
{
    public partial class HisRefectoryDAO : EntityBase
    {
        public List<V_HIS_REFECTORY> GetView(HisRefectorySO search, CommonParam param)
        {
            List<V_HIS_REFECTORY> result = new List<V_HIS_REFECTORY>();
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

        public V_HIS_REFECTORY GetViewById(long id, HisRefectorySO search)
        {
            V_HIS_REFECTORY result = null;

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
