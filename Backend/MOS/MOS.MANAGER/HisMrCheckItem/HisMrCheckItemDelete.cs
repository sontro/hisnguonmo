using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrCheckItem
{
    partial class HisMrCheckItemDelete : BusinessBase
    {
        internal HisMrCheckItemDelete()
            : base()
        {

        }

        internal HisMrCheckItemDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MR_CHECK_ITEM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckItemCheck checker = new HisMrCheckItemCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_ITEM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMrCheckItemDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MR_CHECK_ITEM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrCheckItemCheck checker = new HisMrCheckItemCheck(param);
                List<HIS_MR_CHECK_ITEM> listRaw = new List<HIS_MR_CHECK_ITEM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMrCheckItemDAO.DeleteList(listData);
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
