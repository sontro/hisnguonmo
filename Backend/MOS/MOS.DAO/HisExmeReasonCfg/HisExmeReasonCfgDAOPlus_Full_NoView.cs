using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExmeReasonCfg
{
    public partial class HisExmeReasonCfgDAO : EntityBase
    {
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
