using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriver
{
    partial class HisKskDriverGet : BusinessBase
    {
        internal V_HIS_KSK_DRIVER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisKskDriverViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_DRIVER GetViewByCode(string code, HisKskDriverViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverDAO.GetViewByCode(code, filter.Query());
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
