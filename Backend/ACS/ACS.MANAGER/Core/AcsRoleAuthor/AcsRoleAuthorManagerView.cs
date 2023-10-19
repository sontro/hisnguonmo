using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsRoleAuthor
{
    public partial class AcsRoleAuthorManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_ACS_ROLE_AUTHOR>> GetView(AcsRoleAuthorViewFilterQuery filter)
        {
            ApiResultObject<List<V_ACS_ROLE_AUTHOR>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_ACS_ROLE_AUTHOR> resultData = null;
                if (valid)
                {
                    resultData = new AcsRoleAuthorGet(param).GetView(filter);
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
