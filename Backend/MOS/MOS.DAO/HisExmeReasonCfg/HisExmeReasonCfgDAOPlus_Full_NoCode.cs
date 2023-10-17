using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExmeReasonCfg
{
    public partial class HisExmeReasonCfgDAO : EntityBase
    {
        public List<V_HIS_EXME_REASON_CFG> GetView(HisExmeReasonCfgSO search, CommonParam param)
        {
            List<V_HIS_EXME_REASON_CFG> result = new List<V_HIS_EXME_REASON_CFG>();
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

        public V_HIS_EXME_REASON_CFG GetViewById(long id, HisExmeReasonCfgSO search)
        {
            V_HIS_EXME_REASON_CFG result = null;

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
