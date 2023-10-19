using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsRoleAuthor
{
    partial class AcsRoleAuthorGet : BusinessBase
    {
        internal List<V_ACS_ROLE_AUTHOR> GetView(AcsRoleAuthorViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsRoleAuthorDAO.GetView(filter.Query(), param);
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
