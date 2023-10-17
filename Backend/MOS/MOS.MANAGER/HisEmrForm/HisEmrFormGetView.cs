using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrForm
{
    partial class HisEmrFormGet : BusinessBase
    {
        internal List<V_HIS_EMR_FORM> GetView(HisEmrFormViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrFormDAO.GetView(filter.Query(), param);
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
