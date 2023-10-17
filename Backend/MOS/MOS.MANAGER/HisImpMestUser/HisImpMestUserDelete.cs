using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestUser
{
    partial class HisImpMestUserDelete : BusinessBase
    {
        internal HisImpMestUserDelete()
            : base()
        {

        }

        internal HisImpMestUserDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_IMP_MEST_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestUserDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_IMP_MEST_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                List<HIS_IMP_MEST_USER> listRaw = new List<HIS_IMP_MEST_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestUserDAO.DeleteList(listData);
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
