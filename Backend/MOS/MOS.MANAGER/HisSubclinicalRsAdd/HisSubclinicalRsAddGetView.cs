using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddGet : BusinessBase
    {
        internal List<V_HIS_SUBCLINICAL_RS_ADD> GetView(HisSubclinicalRsAddViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSubclinicalRsAddDAO.GetView(filter.Query(), param);
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
