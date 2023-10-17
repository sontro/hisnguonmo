using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProcessingMethod
{
    partial class HisProcessingMethodGet : BusinessBase
    {
        internal List<V_HIS_PROCESSING_METHOD> GetView(HisProcessingMethodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisProcessingMethodDAO.GetView(filter.Query(), param);
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
