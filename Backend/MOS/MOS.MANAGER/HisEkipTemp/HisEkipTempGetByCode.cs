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
        internal HIS_EKIP_TEMP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEkipTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP_TEMP GetByCode(string code, HisEkipTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipTempDAO.GetByCode(code, filter.Query());
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
