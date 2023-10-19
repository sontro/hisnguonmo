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
        internal V_ACS_AUTHOR_SYSTEM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new AcsAuthorSystemViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_ACS_AUTHOR_SYSTEM GetViewByCode(string code, AcsAuthorSystemViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthorSystemDAO.GetViewByCode(code, filter.Query());
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
