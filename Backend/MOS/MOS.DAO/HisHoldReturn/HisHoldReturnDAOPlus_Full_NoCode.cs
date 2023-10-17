using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoldReturn
{
    public partial class HisHoldReturnDAO : EntityBase
    {
        public List<V_HIS_HOLD_RETURN> GetView(HisHoldReturnSO search, CommonParam param)
        {
            List<V_HIS_HOLD_RETURN> result = new List<V_HIS_HOLD_RETURN>();
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

        public V_HIS_HOLD_RETURN GetViewById(long id, HisHoldReturnSO search)
        {
            V_HIS_HOLD_RETURN result = null;

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
