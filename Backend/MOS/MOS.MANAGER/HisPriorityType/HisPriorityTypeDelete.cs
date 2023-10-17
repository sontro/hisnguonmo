using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPriorityType
{
    partial class HisPriorityTypeDelete : BusinessBase
    {
        internal HisPriorityTypeDelete()
            : base()
        {

        }

        internal HisPriorityTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PRIORITY_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPriorityTypeCheck checker = new HisPriorityTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PRIORITY_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPriorityTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PRIORITY_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPriorityTypeCheck checker = new HisPriorityTypeCheck(param);
                List<HIS_PRIORITY_TYPE> listRaw = new List<HIS_PRIORITY_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPriorityTypeDAO.DeleteList(listData);
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
