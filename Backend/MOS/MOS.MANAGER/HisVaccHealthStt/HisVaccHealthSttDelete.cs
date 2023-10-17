using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccHealthStt
{
    partial class HisVaccHealthSttDelete : BusinessBase
    {
        internal HisVaccHealthSttDelete()
            : base()
        {

        }

        internal HisVaccHealthSttDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACC_HEALTH_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccHealthSttCheck checker = new HisVaccHealthSttCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_HEALTH_STT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccHealthSttDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACC_HEALTH_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccHealthSttCheck checker = new HisVaccHealthSttCheck(param);
                List<HIS_VACC_HEALTH_STT> listRaw = new List<HIS_VACC_HEALTH_STT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccHealthSttDAO.DeleteList(listData);
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
