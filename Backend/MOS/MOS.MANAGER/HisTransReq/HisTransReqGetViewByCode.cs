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
        internal V_HIS_TRANS_REQ GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisTransReqViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TRANS_REQ GetViewByCode(string code, HisTransReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransReqDAO.GetViewByCode(code, filter.Query());
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
