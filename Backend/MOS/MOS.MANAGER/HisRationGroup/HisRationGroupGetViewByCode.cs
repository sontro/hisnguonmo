using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationGroup
{
    partial class HisRationGroupGet : BusinessBase
    {
        internal V_HIS_RATION_GROUP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRationGroupViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_RATION_GROUP GetViewByCode(string code, HisRationGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationGroupDAO.GetViewByCode(code, filter.Query());
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
