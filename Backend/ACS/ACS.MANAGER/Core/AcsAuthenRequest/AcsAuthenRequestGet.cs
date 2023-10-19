using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthenRequest
{
    partial class AcsAuthenRequestGet : BusinessBase
    {
        internal AcsAuthenRequestGet()
            : base()
        {

        }

        internal AcsAuthenRequestGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<ACS_AUTHEN_REQUEST> Get(AcsAuthenRequestFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthenRequestDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_AUTHEN_REQUEST GetById(long id)
        {
            try
            {
                return GetById(id, new AcsAuthenRequestFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_AUTHEN_REQUEST GetById(long id, AcsAuthenRequestFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthenRequestDAO.GetById(id, filter.Query());
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
