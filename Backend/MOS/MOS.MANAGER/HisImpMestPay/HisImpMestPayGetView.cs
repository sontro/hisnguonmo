using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPay
{
    partial class HisImpMestPayGet : BusinessBase
    {
        internal List<V_HIS_IMP_MEST_PAY> GetView(HisImpMestPayViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestPayDAO.GetView(filter.Query(), param);
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
