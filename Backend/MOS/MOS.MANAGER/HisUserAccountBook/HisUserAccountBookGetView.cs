using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserAccountBook
{
    partial class HisUserAccountBookGet : BusinessBase
    {
        internal List<V_HIS_USER_ACCOUNT_BOOK> GetView(HisUserAccountBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserAccountBookDAO.GetView(filter.Query(), param);
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
