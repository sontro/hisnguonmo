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
        internal V_HIS_MATY_MATY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMatyMatyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MATY_MATY GetViewByCode(string code, HisMatyMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMatyMatyDAO.GetViewByCode(code, filter.Query());
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
