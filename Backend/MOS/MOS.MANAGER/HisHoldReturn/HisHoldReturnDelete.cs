using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnDelete : BusinessBase
    {
        internal HisHoldReturnDelete()
            : base()
        {

        }

        internal HisHoldReturnDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_HOLD_RETURN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                valid = valid && IsNotNull(data);
                HIS_HOLD_RETURN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisHoldReturnDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_HOLD_RETURN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                List<HIS_HOLD_RETURN> listRaw = new List<HIS_HOLD_RETURN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisHoldReturnDAO.DeleteList(listData);
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
