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
        internal HisRoomSaroGet()
            : base()
        {

        }

        internal HisRoomSaroGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ROOM_SARO> Get(HisRoomSaroFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomSaroDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ROOM_SARO GetById(long id)
        {
            try
            {
                return GetById(id, new HisRoomSaroFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ROOM_SARO GetById(long id, HisRoomSaroFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomSaroDAO.GetById(id, filter.Query());
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
