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
        internal HIS_SERVICE_REQ_MATY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisServiceReqMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ_MATY GetByCode(string code, HisServiceReqMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMatyDAO.GetByCode(code, filter.Query());
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
