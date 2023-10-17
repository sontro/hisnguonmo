using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomSaro
{
    public partial class HisRoomSaroDAO : EntityBase
    {
        public List<V_HIS_ROOM_SARO> GetView(HisRoomSaroSO search, CommonParam param)
        {
            List<V_HIS_ROOM_SARO> result = new List<V_HIS_ROOM_SARO>();
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

        public V_HIS_ROOM_SARO GetViewById(long id, HisRoomSaroSO search)
        {
            V_HIS_ROOM_SARO result = null;

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
