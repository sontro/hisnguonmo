using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHelmet
{
    partial class HisAccidentHelmetGet : BusinessBase
    {
        internal List<V_HIS_ACCIDENT_HELMET> GetView(HisAccidentHelmetViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAccidentHelmetDAO.GetView(filter.Query(), param);
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
