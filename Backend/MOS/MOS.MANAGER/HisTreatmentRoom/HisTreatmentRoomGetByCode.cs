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
        internal HIS_TREATMENT_ROOM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTreatmentRoomFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_ROOM GetByCode(string code, HisTreatmentRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentRoomDAO.GetByCode(code, filter.Query());
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
