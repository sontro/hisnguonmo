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
        internal HIS_STATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisStationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STATION GetByCode(string code, HisStationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStationDAO.GetByCode(code, filter.Query());
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
