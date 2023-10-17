using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReqMaty
{
    partial class HisServiceReqMatyDelete : BusinessBase
    {
        internal HisServiceReqMatyDelete()
            : base()
        {

        }

        internal HisServiceReqMatyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERVICE_REQ_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqMatyCheck checker = new HisServiceReqMatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceReqMatyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERVICE_REQ_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqMatyCheck checker = new HisServiceReqMatyCheck(param);
                List<HIS_SERVICE_REQ_MATY> listRaw = new List<HIS_SERVICE_REQ_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceReqMatyDAO.DeleteList(listData);
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
