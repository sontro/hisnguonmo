using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidType
{
    partial class HisBidTypeGet : BusinessBase
    {
        internal List<V_HIS_BID_TYPE> GetView(HisBidTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidTypeDAO.GetView(filter.Query(), param);
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
