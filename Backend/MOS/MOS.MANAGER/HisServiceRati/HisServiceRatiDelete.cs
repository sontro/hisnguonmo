using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceRati
{
    partial class HisServiceRatiDelete : BusinessBase
    {
        internal HisServiceRatiDelete()
            : base()
        {

        }

        internal HisServiceRatiDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERVICE_RATI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RATI raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceRatiDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERVICE_RATI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
                List<HIS_SERVICE_RATI> listRaw = new List<HIS_SERVICE_RATI>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceRatiDAO.DeleteList(listData);
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
