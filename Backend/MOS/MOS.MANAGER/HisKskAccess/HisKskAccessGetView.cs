using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    partial class HisKskAccessGet : BusinessBase
    {
        internal List<V_HIS_KSK_ACCESS> GetView(HisKskAccessViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskAccessDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_KSK_ACCESS> GetViewByLoginName(string loginName)
        {
            try
            {
                HisKskAccessViewFilterQuery filter = new HisKskAccessViewFilterQuery();
                filter.LOGINNAME = loginName;
                return this.GetView(filter);
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
