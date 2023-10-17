using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrChecklist
{
    partial class HisMrChecklistDelete : BusinessBase
    {
        internal HisMrChecklistDelete()
            : base()
        {

        }

        internal HisMrChecklistDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MR_CHECKLIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECKLIST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMrChecklistDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MR_CHECKLIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                List<HIS_MR_CHECKLIST> listRaw = new List<HIS_MR_CHECKLIST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMrChecklistDAO.DeleteList(listData);
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
