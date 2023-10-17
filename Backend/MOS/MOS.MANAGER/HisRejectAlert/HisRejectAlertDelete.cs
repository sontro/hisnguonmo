using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRejectAlert
{
    partial class HisRejectAlertDelete : BusinessBase
    {
        internal HisRejectAlertDelete()
            : base()
        {

        }

        internal HisRejectAlertDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_REJECT_ALERT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRejectAlertCheck checker = new HisRejectAlertCheck(param);
                valid = valid && IsNotNull(data);
                HIS_REJECT_ALERT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRejectAlertDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_REJECT_ALERT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRejectAlertCheck checker = new HisRejectAlertCheck(param);
                List<HIS_REJECT_ALERT> listRaw = new List<HIS_REJECT_ALERT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRejectAlertDAO.DeleteList(listData);
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
