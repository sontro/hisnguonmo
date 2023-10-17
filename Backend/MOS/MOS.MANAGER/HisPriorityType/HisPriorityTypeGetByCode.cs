using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPriorityType
{
    partial class HisPriorityTypeGet : BusinessBase
    {
        internal HIS_PRIORITY_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPriorityTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PRIORITY_TYPE GetByCode(string code, HisPriorityTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPriorityTypeDAO.GetByCode(code, filter.Query());
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
