using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisWarningFeeCfg
{
    public partial class HisWarningFeeCfgDAO : EntityBase
    {
        public List<V_HIS_WARNING_FEE_CFG> GetView(HisWarningFeeCfgSO search, CommonParam param)
        {
            List<V_HIS_WARNING_FEE_CFG> result = new List<V_HIS_WARNING_FEE_CFG>();
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

        public V_HIS_WARNING_FEE_CFG GetViewById(long id, HisWarningFeeCfgSO search)
        {
            V_HIS_WARNING_FEE_CFG result = null;

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
