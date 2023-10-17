using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashout
{
    partial class HisCashoutGet : BusinessBase
    {
        internal List<V_HIS_CASHOUT> GetView(HisCashoutViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCashoutDAO.GetView(filter.Query(), param);
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
