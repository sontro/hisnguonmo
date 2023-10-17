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
        internal HIS_SESE_TRANS_REQ GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSeseTransReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SESE_TRANS_REQ GetByCode(string code, HisSeseTransReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSeseTransReqDAO.GetByCode(code, filter.Query());
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
