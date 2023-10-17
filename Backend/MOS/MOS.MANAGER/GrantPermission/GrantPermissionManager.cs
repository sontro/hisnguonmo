using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.GrantPermission
{
    public partial class GrantPermissionManager : BusinessBase
    {
        public GrantPermissionManager()
            : base()
        {

        }

        public GrantPermissionManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<bool> GrantUpdate(GrantPermissionSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new GrantUpdate(param).Run(data);
                }
                result = this.PackResult(isSuccess, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }
    }
}
