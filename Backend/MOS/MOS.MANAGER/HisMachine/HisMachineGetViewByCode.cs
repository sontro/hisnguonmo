using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachine
{
    partial class HisMachineGet : BusinessBase
    {
        internal V_HIS_MACHINE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMachineViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MACHINE GetViewByCode(string code, HisMachineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMachineDAO.GetViewByCode(code, filter.Query());
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
