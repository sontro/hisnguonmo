using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTemp
{
    partial class HisUserGroupTempGet : BusinessBase
    {
        internal List<V_HIS_USER_GROUP_TEMP> GetView(HisUserGroupTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDAO.GetView(filter.Query(), param);
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
