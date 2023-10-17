using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFund
{
    partial class HisFundGet : BusinessBase
    {
        internal V_HIS_FUND GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisFundViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_FUND GetViewByCode(string code, HisFundViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFundDAO.GetViewByCode(code, filter.Query());
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
