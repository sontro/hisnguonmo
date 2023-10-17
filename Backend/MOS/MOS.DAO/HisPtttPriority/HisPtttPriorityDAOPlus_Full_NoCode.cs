using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttPriority
{
    public partial class HisPtttPriorityDAO : EntityBase
    {
        public List<V_HIS_PTTT_PRIORITY> GetView(HisPtttPrioritySO search, CommonParam param)
        {
            List<V_HIS_PTTT_PRIORITY> result = new List<V_HIS_PTTT_PRIORITY>();
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

        public V_HIS_PTTT_PRIORITY GetViewById(long id, HisPtttPrioritySO search)
        {
            V_HIS_PTTT_PRIORITY result = null;

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
