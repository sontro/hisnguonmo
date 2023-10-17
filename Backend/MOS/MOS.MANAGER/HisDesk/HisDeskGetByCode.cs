using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    partial class HisDeskGet : BusinessBase
    {
        internal HIS_DESK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDeskFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DESK GetByCode(string code, HisDeskFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeskDAO.GetByCode(code, filter.Query());
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
