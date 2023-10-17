using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDosageForm
{
    partial class HisDosageFormGet : BusinessBase
    {
        internal V_HIS_DOSAGE_FORM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDosageFormViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DOSAGE_FORM GetViewByCode(string code, HisDosageFormViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDosageFormDAO.GetViewByCode(code, filter.Query());
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
