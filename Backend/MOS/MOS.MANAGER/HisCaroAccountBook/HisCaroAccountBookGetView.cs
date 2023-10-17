using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroAccountBook
{
    partial class HisCaroAccountBookGet : BusinessBase
    {
        internal List<V_HIS_CARO_ACCOUNT_BOOK> GetView(HisCaroAccountBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCaroAccountBookDAO.GetView(filter.Query(), param);
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
