using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMatyMaty
{
    partial class HisMatyMatyGet : BusinessBase
    {
        internal HIS_MATY_MATY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMatyMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATY_MATY GetByCode(string code, HisMatyMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMatyMatyDAO.GetByCode(code, filter.Query());
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
