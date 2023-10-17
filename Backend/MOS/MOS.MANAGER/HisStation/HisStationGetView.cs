using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStation
{
    partial class HisStationGet : BusinessBase
    {
        internal List<V_HIS_STATION> GetView(HisStationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStationDAO.GetView(filter.Query(), param);
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
