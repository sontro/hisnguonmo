using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOccupational
{
    partial class HisKskOccupationalGet : BusinessBase
    {
        internal List<V_HIS_KSK_OCCUPATIONAL> GetView(HisKskOccupationalViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOccupationalDAO.GetView(filter.Query(), param);
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
