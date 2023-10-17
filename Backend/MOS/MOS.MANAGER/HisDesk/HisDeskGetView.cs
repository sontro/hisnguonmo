using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    partial class HisDeskGet : BusinessBase
    {
        internal List<V_HIS_DESK> GetView(HisDeskViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeskDAO.GetView(filter.Query(), param);
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
