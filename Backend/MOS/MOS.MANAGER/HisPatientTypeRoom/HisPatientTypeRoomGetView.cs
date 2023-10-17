using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomGet : BusinessBase
    {
        internal List<V_HIS_PATIENT_TYPE_ROOM> GetView(HisPatientTypeRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientTypeRoomDAO.GetView(filter.Query(), param);
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
