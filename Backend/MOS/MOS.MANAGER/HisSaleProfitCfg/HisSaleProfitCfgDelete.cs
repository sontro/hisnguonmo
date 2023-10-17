using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgDelete : BusinessBase
    {
        internal HisSaleProfitCfgDelete()
            : base()
        {

        }

        internal HisSaleProfitCfgDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SALE_PROFIT_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSaleProfitCfgCheck checker = new HisSaleProfitCfgCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SALE_PROFIT_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSaleProfitCfgDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_SALE_PROFIT_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSaleProfitCfgCheck checker = new HisSaleProfitCfgCheck(param);
                List<HIS_SALE_PROFIT_CFG> listRaw = new List<HIS_SALE_PROFIT_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSaleProfitCfgDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
