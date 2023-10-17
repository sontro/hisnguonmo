using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    partial class HisRefectoryGet : BusinessBase
    {
        internal List<V_HIS_REFECTORY> GetView(HisRefectoryViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRefectoryDAO.GetView(filter.Query(), param);
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
