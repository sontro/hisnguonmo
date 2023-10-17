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
        internal HIS_REGIMEN_HIV GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRegimenHivFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REGIMEN_HIV GetByCode(string code, HisRegimenHivFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRegimenHivDAO.GetByCode(code, filter.Query());
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
