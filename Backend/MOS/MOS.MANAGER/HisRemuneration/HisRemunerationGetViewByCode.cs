using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRemuneration
{
    partial class HisRemunerationGet : BusinessBase
    {
        internal V_HIS_REMUNERATION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRemunerationViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_REMUNERATION GetViewByCode(string code, HisRemunerationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRemunerationDAO.GetViewByCode(code, filter.Query());
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
