using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationSumStt
{
    partial class HisRationSumSttDelete : BusinessBase
    {
        internal HisRationSumSttDelete()
            : base()
        {

        }

        internal HisRationSumSttDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_RATION_SUM_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationSumSttCheck checker = new HisRationSumSttCheck(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM_STT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRationSumSttDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_RATION_SUM_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationSumSttCheck checker = new HisRationSumSttCheck(param);
                List<HIS_RATION_SUM_STT> listRaw = new List<HIS_RATION_SUM_STT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRationSumSttDAO.DeleteList(listData);
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
