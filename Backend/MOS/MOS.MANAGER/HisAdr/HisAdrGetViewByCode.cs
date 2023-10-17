using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdr
{
    partial class HisAdrGet : BusinessBase
    {
        internal V_HIS_ADR GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAdrViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ADR GetViewByCode(string code, HisAdrViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrDAO.GetViewByCode(code, filter.Query());
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
