using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtGet : BusinessBase
    {
        internal List<V_HIS_USER_GROUP_TEMP_DT> GetView(HisUserGroupTempDtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDtDAO.GetView(filter.Query(), param);
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
