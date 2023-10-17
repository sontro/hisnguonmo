using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttPriority
{
    partial class HisPtttPriorityGet : BusinessBase
    {
        internal HIS_PTTT_PRIORITY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPtttPriorityFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_PRIORITY GetByCode(string code, HisPtttPriorityFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttPriorityDAO.GetByCode(code, filter.Query());
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
