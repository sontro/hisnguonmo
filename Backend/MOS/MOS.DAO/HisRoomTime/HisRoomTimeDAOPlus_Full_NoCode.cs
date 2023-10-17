using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomTime
{
    public partial class HisRoomTimeDAO : EntityBase
    {
        public List<V_HIS_ROOM_TIME> GetView(HisRoomTimeSO search, CommonParam param)
        {
            List<V_HIS_ROOM_TIME> result = new List<V_HIS_ROOM_TIME>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_ROOM_TIME GetViewById(long id, HisRoomTimeSO search)
        {
            V_HIS_ROOM_TIME result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
