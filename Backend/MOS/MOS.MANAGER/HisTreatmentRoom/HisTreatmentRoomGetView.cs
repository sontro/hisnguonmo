using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentRoom
{
    partial class HisTreatmentRoomGet : BusinessBase
    {
        internal List<V_HIS_TREATMENT_ROOM> GetView(HisTreatmentRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentRoomDAO.GetView(filter.Query(), param);
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
