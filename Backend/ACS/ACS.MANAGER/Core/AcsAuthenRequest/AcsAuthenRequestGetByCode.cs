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
        internal ACS_AUTHEN_REQUEST GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new AcsAuthenRequestFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_AUTHEN_REQUEST GetByCode(string code, AcsAuthenRequestFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthenRequestDAO.GetByCode(code, filter.Query());
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
