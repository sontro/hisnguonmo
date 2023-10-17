using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipPlan
{
    partial class HisEkipPlanDelete : BusinessBase
    {
        internal HisEkipPlanDelete()
            : base()
        {

        }

        internal HisEkipPlanDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EKIP_PLAN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipPlanCheck checker = new HisEkipPlanCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_PLAN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEkipPlanDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EKIP_PLAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipPlanCheck checker = new HisEkipPlanCheck(param);
                List<HIS_EKIP_PLAN> listRaw = new List<HIS_EKIP_PLAN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEkipPlanDAO.DeleteList(listData);
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
