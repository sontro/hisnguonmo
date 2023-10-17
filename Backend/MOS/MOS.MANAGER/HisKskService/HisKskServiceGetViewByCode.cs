using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceGet : BusinessBase
    {
        internal V_HIS_KSK_SERVICE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisKskServiceViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_SERVICE GetViewByCode(string code, HisKskServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskServiceDAO.GetViewByCode(code, filter.Query());
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
