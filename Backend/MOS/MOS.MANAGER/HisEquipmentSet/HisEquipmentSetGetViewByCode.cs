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
        internal V_HIS_EQUIPMENT_SET GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEquipmentSetViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EQUIPMENT_SET GetViewByCode(string code, HisEquipmentSetViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEquipmentSetDAO.GetViewByCode(code, filter.Query());
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
