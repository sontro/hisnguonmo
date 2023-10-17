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
        internal List<V_HIS_REGIMEN_HIV> GetView(HisRegimenHivViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRegimenHivDAO.GetView(filter.Query(), param);
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
