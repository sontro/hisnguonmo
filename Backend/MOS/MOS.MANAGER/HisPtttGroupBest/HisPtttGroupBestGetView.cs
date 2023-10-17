using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroupBest
{
    partial class HisPtttGroupBestGet : BusinessBase
    {
        internal List<V_HIS_PTTT_GROUP_BEST> GetView(HisPtttGroupBestViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttGroupBestDAO.GetView(filter.Query(), param);
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
