using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMixedMedicine
{
    partial class HisMixedMedicineGet : BusinessBase
    {
        internal List<V_HIS_MIXED_MEDICINE> GetView(HisMixedMedicineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMixedMedicineDAO.GetView(filter.Query(), param);
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
