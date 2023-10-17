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
        internal HIS_MR_CHECKLIST GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMrChecklistFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECKLIST GetByCode(string code, HisMrChecklistFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrChecklistDAO.GetByCode(code, filter.Query());
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
