using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepositReq
{
    partial class HisDepositReqDelete : BusinessBase
    {
        internal HisDepositReqDelete()
            : base()
        {

        }

        internal HisDepositReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DEPOSIT_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepositReqCheck checker = new HisDepositReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DEPOSIT_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDepositReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DEPOSIT_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDepositReqCheck checker = new HisDepositReqCheck(param);
                List<HIS_DEPOSIT_REQ> listRaw = new List<HIS_DEPOSIT_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.HasNoDeposit(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDepositReqDAO.DeleteList(listData);
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
