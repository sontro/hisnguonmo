using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    partial class HisMestMetyUnitGet : BusinessBase
    {
        internal V_HIS_MEST_METY_UNIT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMestMetyUnitViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEST_METY_UNIT GetViewByCode(string code, HisMestMetyUnitViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyUnitDAO.GetViewByCode(code, filter.Query());
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
