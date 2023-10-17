using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpiredDateCfg
{
    public partial class HisExpiredDateCfgDAO : EntityBase
    {
        public List<V_HIS_EXPIRED_DATE_CFG> GetView(HisExpiredDateCfgSO search, CommonParam param)
        {
            List<V_HIS_EXPIRED_DATE_CFG> result = new List<V_HIS_EXPIRED_DATE_CFG>();
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

        public V_HIS_EXPIRED_DATE_CFG GetViewById(long id, HisExpiredDateCfgSO search)
        {
            V_HIS_EXPIRED_DATE_CFG result = null;

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
