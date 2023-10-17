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
        internal HisRoomTimeGet()
            : base()
        {

        }

        internal HisRoomTimeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ROOM_TIME> Get(HisRoomTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomTimeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ROOM_TIME GetById(long id)
        {
            try
            {
                return GetById(id, new HisRoomTimeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ROOM_TIME GetById(long id, HisRoomTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomTimeDAO.GetById(id, filter.Query());
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
