using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoGet : BusinessBase
    {
        internal List<V_HIS_SEVERE_ILLNESS_INFO> GetView(HisSevereIllnessInfoViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSevereIllnessInfoDAO.GetView(filter.Query(), param);
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
