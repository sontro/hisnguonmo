using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateBlty
{
    partial class HisAnticipateBltyGet : BusinessBase
    {
        internal List<V_HIS_ANTICIPATE_BLTY> GetView(HisAnticipateBltyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAnticipateBltyDAO.GetView(filter.Query(), param);
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
