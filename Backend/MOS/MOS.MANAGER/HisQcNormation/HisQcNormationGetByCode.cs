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
        internal HIS_QC_NORMATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisQcNormationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_QC_NORMATION GetByCode(string code, HisQcNormationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcNormationDAO.GetByCode(code, filter.Query());
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
