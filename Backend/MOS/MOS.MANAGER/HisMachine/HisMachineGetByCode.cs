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
        internal HIS_MACHINE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMachineFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MACHINE GetByCode(string code, HisMachineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMachineDAO.GetByCode(code, filter.Query());
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
