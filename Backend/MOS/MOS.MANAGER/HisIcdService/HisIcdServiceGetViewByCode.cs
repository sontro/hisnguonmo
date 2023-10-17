using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdService
{
    partial class HisIcdServiceGet : BusinessBase
    {
        internal V_HIS_ICD_SERVICE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisIcdServiceViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ICD_SERVICE GetViewByCode(string code, HisIcdServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisIcdServiceDAO.GetViewByCode(code, filter.Query());
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
