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
        internal HIS_MACHINE_SERV_MATY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMachineServMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MACHINE_SERV_MATY GetByCode(string code, HisMachineServMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMachineServMatyDAO.GetByCode(code, filter.Query());
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
