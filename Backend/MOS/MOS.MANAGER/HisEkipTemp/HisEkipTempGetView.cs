using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTemp
{
    partial class HisEkipTempGet : BusinessBase
    {
        internal List<V_HIS_EKIP_TEMP> GetView(HisEkipTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipTempDAO.GetView(filter.Query(), param);
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
