using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServDeposit
{
    partial class HisSereServDepositGet : BusinessBase
    {
        internal List<V_HIS_SERE_SERV_DEPOSIT> GetView(HisSereServDepositViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDepositDAO.GetView(filter.Query(), param);
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
