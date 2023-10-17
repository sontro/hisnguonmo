using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyProduct
{
    partial class HisMetyProductGet : BusinessBase
    {
        internal List<V_HIS_METY_PRODUCT> GetView(HisMetyProductViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyProductDAO.GetView(filter.Query(), param);
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
