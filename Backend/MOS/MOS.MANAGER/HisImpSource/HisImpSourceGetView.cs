using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpSource
{
    partial class HisImpSourceGet : BusinessBase
    {
        internal List<V_HIS_IMP_SOURCE> GetView(HisImpSourceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpSourceDAO.GetView(filter.Query(), param);
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
