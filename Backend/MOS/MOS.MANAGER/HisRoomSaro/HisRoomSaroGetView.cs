using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomSaro
{
    partial class HisRoomSaroGet : BusinessBase
    {
        internal List<V_HIS_ROOM_SARO> GetView(HisRoomSaroViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomSaroDAO.GetView(filter.Query(), param);
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
