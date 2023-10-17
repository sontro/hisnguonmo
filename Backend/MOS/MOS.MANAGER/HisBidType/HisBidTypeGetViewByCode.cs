using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidType
{
    partial class HisBidTypeGet : BusinessBase
    {
        internal V_HIS_BID_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBidTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BID_TYPE GetViewByCode(string code, HisBidTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidTypeDAO.GetViewByCode(code, filter.Query());
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
