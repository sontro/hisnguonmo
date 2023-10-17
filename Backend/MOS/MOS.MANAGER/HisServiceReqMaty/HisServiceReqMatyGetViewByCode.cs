using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMaty
{
    partial class HisServiceReqMatyGet : BusinessBase
    {
        internal V_HIS_SERVICE_REQ_MATY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisServiceReqMatyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ_MATY GetViewByCode(string code, HisServiceReqMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMatyDAO.GetViewByCode(code, filter.Query());
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
