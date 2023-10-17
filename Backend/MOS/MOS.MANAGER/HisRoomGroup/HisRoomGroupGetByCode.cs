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
        internal HIS_ROOM_GROUP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRoomGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ROOM_GROUP GetByCode(string code, HisRoomGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomGroupDAO.GetByCode(code, filter.Query());
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
