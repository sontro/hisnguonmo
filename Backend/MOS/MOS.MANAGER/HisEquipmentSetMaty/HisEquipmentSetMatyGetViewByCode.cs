using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyGet : BusinessBase
    {
        internal V_HIS_EQUIPMENT_SET_MATY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEquipmentSetMatyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EQUIPMENT_SET_MATY GetViewByCode(string code, HisEquipmentSetMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEquipmentSetMatyDAO.GetViewByCode(code, filter.Query());
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
