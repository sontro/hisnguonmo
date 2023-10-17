using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceNumOrder
{
    partial class HisServiceNumOrderDelete : BusinessBase
    {
        internal HisServiceNumOrderDelete()
            : base()
        {

        }

        internal HisServiceNumOrderDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERVICE_NUM_ORDER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceNumOrderCheck checker = new HisServiceNumOrderCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_NUM_ORDER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceNumOrderDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERVICE_NUM_ORDER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceNumOrderCheck checker = new HisServiceNumOrderCheck(param);
                List<HIS_SERVICE_NUM_ORDER> listRaw = new List<HIS_SERVICE_NUM_ORDER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceNumOrderDAO.DeleteList(listData);
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
