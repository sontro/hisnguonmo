using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthorSystem
{
    partial class AcsAuthorSystemGet : BusinessBase
    {
        internal AcsAuthorSystemGet()
            : base()
        {

        }

        internal AcsAuthorSystemGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<ACS_AUTHOR_SYSTEM> Get(AcsAuthorSystemFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthorSystemDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_AUTHOR_SYSTEM GetById(long id)
        {
            try
            {
                return GetById(id, new AcsAuthorSystemFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_AUTHOR_SYSTEM GetById(long id, AcsAuthorSystemFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthorSystemDAO.GetById(id, filter.Query());
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
