using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMemaGroup
{
    partial class HisMemaGroupGet : BusinessBase
    {
        internal List<V_HIS_MEMA_GROUP> GetView(HisMemaGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMemaGroupDAO.GetView(filter.Query(), param);
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
