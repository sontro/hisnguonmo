using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.SdaCustomizeButton
{
    partial class SdaCustomizeButtonGet : BusinessBase
    {
        internal SDA_CUSTOMIZE_BUTTON GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new SdaCustomizeButtonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal SDA_CUSTOMIZE_BUTTON GetByCode(string code, SdaCustomizeButtonFilterQuery filter)
        {
            try
            {
                return DAOWorker.SdaCustomizeButtonDAO.GetByCode(code, filter.Query());
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
