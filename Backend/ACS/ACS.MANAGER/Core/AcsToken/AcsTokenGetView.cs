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
        internal List<V_ACS_TOKEN> GetView(AcsTokenViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsTokenDAO.GetView(filter.Query(), param);
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
