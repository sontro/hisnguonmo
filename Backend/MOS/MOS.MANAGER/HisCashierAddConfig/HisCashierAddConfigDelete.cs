using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCashierAddConfig
{
    partial class HisCashierAddConfigDelete : BusinessBase
    {
        internal HisCashierAddConfigDelete()
            : base()
        {

        }

        internal HisCashierAddConfigDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CASHIER_ADD_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashierAddConfigCheck checker = new HisCashierAddConfigCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CASHIER_ADD_CONFIG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCashierAddConfigDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CASHIER_ADD_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashierAddConfigCheck checker = new HisCashierAddConfigCheck(param);
                List<HIS_CASHIER_ADD_CONFIG> listRaw = new List<HIS_CASHIER_ADD_CONFIG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCashierAddConfigDAO.DeleteList(listData);
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
