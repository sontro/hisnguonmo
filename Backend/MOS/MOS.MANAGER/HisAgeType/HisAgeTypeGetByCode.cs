using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAgeType
{
    partial class HisAgeTypeGet : BusinessBase
    {
        internal HIS_AGE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAgeTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_AGE_TYPE GetByCode(string code, HisAgeTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAgeTypeDAO.GetByCode(code, filter.Query());
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
