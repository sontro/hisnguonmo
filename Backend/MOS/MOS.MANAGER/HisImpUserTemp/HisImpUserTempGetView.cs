using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTemp
{
    partial class HisImpUserTempGet : BusinessBase
    {
        internal List<V_HIS_IMP_USER_TEMP> GetView(HisImpUserTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDAO.GetView(filter.Query(), param);
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
