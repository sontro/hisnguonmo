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
        internal V_ACS_AUTHEN_REQUEST GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new AcsAuthenRequestViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_ACS_AUTHEN_REQUEST GetViewByCode(string code, AcsAuthenRequestViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthenRequestDAO.GetViewByCode(code, filter.Query());
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
