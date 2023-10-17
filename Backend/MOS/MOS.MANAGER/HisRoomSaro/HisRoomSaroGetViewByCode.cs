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
        internal V_HIS_ROOM_SARO GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRoomSaroViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ROOM_SARO GetViewByCode(string code, HisRoomSaroViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomSaroDAO.GetViewByCode(code, filter.Query());
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
