using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranchTime
{
    partial class HisBranchTimeGet : BusinessBase
    {
        internal List<V_HIS_BRANCH_TIME> GetView(HisBranchTimeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBranchTimeDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
