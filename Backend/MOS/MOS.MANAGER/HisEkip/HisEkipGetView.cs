using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkip
{
    partial class HisEkipGet : BusinessBase
    {
        internal List<V_HIS_EKIP> GetView(HisEkipViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipDAO.GetView(filter.Query(), param);
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
