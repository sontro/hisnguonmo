using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    partial class HisSesePtttMethodGet : BusinessBase
    {
        internal List<V_HIS_SESE_PTTT_METHOD> GetView(HisSesePtttMethodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSesePtttMethodDAO.GetView(filter.Query(), param);
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
