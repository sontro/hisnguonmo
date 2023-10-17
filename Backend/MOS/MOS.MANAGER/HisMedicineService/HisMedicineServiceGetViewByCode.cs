using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineService
{
    partial class HisMedicineServiceGet : BusinessBase
    {
        internal V_HIS_MEDICINE_SERVICE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMedicineServiceViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDICINE_SERVICE GetViewByCode(string code, HisMedicineServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineServiceDAO.GetViewByCode(code, filter.Query());
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
