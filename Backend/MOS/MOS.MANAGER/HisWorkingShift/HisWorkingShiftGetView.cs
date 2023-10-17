using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkingShift
{
    partial class HisWorkingShiftGet : BusinessBase
    {
        internal List<V_HIS_WORKING_SHIFT> GetView(HisWorkingShiftViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWorkingShiftDAO.GetView(filter.Query(), param);
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
