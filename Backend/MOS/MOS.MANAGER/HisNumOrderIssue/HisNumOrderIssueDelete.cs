using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNumOrderIssue
{
    partial class HisNumOrderIssueDelete : BusinessBase
    {
        internal HisNumOrderIssueDelete()
            : base()
        {

        }

        internal HisNumOrderIssueDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_NUM_ORDER_ISSUE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNumOrderIssueCheck checker = new HisNumOrderIssueCheck(param);
                valid = valid && IsNotNull(data);
                HIS_NUM_ORDER_ISSUE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisNumOrderIssueDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_NUM_ORDER_ISSUE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNumOrderIssueCheck checker = new HisNumOrderIssueCheck(param);
                List<HIS_NUM_ORDER_ISSUE> listRaw = new List<HIS_NUM_ORDER_ISSUE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisNumOrderIssueDAO.DeleteList(listData);
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
