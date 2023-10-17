using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisStorageCondition
{
    partial class HisStorageConditionDelete : BusinessBase
    {
        internal HisStorageConditionDelete()
            : base()
        {

        }

        internal HisStorageConditionDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_STORAGE_CONDITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStorageConditionCheck checker = new HisStorageConditionCheck(param);
                valid = valid && IsNotNull(data);
                HIS_STORAGE_CONDITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisStorageConditionDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_STORAGE_CONDITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisStorageConditionCheck checker = new HisStorageConditionCheck(param);
                List<HIS_STORAGE_CONDITION> listRaw = new List<HIS_STORAGE_CONDITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisStorageConditionDAO.DeleteList(listData);
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
