using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomGroup
{
    partial class HisRoomGroupGet : BusinessBase
    {
        internal List<V_HIS_ROOM_GROUP> GetView(HisRoomGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomGroupDAO.GetView(filter.Query(), param);
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
