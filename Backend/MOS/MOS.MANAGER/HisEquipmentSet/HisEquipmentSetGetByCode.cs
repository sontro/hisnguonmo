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
        internal HIS_EQUIPMENT_SET GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEquipmentSetFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EQUIPMENT_SET GetByCode(string code, HisEquipmentSetFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEquipmentSetDAO.GetByCode(code, filter.Query());
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
