using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserDelete : BusinessBase
    {
        internal HisExecuteRoleUserDelete()
            : base()
        {

        }

        internal HisExecuteRoleUserDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXECUTE_ROLE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteRoleUserCheck checker = new HisExecuteRoleUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_ROLE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExecuteRoleUserDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXECUTE_ROLE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExecuteRoleUserCheck checker = new HisExecuteRoleUserCheck(param);
                List<HIS_EXECUTE_ROLE_USER> listRaw = new List<HIS_EXECUTE_ROLE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExecuteRoleUserDAO.DeleteList(listData);
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
