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
        internal V_HIS_AGE_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAgeTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_AGE_TYPE GetViewByCode(string code, HisAgeTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAgeTypeDAO.GetViewByCode(code, filter.Query());
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
