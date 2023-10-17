using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSum
{
    partial class HisRationSumGet : BusinessBase
    {
        internal List<V_HIS_RATION_SUM> GetView(HisRationSumViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationSumDAO.GetView(filter.Query(), param);
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
