using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSpeedUnit
{
    public partial class HisSpeedUnitDAO : EntityBase
    {
        public List<V_HIS_SPEED_UNIT> GetView(HisSpeedUnitSO search, CommonParam param)
        {
            List<V_HIS_SPEED_UNIT> result = new List<V_HIS_SPEED_UNIT>();
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

        public V_HIS_SPEED_UNIT GetViewById(long id, HisSpeedUnitSO search)
        {
            V_HIS_SPEED_UNIT result = null;

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
