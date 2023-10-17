using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployee
{
    partial class HisEmployeeGet : BusinessBase
    {
        internal List<V_HIS_EMPLOYEE> GetView(HisEmployeeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmployeeDAO.GetView(filter.Query(), param);
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
