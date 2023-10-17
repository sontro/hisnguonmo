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
        internal HIS_USER_GROUP_TEMP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisUserGroupTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_USER_GROUP_TEMP GetByCode(string code, HisUserGroupTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDAO.GetByCode(code, filter.Query());
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
