using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStation
{
    partial class HisStationGet : BusinessBase
    {
        internal V_HIS_STATION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisStationViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_STATION GetViewByCode(string code, HisStationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStationDAO.GetViewByCode(code, filter.Query());
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
