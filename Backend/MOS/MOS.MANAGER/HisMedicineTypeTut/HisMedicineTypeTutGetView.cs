using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutGet : BusinessBase
    {
        internal List<V_HIS_MEDICINE_TYPE_TUT> GetView(HisMedicineTypeTutViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineTypeTutDAO.GetView(filter.Query(), param);
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
