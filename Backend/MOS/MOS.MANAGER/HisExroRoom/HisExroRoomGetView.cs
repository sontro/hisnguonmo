using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExroRoom
{
    partial class HisExroRoomGet : BusinessBase
    {
        internal List<V_HIS_EXRO_ROOM> GetView(HisExroRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExroRoomDAO.GetView(filter.Query(), param);
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
