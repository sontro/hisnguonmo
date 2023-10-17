using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipPlanUser
{
    partial class HisEkipPlanUserDelete : BusinessBase
    {
        internal HisEkipPlanUserDelete()
            : base()
        {

        }

        internal HisEkipPlanUserDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EKIP_PLAN_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipPlanUserCheck checker = new HisEkipPlanUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_PLAN_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEkipPlanUserDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EKIP_PLAN_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipPlanUserCheck checker = new HisEkipPlanUserCheck(param);
                List<HIS_EKIP_PLAN_USER> listRaw = new List<HIS_EKIP_PLAN_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEkipPlanUserDAO.DeleteList(listData);
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
