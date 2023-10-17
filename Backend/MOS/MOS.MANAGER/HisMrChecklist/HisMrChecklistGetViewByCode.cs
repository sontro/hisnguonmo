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
        internal V_HIS_MR_CHECKLIST GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMrChecklistViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MR_CHECKLIST GetViewByCode(string code, HisMrChecklistViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrChecklistDAO.GetViewByCode(code, filter.Query());
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
