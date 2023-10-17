using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtGet : BusinessBase
    {
        internal List<V_HIS_TREATMENT_END_TYPE_EXT> GetView(HisTreatmentEndTypeExtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentEndTypeExtDAO.GetView(filter.Query(), param);
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
