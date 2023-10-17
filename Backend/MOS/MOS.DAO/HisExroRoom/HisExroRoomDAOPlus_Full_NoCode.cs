using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExroRoom
{
    public partial class HisExroRoomDAO : EntityBase
    {
        public List<V_HIS_EXRO_ROOM> GetView(HisExroRoomSO search, CommonParam param)
        {
            List<V_HIS_EXRO_ROOM> result = new List<V_HIS_EXRO_ROOM>();
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

        public V_HIS_EXRO_ROOM GetViewById(long id, HisExroRoomSO search)
        {
            V_HIS_EXRO_ROOM result = null;

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
