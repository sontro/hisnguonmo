using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWelfareType
{
    partial class HisWelfareTypeGet : BusinessBase
    {
        internal List<V_HIS_WELFARE_TYPE> GetView(HisWelfareTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWelfareTypeDAO.GetView(filter.Query(), param);
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
