using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpBltyService
{
    partial class HisExpBltyServiceGet : BusinessBase
    {
        internal List<V_HIS_EXP_BLTY_SERVICE> GetView(HisExpBltyServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpBltyServiceDAO.GetView(filter.Query(), param);
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
