using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUnlimitType
{
    partial class HisUnlimitTypeGet : BusinessBase
    {
        internal HIS_UNLIMIT_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisUnlimitTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_UNLIMIT_TYPE GetByCode(string code, HisUnlimitTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUnlimitTypeDAO.GetByCode(code, filter.Query());
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
