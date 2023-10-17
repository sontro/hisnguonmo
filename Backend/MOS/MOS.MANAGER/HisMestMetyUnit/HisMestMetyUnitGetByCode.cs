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
        internal HIS_MEST_METY_UNIT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMestMetyUnitFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_METY_UNIT GetByCode(string code, HisMestMetyUnitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyUnitDAO.GetByCode(code, filter.Query());
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
