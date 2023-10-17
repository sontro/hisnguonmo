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
        internal HIS_ROOM_TYPE_MODULE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRoomTypeModuleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ROOM_TYPE_MODULE GetByCode(string code, HisRoomTypeModuleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomTypeModuleDAO.GetByCode(code, filter.Query());
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
