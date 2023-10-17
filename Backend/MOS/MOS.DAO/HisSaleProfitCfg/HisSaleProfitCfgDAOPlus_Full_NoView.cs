using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSaleProfitCfg
{
    public partial class HisSaleProfitCfgDAO : EntityBase
    {
        public HIS_SALE_PROFIT_CFG GetByCode(string code, HisSaleProfitCfgSO search)
        {
            HIS_SALE_PROFIT_CFG result = null;

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

        public Dictionary<string, HIS_SALE_PROFIT_CFG> GetDicByCode(HisSaleProfitCfgSO search, CommonParam param)
        {
            Dictionary<string, HIS_SALE_PROFIT_CFG> result = new Dictionary<string, HIS_SALE_PROFIT_CFG>();
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
