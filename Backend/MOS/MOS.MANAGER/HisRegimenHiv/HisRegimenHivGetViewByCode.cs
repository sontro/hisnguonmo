using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegimenHiv
{
    partial class HisRegimenHivGet : BusinessBase
    {
        internal V_HIS_REGIMEN_HIV GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRegimenHivViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_REGIMEN_HIV GetViewByCode(string code, HisRegimenHivViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRegimenHivDAO.GetViewByCode(code, filter.Query());
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
