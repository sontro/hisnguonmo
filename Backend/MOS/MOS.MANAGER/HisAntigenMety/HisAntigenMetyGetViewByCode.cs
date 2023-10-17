using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigenMety
{
    partial class HisAntigenMetyGet : BusinessBase
    {
        internal V_HIS_ANTIGEN_METY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAntigenMetyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ANTIGEN_METY GetViewByCode(string code, HisAntigenMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenMetyDAO.GetViewByCode(code, filter.Query());
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
