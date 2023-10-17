using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExecuteRole
{
    partial class HisExecuteRoleDelete : BusinessBase
    {
        internal HisExecuteRoleDelete()
            : base()
        {

        }

        internal HisExecuteRoleDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXECUTE_ROLE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteRoleCheck checker = new HisExecuteRoleCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_ROLE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExecuteRoleDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXECUTE_ROLE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExecuteRoleCheck checker = new HisExecuteRoleCheck(param);
                List<HIS_EXECUTE_ROLE> listRaw = new List<HIS_EXECUTE_ROLE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExecuteRoleDAO.DeleteList(listData);
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
