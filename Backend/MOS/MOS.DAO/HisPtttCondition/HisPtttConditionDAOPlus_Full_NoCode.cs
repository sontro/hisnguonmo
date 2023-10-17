using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCondition
{
    public partial class HisPtttConditionDAO : EntityBase
    {
        public List<V_HIS_PTTT_CONDITION> GetView(HisPtttConditionSO search, CommonParam param)
        {
            List<V_HIS_PTTT_CONDITION> result = new List<V_HIS_PTTT_CONDITION>();
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

        public V_HIS_PTTT_CONDITION GetViewById(long id, HisPtttConditionSO search)
        {
            V_HIS_PTTT_CONDITION result = null;

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
