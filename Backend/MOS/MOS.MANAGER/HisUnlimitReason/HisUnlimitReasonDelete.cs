using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUnlimitReason
{
    partial class HisUnlimitReasonDelete : BusinessBase
    {
        internal HisUnlimitReasonDelete()
            : base()
        {

        }

        internal HisUnlimitReasonDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_UNLIMIT_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUnlimitReasonCheck checker = new HisUnlimitReasonCheck(param);
                valid = valid && IsNotNull(data);
                HIS_UNLIMIT_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisUnlimitReasonDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_UNLIMIT_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUnlimitReasonCheck checker = new HisUnlimitReasonCheck(param);
                List<HIS_UNLIMIT_REASON> listRaw = new List<HIS_UNLIMIT_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisUnlimitReasonDAO.DeleteList(listData);
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
