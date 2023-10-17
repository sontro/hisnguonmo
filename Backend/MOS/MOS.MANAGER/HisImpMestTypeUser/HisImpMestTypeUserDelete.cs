using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserDelete : BusinessBase
    {
        internal HisImpMestTypeUserDelete()
            : base()
        {

        }

        internal HisImpMestTypeUserDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_IMP_MEST_TYPE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestTypeUserCheck checker = new HisImpMestTypeUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_TYPE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestTypeUserDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_IMP_MEST_TYPE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestTypeUserCheck checker = new HisImpMestTypeUserCheck(param);
                List<HIS_IMP_MEST_TYPE_USER> listRaw = new List<HIS_IMP_MEST_TYPE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestTypeUserDAO.DeleteList(listData);
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
