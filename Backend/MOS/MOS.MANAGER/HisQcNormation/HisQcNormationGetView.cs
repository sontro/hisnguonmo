using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcNormation
{
    partial class HisQcNormationGet : BusinessBase
    {
        internal List<V_HIS_QC_NORMATION> GetView(HisQcNormationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcNormationDAO.GetView(filter.Query(), param);
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
