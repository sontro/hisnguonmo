using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    partial class HisNoneMediServiceGet : BusinessBase
    {
        internal V_HIS_NONE_MEDI_SERVICE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisNoneMediServiceViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_NONE_MEDI_SERVICE GetViewByCode(string code, HisNoneMediServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNoneMediServiceDAO.GetViewByCode(code, filter.Query());
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
