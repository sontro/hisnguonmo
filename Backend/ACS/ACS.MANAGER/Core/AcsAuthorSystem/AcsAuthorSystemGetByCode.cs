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
        internal ACS_AUTHOR_SYSTEM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new AcsAuthorSystemFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_AUTHOR_SYSTEM GetByCode(string code, AcsAuthorSystemFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsAuthorSystemDAO.GetByCode(code, filter.Query());
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
