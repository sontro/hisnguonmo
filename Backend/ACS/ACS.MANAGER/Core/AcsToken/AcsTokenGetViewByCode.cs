using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsToken
{
    partial class AcsTokenGet : BusinessBase
    {
        internal V_ACS_TOKEN GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new AcsTokenViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_ACS_TOKEN GetViewByCode(string code, AcsTokenViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsTokenDAO.GetViewByCode(code, filter.Query());
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
