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
        internal V_HIS_STENT_CONCLUDE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisStentConcludeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_STENT_CONCLUDE GetViewByCode(string code, HisStentConcludeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStentConcludeDAO.GetViewByCode(code, filter.Query());
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
