using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRehaSum
{
    class HisRehaSumTruncate : BusinessBase
    {
        internal HisRehaSumTruncate()
            : base()
        {

        }

        internal HisRehaSumTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long rehaSumId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaSumCheck checker = new HisRehaSumCheck(param);
                valid = valid && IsGreaterThanZero(rehaSumId);
                HIS_REHA_SUM raw = null;
                valid = valid && checker.VerifyId(rehaSumId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByRehaSumId(rehaSumId);
                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        serviceReqs.ForEach(o => o.REHA_SUM_ID = null);
                        if (!new HisServiceReqUpdate(param).UpdateList(serviceReqs))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                    }
                    result = DAOWorker.HisRehaSumDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_REHA_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaSumCheck checker = new HisRehaSumCheck(param);
                List<HIS_REHA_SUM> listRaw = new List<HIS_REHA_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRehaSumDAO.TruncateList(listData);
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
