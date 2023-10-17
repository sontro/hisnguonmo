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
        internal List<V_HIS_MEDICINE_SERVICE> GetView(HisMedicineServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineServiceDAO.GetView(filter.Query(), param);
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
