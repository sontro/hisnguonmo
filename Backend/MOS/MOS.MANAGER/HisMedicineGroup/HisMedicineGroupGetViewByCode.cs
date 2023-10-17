using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineGroup
{
    partial class HisMedicineGroupGet : BusinessBase
    {
        internal V_HIS_MEDICINE_GROUP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMedicineGroupViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDICINE_GROUP GetViewByCode(string code, HisMedicineGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineGroupDAO.GetViewByCode(code, filter.Query());
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
