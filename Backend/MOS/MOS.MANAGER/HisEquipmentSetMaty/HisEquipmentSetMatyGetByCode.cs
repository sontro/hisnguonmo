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
        internal HIS_EQUIPMENT_SET_MATY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEquipmentSetMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EQUIPMENT_SET_MATY GetByCode(string code, HisEquipmentSetMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEquipmentSetMatyDAO.GetByCode(code, filter.Query());
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
