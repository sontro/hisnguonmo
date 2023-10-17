using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierAddConfig
{
    partial class HisCashierAddConfigGet : BusinessBase
    {
        internal List<V_HIS_CASHIER_ADD_CONFIG> GetView(HisCashierAddConfigViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCashierAddConfigDAO.GetView(filter.Query(), param);
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
