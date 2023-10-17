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
        internal V_HIS_ROOM_GROUP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRoomGroupViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ROOM_GROUP GetViewByCode(string code, HisRoomGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomGroupDAO.GetViewByCode(code, filter.Query());
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
