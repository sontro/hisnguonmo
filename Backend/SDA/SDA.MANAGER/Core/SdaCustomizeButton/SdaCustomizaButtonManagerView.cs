using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.SdaCustomizaButton
{
    public partial class SdaCustomizaButtonManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_SDA_CUSTOMIZA_BUTTON>> GetView(SdaCustomizaButtonViewFilterQuery filter)
        {
            ApiResultObject<List<V_SDA_CUSTOMIZA_BUTTON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_SDA_CUSTOMIZA_BUTTON> resultData = null;
                if (valid)
                {
                    resultData = new SdaCustomizaButtonGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
