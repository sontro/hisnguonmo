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
        internal HIS_MEDICINE_SERVICE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMedicineServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_SERVICE GetByCode(string code, HisMedicineServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineServiceDAO.GetByCode(code, filter.Query());
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
