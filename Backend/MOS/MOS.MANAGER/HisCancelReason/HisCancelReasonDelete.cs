using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCancelReason
{
    partial class HisCancelReasonDelete : BusinessBase
    {
        internal HisCancelReasonDelete()
            : base()
        {

        }

        internal HisCancelReasonDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CANCEL_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCancelReasonCheck checker = new HisCancelReasonCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CANCEL_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCancelReasonDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CANCEL_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCancelReasonCheck checker = new HisCancelReasonCheck(param);
                List<HIS_CANCEL_REASON> listRaw = new List<HIS_CANCEL_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCancelReasonDAO.DeleteList(listData);
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
