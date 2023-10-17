using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCare;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareSum
{
    class HisCareSumTruncate : BusinessBase
    {
        internal HisCareSumTruncate()
            : base()
        {

        }

        internal HisCareSumTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long careSumId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareSumCheck checker = new HisCareSumCheck(param);
                valid = valid && IsGreaterThanZero(careSumId);
                HIS_CARE_SUM raw = null;
                valid = valid && checker.VerifyId(careSumId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(careSumId);
                if (valid)
                {
                    result = DAOWorker.HisCareSumDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_CARE_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareSumCheck checker = new HisCareSumCheck(param);
                List<HIS_CARE_SUM> listRaw = new List<HIS_CARE_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCareSumDAO.TruncateList(listData);
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
