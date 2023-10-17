using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    partial class HisImpUserTempDtGet : BusinessBase
    {
        internal List<V_HIS_IMP_USER_TEMP_DT> GetView(HisImpUserTempDtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDtDAO.GetView(filter.Query(), param);
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
