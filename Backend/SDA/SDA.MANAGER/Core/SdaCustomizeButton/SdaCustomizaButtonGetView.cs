using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.SdaCustomizaButton
{
    partial class SdaCustomizaButtonGet : BusinessBase
    {
        internal List<V_SDA_CUSTOMIZA_BUTTON> GetView(SdaCustomizaButtonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.SdaCustomizaButtonDAO.GetView(filter.Query(), param);
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
