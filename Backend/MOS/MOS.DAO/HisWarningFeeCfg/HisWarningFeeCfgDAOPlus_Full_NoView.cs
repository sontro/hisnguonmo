using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisWarningFeeCfg
{
    public partial class HisWarningFeeCfgDAO : EntityBase
    {
        public HIS_WARNING_FEE_CFG GetByCode(string code, HisWarningFeeCfgSO search)
        {
            HIS_WARNING_FEE_CFG result = null;

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

        public Dictionary<string, HIS_WARNING_FEE_CFG> GetDicByCode(HisWarningFeeCfgSO search, CommonParam param)
        {
            Dictionary<string, HIS_WARNING_FEE_CFG> result = new Dictionary<string, HIS_WARNING_FEE_CFG>();
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
