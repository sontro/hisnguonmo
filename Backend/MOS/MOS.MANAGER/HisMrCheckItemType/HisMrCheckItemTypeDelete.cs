using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeDelete : BusinessBase
    {
        internal HisMrCheckItemTypeDelete()
            : base()
        {

        }

        internal HisMrCheckItemTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MR_CHECK_ITEM_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckItemTypeCheck checker = new HisMrCheckItemTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_ITEM_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMrCheckItemTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrCheckItemTypeCheck checker = new HisMrCheckItemTypeCheck(param);
                List<HIS_MR_CHECK_ITEM_TYPE> listRaw = new List<HIS_MR_CHECK_ITEM_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMrCheckItemTypeDAO.DeleteList(listData);
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
