using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepositReq
{
    partial class HisDepositReqTruncate : BusinessBase
    {
        internal HisDepositReqTruncate()
            : base()
        {

        }

        internal HisDepositReqTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepositReqCheck checker = new HisDepositReqCheck(param);
                HIS_DEPOSIT_REQ raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNoDeposit(raw);
                if (valid)
                {
                    result = DAOWorker.HisDepositReqDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_DEPOSIT_REQ> listData)
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
                    result = DAOWorker.HisDepositReqDAO.TruncateList(listData);
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
