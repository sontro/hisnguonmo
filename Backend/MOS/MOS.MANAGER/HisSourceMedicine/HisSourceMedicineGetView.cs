using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSourceMedicine
{
    partial class HisSourceMedicineGet : BusinessBase
    {
        internal List<V_HIS_SOURCE_MEDICINE> GetView(HisSourceMedicineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSourceMedicineDAO.GetView(filter.Query(), param);
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
