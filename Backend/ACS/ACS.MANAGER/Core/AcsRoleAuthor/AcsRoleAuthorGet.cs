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
        internal AcsRoleAuthorGet()
            : base()
        {

        }

        internal AcsRoleAuthorGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<ACS_ROLE_AUTHOR> Get(AcsRoleAuthorFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsRoleAuthorDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_ROLE_AUTHOR GetById(long id)
        {
            try
            {
                return GetById(id, new AcsRoleAuthorFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_ROLE_AUTHOR GetById(long id, AcsRoleAuthorFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsRoleAuthorDAO.GetById(id, filter.Query());
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
