using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransactionExp
{
    partial class HisTransactionExpDelete : BusinessBase
    {
        internal HisTransactionExpDelete()
            : base()
        {

        }

        internal HisTransactionExpDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TRANSACTION_EXP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionExpCheck checker = new HisTransactionExpCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION_EXP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTransactionExpDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TRANSACTION_EXP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionExpCheck checker = new HisTransactionExpCheck(param);
                List<HIS_TRANSACTION_EXP> listRaw = new List<HIS_TRANSACTION_EXP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTransactionExpDAO.DeleteList(listData);
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
