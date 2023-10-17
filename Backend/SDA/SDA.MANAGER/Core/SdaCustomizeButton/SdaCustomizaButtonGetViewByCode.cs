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
        internal V_SDA_CUSTOMIZA_BUTTON GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new SdaCustomizaButtonViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_SDA_CUSTOMIZA_BUTTON GetViewByCode(string code, SdaCustomizaButtonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.SdaCustomizaButtonDAO.GetViewByCode(code, filter.Query());
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
