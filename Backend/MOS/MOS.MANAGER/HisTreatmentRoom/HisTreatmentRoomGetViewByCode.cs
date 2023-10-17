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
        internal V_HIS_TREATMENT_ROOM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisTreatmentRoomViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_ROOM GetViewByCode(string code, HisTreatmentRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentRoomDAO.GetViewByCode(code, filter.Query());
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
