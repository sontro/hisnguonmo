using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgDelete : BusinessBase
    {
        internal HisExmeReasonCfgDelete()
            : base()
        {

        }

        internal HisExmeReasonCfgDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXME_REASON_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExmeReasonCfgCheck checker = new HisExmeReasonCfgCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXME_REASON_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExmeReasonCfgDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXME_REASON_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExmeReasonCfgCheck checker = new HisExmeReasonCfgCheck(param);
                List<HIS_EXME_REASON_CFG> listRaw = new List<HIS_EXME_REASON_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExmeReasonCfgDAO.DeleteList(listData);
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
