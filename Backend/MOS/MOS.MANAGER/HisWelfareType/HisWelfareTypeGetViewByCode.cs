using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWelfareType
{
    partial class HisWelfareTypeGet : BusinessBase
    {
        internal V_HIS_WELFARE_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisWelfareTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_WELFARE_TYPE GetViewByCode(string code, HisWelfareTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWelfareTypeDAO.GetViewByCode(code, filter.Query());
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
