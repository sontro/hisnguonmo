using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPetroleum
{
    partial class HisPetroleumGet : BusinessBase
    {
        internal V_HIS_PETROLEUM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPetroleumViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PETROLEUM GetViewByCode(string code, HisPetroleumViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPetroleumDAO.GetViewByCode(code, filter.Query());
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
