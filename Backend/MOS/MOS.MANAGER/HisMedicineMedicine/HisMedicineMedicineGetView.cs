using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMedicine
{
    partial class HisMedicineMedicineGet : BusinessBase
    {
        internal List<V_HIS_MEDICINE_MEDICINE> GetView(HisMedicineMedicineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineMedicineDAO.GetView(filter.Query(), param);
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
