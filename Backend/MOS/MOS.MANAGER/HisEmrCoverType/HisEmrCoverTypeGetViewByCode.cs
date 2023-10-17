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
        internal V_HIS_EMR_COVER_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEmrCoverTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EMR_COVER_TYPE GetViewByCode(string code, HisEmrCoverTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverTypeDAO.GetViewByCode(code, filter.Query());
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
