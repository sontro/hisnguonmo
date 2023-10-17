using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserGroupTemp
{
    partial class HisUserGroupTempDelete : BusinessBase
    {
        internal HisUserGroupTempDelete()
            : base()
        {

        }

        internal HisUserGroupTempDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_USER_GROUP_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserGroupTempCheck checker = new HisUserGroupTempCheck(param);
                valid = valid && IsNotNull(data);
                HIS_USER_GROUP_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisUserGroupTempDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_USER_GROUP_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserGroupTempCheck checker = new HisUserGroupTempCheck(param);
                List<HIS_USER_GROUP_TEMP> listRaw = new List<HIS_USER_GROUP_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisUserGroupTempDAO.DeleteList(listData);
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
