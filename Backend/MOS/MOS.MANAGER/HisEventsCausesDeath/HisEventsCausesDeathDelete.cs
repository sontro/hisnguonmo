using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathDelete : BusinessBase
    {
        internal HisEventsCausesDeathDelete()
            : base()
        {

        }

        internal HisEventsCausesDeathDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EVENTS_CAUSES_DEATH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEventsCausesDeathCheck checker = new HisEventsCausesDeathCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EVENTS_CAUSES_DEATH raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEventsCausesDeathDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EVENTS_CAUSES_DEATH> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEventsCausesDeathCheck checker = new HisEventsCausesDeathCheck(param);
                List<HIS_EVENTS_CAUSES_DEATH> listRaw = new List<HIS_EVENTS_CAUSES_DEATH>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEventsCausesDeathDAO.DeleteList(listData);
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
