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
        internal HIS_WELFARE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisWelfareTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WELFARE_TYPE GetByCode(string code, HisWelfareTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWelfareTypeDAO.GetByCode(code, filter.Query());
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
