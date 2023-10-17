using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    partial class HisStentConcludeGet : BusinessBase
    {
        internal List<V_HIS_STENT_CONCLUDE> GetView(HisStentConcludeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStentConcludeDAO.GetView(filter.Query(), param);
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
