using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttPriority
{
    partial class HisPtttPriorityDelete : BusinessBase
    {
        internal HisPtttPriorityDelete()
            : base()
        {

        }

        internal HisPtttPriorityDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PTTT_PRIORITY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttPriorityCheck checker = new HisPtttPriorityCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_PRIORITY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPtttPriorityDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PTTT_PRIORITY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttPriorityCheck checker = new HisPtttPriorityCheck(param);
                List<HIS_PTTT_PRIORITY> listRaw = new List<HIS_PTTT_PRIORITY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPtttPriorityDAO.DeleteList(listData);
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
