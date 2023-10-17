using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestInventory
{
    partial class HisMestInventoryDelete : BusinessBase
    {
        internal HisMestInventoryDelete()
            : base()
        {

        }

        internal HisMestInventoryDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_INVENTORY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestInventoryCheck checker = new HisMestInventoryCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_INVENTORY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestInventoryDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_INVENTORY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestInventoryCheck checker = new HisMestInventoryCheck(param);
                List<HIS_MEST_INVENTORY> listRaw = new List<HIS_MEST_INVENTORY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestInventoryDAO.DeleteList(listData);
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
