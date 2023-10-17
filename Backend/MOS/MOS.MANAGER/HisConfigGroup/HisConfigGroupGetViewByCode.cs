using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfigGroup
{
    partial class HisConfigGroupGet : BusinessBase
    {
        internal V_HIS_CONFIG_GROUP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisConfigGroupViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_CONFIG_GROUP GetViewByCode(string code, HisConfigGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisConfigGroupDAO.GetViewByCode(code, filter.Query());
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
