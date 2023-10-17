using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtDelete : BusinessBase
    {
        internal HisUserGroupTempDtDelete()
            : base()
        {

        }

        internal HisUserGroupTempDtDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_USER_GROUP_TEMP_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserGroupTempDtCheck checker = new HisUserGroupTempDtCheck(param);
                valid = valid && IsNotNull(data);
                HIS_USER_GROUP_TEMP_DT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisUserGroupTempDtDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_USER_GROUP_TEMP_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserGroupTempDtCheck checker = new HisUserGroupTempDtCheck(param);
                List<HIS_USER_GROUP_TEMP_DT> listRaw = new List<HIS_USER_GROUP_TEMP_DT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisUserGroupTempDtDAO.DeleteList(listData);
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
