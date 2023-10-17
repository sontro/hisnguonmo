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
        internal HIS_EKIP_TEMP_USER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEkipTempUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP_TEMP_USER GetByCode(string code, HisEkipTempUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipTempUserDAO.GetByCode(code, filter.Query());
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
