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
        internal V_HIS_TREATMENT_END_TYPE_EXT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisTreatmentEndTypeExtViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_END_TYPE_EXT GetViewByCode(string code, HisTreatmentEndTypeExtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentEndTypeExtDAO.GetViewByCode(code, filter.Query());
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
