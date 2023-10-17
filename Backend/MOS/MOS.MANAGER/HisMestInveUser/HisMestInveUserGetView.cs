using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestInveUser
{
    partial class HisMestInveUserGet : BusinessBase
    {
        internal List<V_HIS_MEST_INVE_USER> GetView(HisMestInveUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestInveUserDAO.GetView(filter.Query(), param);
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
