using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomTime
{
    partial class HisRoomTimeGet : BusinessBase
    {
        internal List<V_HIS_ROOM_TIME> GetView(HisRoomTimeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomTimeDAO.GetView(filter.Query(), param);
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
