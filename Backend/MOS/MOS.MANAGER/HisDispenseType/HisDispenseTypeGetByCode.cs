using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispenseType
{
    partial class HisDispenseTypeGet : BusinessBase
    {
        internal HIS_DISPENSE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDispenseTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DISPENSE_TYPE GetByCode(string code, HisDispenseTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDispenseTypeDAO.GetByCode(code, filter.Query());
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
