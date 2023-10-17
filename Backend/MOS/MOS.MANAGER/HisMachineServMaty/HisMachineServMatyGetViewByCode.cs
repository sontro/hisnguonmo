using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachineServMaty
{
    partial class HisMachineServMatyGet : BusinessBase
    {
        internal V_HIS_MACHINE_SERV_MATY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMachineServMatyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MACHINE_SERV_MATY GetViewByCode(string code, HisMachineServMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMachineServMatyDAO.GetViewByCode(code, filter.Query());
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
