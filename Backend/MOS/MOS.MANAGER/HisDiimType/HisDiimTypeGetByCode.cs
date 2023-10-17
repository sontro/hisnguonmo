using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDiimType
{
    partial class HisDiimTypeGet : BusinessBase
    {
        internal HIS_DIIM_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDiimTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DIIM_TYPE GetByCode(string code, HisDiimTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDiimTypeDAO.GetByCode(code, filter.Query());
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
