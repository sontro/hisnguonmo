using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTemp
{
    partial class HisEkipTempGet : BusinessBase
    {
        internal V_HIS_EKIP_TEMP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEkipTempViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EKIP_TEMP GetViewByCode(string code, HisEkipTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipTempDAO.GetViewByCode(code, filter.Query());
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
