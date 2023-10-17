using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfigGroup
{
    partial class HisConfigGroupGet : BusinessBase
    {
        internal HIS_CONFIG_GROUP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisConfigGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONFIG_GROUP GetByCode(string code, HisConfigGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisConfigGroupDAO.GetByCode(code, filter.Query());
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
