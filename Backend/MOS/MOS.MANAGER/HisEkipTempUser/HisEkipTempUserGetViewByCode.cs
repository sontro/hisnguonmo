using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTempUser
{
    partial class HisEkipTempUserGet : BusinessBase
    {
        internal V_HIS_EKIP_TEMP_USER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEkipTempUserViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EKIP_TEMP_USER GetViewByCode(string code, HisEkipTempUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipTempUserDAO.GetViewByCode(code, filter.Query());
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
