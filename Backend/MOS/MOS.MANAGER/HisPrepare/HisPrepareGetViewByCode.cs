using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepare
{
    partial class HisPrepareGet : BusinessBase
    {
        internal V_HIS_PREPARE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPrepareViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PREPARE GetViewByCode(string code, HisPrepareViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareDAO.GetViewByCode(code, filter.Query());
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
