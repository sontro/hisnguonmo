using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisArea
{
    partial class HisAreaGet : BusinessBase
    {
        internal HIS_AREA GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAreaFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_AREA GetByCode(string code, HisAreaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAreaDAO.GetByCode(code, filter.Query());
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
