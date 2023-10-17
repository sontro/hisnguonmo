using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcType
{
    partial class HisQcTypeGet : BusinessBase
    {
        internal HIS_QC_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisQcTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_QC_TYPE GetByCode(string code, HisQcTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcTypeDAO.GetByCode(code, filter.Query());
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
