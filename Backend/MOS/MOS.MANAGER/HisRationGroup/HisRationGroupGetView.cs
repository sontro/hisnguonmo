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
        internal List<V_HIS_RATION_GROUP> GetView(HisRationGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationGroupDAO.GetView(filter.Query(), param);
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
