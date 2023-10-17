using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBranchTime
{
    partial class HisBranchTimeDelete : BusinessBase
    {
        internal HisBranchTimeDelete()
            : base()
        {

        }

        internal HisBranchTimeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBranchTimeCheck checker = new HisBranchTimeCheck(param);
                HIS_BRANCH_TIME raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBranchTimeDAO.Delete(raw);
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

        internal bool DeleteList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisBranchTimeCheck checker = new HisBranchTimeCheck(param);
                List<HIS_BRANCH_TIME> listRaw = new List<HIS_BRANCH_TIME>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBranchTimeDAO.DeleteList(listRaw);
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
