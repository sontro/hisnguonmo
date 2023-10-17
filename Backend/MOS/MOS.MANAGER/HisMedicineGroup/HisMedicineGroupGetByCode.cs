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
        internal HIS_MEDICINE_GROUP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMedicineGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_GROUP GetByCode(string code, HisMedicineGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineGroupDAO.GetByCode(code, filter.Query());
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
