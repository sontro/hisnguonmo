using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGiver
{
    partial class HisBloodGiverGet : BusinessBase
    {
        internal List<V_HIS_BLOOD_GIVER> GetView(HisBloodGiverViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBloodGiverDAO.GetView(filter.Query(), param);
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
