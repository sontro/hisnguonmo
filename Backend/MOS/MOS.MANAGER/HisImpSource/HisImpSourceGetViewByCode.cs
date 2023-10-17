using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpSource
{
    partial class HisImpSourceGet : BusinessBase
    {
        internal V_HIS_IMP_SOURCE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisImpSourceViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_SOURCE GetViewByCode(string code, HisImpSourceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpSourceDAO.GetViewByCode(code, filter.Query());
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
