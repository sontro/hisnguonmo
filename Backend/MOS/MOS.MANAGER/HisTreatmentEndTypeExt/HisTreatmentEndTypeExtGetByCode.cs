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
        internal HIS_TREATMENT_END_TYPE_EXT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTreatmentEndTypeExtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_END_TYPE_EXT GetByCode(string code, HisTreatmentEndTypeExtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentEndTypeExtDAO.GetByCode(code, filter.Query());
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
