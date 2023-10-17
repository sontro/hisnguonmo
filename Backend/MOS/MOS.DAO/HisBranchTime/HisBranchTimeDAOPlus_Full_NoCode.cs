using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBranchTime
{
    public partial class HisBranchTimeDAO : EntityBase
    {
        public List<V_HIS_BRANCH_TIME> GetView(HisBranchTimeSO search, CommonParam param)
        {
            List<V_HIS_BRANCH_TIME> result = new List<V_HIS_BRANCH_TIME>();
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

        public V_HIS_BRANCH_TIME GetViewById(long id, HisBranchTimeSO search)
        {
            V_HIS_BRANCH_TIME result = null;

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
