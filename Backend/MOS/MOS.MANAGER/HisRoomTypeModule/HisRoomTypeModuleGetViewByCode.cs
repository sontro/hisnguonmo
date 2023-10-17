using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomTypeModule
{
    partial class HisRoomTypeModuleGet : BusinessBase
    {
        internal V_HIS_ROOM_TYPE_MODULE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRoomTypeModuleViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ROOM_TYPE_MODULE GetViewByCode(string code, HisRoomTypeModuleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomTypeModuleDAO.GetViewByCode(code, filter.Query());
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
