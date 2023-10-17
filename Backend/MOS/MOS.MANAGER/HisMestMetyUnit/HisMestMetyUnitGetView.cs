using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    partial class HisMestMetyUnitGet : BusinessBase
    {
        internal List<V_HIS_MEST_METY_UNIT> GetView(HisMestMetyUnitViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyUnitDAO.GetView(filter.Query(), param);
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
