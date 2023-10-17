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
        internal List<V_HIS_CONFIG_GROUP> GetView(HisConfigGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisConfigGroupDAO.GetView(filter.Query(), param);
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
