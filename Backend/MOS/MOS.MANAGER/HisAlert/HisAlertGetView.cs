using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAlert
{
    partial class HisAlertGet : BusinessBase
    {
        internal List<V_HIS_ALERT> GetView(HisAlertViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAlertDAO.GetView(filter.Query(), param);
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
