using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayDelete : BusinessBase
    {
        internal HisSeseDepoRepayDelete()
            : base()
        {

        }

        internal HisSeseDepoRepayDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SESE_DEPO_REPAY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSeseDepoRepayCheck checker = new HisSeseDepoRepayCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_DEPO_REPAY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSeseDepoRepayDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SESE_DEPO_REPAY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSeseDepoRepayCheck checker = new HisSeseDepoRepayCheck(param);
                List<HIS_SESE_DEPO_REPAY> listRaw = new List<HIS_SESE_DEPO_REPAY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSeseDepoRepayDAO.DeleteList(listData);
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
