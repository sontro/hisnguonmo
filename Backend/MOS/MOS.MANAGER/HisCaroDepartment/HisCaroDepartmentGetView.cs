using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroDepartment
{
    partial class HisCaroDepartmentGet : BusinessBase
    {
        internal List<V_HIS_CARO_DEPARTMENT> GetView(HisCaroDepartmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCaroDepartmentDAO.GetView(filter.Query(), param);
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
