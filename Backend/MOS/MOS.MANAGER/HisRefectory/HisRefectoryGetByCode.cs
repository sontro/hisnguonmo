using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    partial class HisRefectoryGet : BusinessBase
    {
        internal HIS_REFECTORY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRefectoryFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REFECTORY GetByCode(string code, HisRefectoryFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRefectoryDAO.GetByCode(code, filter.Query());
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
