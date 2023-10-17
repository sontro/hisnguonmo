using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEquipmentSet
{
    partial class HisEquipmentSetGet : BusinessBase
    {
        internal List<V_HIS_EQUIPMENT_SET> GetView(HisEquipmentSetViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEquipmentSetDAO.GetView(filter.Query(), param);
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
