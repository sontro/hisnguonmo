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
        internal List<V_HIS_DOSAGE_FORM> GetView(HisDosageFormViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDosageFormDAO.GetView(filter.Query(), param);
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
