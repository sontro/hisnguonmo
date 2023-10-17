using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPetroleum
{
    partial class HisPetroleumGet : BusinessBase
    {
        internal List<V_HIS_PETROLEUM> GetView(HisPetroleumViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPetroleumDAO.GetView(filter.Query(), param);
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
