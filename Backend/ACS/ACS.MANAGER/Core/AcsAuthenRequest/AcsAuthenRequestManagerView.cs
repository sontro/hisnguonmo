using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthenRequest
{
    public partial class AcsAuthenRequestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_ACS_AUTHEN_REQUEST>> GetView(AcsAuthenRequestViewFilterQuery filter)
        {
            ApiResultObject<List<V_ACS_AUTHEN_REQUEST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_ACS_AUTHEN_REQUEST> resultData = null;
                if (valid)
                {
                    resultData = new AcsAuthenRequestGet(param).GetView(filter);
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
