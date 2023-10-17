using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfig
{
    partial class HisConfigGet : BusinessBase
    {
        internal List<V_HIS_CONFIG> GetView(HisConfigViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisConfigDAO.GetView(filter.Query(), param);
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
