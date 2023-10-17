using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkip
{
    partial class HisEkipGet : BusinessBase
    {
        internal V_HIS_EKIP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEkipViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EKIP GetViewByCode(string code, HisEkipViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipDAO.GetViewByCode(code, filter.Query());
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
