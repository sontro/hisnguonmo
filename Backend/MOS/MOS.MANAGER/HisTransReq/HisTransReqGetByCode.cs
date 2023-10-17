using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransReq
{
    partial class HisTransReqGet : BusinessBase
    {
        internal HIS_TRANS_REQ GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTransReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANS_REQ GetByCode(string code, HisTransReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransReqDAO.GetByCode(code, filter.Query());
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
