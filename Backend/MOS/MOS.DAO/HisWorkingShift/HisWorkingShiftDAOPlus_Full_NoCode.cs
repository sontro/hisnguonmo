using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisWorkingShift
{
    public partial class HisWorkingShiftDAO : EntityBase
    {
        public List<V_HIS_WORKING_SHIFT> GetView(HisWorkingShiftSO search, CommonParam param)
        {
            List<V_HIS_WORKING_SHIFT> result = new List<V_HIS_WORKING_SHIFT>();
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

        public V_HIS_WORKING_SHIFT GetViewById(long id, HisWorkingShiftSO search)
        {
            V_HIS_WORKING_SHIFT result = null;

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
