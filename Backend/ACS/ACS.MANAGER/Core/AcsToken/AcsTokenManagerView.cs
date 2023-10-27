using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsToken
{
    public partial class AcsTokenManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_ACS_TOKEN>> GetView(AcsTokenViewFilterQuery filter)
        {
            ApiResultObject<List<V_ACS_TOKEN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_ACS_TOKEN> resultData = null;
                if (valid)
                {
                    resultData = new AcsTokenGet(param).GetView(filter);
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
