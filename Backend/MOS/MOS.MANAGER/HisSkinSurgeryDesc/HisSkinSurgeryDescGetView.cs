using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescGet : BusinessBase
    {
        internal List<V_HIS_SKIN_SURGERY_DESC> GetView(HisSkinSurgeryDescViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSkinSurgeryDescDAO.GetView(filter.Query(), param);
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
