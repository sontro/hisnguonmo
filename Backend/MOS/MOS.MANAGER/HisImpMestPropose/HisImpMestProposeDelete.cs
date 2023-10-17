using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestPropose
{
    partial class HisImpMestProposeDelete : BusinessBase
    {
        internal HisImpMestProposeDelete()
            : base()
        {

        }

        internal HisImpMestProposeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_IMP_MEST_PROPOSE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_PROPOSE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestProposeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_IMP_MEST_PROPOSE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                List<HIS_IMP_MEST_PROPOSE> listRaw = new List<HIS_IMP_MEST_PROPOSE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestProposeDAO.DeleteList(listData);
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
