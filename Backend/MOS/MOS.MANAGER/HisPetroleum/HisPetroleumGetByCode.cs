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
        internal HIS_PETROLEUM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPetroleumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PETROLEUM GetByCode(string code, HisPetroleumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPetroleumDAO.GetByCode(code, filter.Query());
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
