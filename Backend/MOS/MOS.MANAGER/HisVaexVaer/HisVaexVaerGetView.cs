using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaexVaer
{
    partial class HisVaexVaerGet : BusinessBase
    {
        internal List<V_HIS_VAEX_VAER> GetView(HisVaexVaerViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaexVaerDAO.GetView(filter.Query(), param);
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
