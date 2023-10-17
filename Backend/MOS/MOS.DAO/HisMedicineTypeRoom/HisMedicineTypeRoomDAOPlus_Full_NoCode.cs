using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeRoom
{
    public partial class HisMedicineTypeRoomDAO : EntityBase
    {
        public List<V_HIS_MEDICINE_TYPE_ROOM> GetView(HisMedicineTypeRoomSO search, CommonParam param)
        {
            List<V_HIS_MEDICINE_TYPE_ROOM> result = new List<V_HIS_MEDICINE_TYPE_ROOM>();
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

        public V_HIS_MEDICINE_TYPE_ROOM GetViewById(long id, HisMedicineTypeRoomSO search)
        {
            V_HIS_MEDICINE_TYPE_ROOM result = null;

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
