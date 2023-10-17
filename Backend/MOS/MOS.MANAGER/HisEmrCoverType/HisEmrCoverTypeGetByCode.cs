using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverType
{
    partial class HisEmrCoverTypeGet : BusinessBase
    {
        internal HIS_EMR_COVER_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEmrCoverTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_COVER_TYPE GetByCode(string code, HisEmrCoverTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverTypeDAO.GetByCode(code, filter.Query());
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
