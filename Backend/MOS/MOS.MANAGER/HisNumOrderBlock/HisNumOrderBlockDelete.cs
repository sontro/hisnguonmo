using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNumOrderBlock
{
    partial class HisNumOrderBlockDelete : BusinessBase
    {
        internal HisNumOrderBlockDelete()
            : base()
        {

        }

        internal HisNumOrderBlockDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_NUM_ORDER_BLOCK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                valid = valid && IsNotNull(data);
                HIS_NUM_ORDER_BLOCK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisNumOrderBlockDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_NUM_ORDER_BLOCK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                List<HIS_NUM_ORDER_BLOCK> listRaw = new List<HIS_NUM_ORDER_BLOCK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisNumOrderBlockDAO.DeleteList(listData);
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
