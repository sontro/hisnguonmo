using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    partial class HisKskAccessGet : BusinessBase
    {
        internal V_HIS_KSK_ACCESS GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisKskAccessViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_ACCESS GetViewByCode(string code, HisKskAccessViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskAccessDAO.GetViewByCode(code, filter.Query());
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
