using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthorSystem
{
    public partial class AcsAuthorSystemManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_ACS_AUTHOR_SYSTEM>> GetView(AcsAuthorSystemViewFilterQuery filter)
        {
            ApiResultObject<List<V_ACS_AUTHOR_SYSTEM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_ACS_AUTHOR_SYSTEM> resultData = null;
                if (valid)
                {
                    resultData = new AcsAuthorSystemGet(param).GetView(filter);
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
