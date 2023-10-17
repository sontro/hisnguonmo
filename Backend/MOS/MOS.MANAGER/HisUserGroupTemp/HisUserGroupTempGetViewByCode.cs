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
        internal V_HIS_USER_GROUP_TEMP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisUserGroupTempViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_USER_GROUP_TEMP GetViewByCode(string code, HisUserGroupTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDAO.GetViewByCode(code, filter.Query());
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
