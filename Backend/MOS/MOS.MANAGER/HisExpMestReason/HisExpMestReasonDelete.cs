using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestReason
{
    partial class HisExpMestReasonDelete : BusinessBase
    {
        internal HisExpMestReasonDelete()
            : base()
        {

        }

        internal HisExpMestReasonDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXP_MEST_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestReasonCheck checker = new HisExpMestReasonCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestReasonDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXP_MEST_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestReasonCheck checker = new HisExpMestReasonCheck(param);
                List<HIS_EXP_MEST_REASON> listRaw = new List<HIS_EXP_MEST_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestReasonDAO.DeleteList(listData);
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
