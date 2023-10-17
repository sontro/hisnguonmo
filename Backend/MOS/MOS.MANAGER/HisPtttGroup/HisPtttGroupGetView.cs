using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroup
{
    partial class HisPtttGroupGet : BusinessBase
    {
        internal List<V_HIS_PTTT_GROUP> GetView(HisPtttGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttGroupDAO.GetView(filter.Query(), param);
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
