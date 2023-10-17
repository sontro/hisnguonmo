using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseTransReq
{
    partial class HisSeseTransReqGet : BusinessBase
    {
        internal V_HIS_SESE_TRANS_REQ GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSeseTransReqViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SESE_TRANS_REQ GetViewByCode(string code, HisSeseTransReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSeseTransReqDAO.GetViewByCode(code, filter.Query());
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
