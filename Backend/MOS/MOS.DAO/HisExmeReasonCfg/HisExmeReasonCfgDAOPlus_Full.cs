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

        public HIS_EXME_REASON_CFG GetByCode(string code, HisExmeReasonCfgSO search)
        {
            HIS_EXME_REASON_CFG result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_EXME_REASON_CFG GetViewByCode(string code, HisExmeReasonCfgSO search)
        {
            V_HIS_EXME_REASON_CFG result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_EXME_REASON_CFG> GetDicByCode(HisExmeReasonCfgSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXME_REASON_CFG> result = new Dictionary<string, HIS_EXME_REASON_CFG>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
