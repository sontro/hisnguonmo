using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgDelete : BusinessBase
    {
        internal HisExpiredDateCfgDelete()
            : base()
        {

        }

        internal HisExpiredDateCfgDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXPIRED_DATE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpiredDateCfgCheck checker = new HisExpiredDateCfgCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXPIRED_DATE_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExpiredDateCfgDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXPIRED_DATE_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpiredDateCfgCheck checker = new HisExpiredDateCfgCheck(param);
                List<HIS_EXPIRED_DATE_CFG> listRaw = new List<HIS_EXPIRED_DATE_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExpiredDateCfgDAO.DeleteList(listData);
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
