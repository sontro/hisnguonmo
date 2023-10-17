using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyService
{
    partial class HisBltyServiceGet : BusinessBase
    {
        internal List<V_HIS_BLTY_SERVICE> GetView(HisBltyServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBltyServiceDAO.GetView(filter.Query(), param);
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
