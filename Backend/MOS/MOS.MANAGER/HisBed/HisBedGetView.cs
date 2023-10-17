using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBed
{
    partial class HisBedGet : BusinessBase
    {
        internal List<V_HIS_BED> GetView(HisBedViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedDAO.GetView(filter.Query(), param);
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
