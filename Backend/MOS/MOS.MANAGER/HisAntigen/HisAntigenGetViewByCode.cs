using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigen
{
    partial class HisAntigenGet : BusinessBase
    {
        internal V_HIS_ANTIGEN GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAntigenViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ANTIGEN GetViewByCode(string code, HisAntigenViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenDAO.GetViewByCode(code, filter.Query());
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
