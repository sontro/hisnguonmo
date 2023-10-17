using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFund
{
    partial class HisFundGet : BusinessBase
    {
        internal List<V_HIS_FUND> GetView(HisFundViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFundDAO.GetView(filter.Query(), param);
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
