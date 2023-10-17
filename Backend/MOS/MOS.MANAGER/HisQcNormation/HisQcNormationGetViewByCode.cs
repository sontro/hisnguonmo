using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcNormation
{
    partial class HisQcNormationGet : BusinessBase
    {
        internal V_HIS_QC_NORMATION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisQcNormationViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_QC_NORMATION GetViewByCode(string code, HisQcNormationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcNormationDAO.GetViewByCode(code, filter.Query());
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
