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
        internal HIS_RATION_GROUP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRationGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_GROUP GetByCode(string code, HisRationGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationGroupDAO.GetByCode(code, filter.Query());
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
