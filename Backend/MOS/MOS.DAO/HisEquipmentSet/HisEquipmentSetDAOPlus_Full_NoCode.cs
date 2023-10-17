using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEquipmentSet
{
    public partial class HisEquipmentSetDAO : EntityBase
    {
        public List<V_HIS_EQUIPMENT_SET> GetView(HisEquipmentSetSO search, CommonParam param)
        {
            List<V_HIS_EQUIPMENT_SET> result = new List<V_HIS_EQUIPMENT_SET>();
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

        public V_HIS_EQUIPMENT_SET GetViewById(long id, HisEquipmentSetSO search)
        {
            V_HIS_EQUIPMENT_SET result = null;

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
