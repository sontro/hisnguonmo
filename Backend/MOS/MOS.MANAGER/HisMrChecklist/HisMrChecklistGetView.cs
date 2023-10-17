using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrChecklist
{
    partial class HisMrChecklistGet : BusinessBase
    {
        internal List<V_HIS_MR_CHECKLIST> GetView(HisMrChecklistViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrChecklistDAO.GetView(filter.Query(), param);
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
