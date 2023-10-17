using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.SdaConfig
{
    public class SdaConfigManager : BusinessBase
    {
        public SdaConfigManager()
            : base()
        {

        }

        public SdaConfigManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<SDA_CONFIG>> Get(SdaConfigFilter filter)
        {
            ApiResultObject<List<SDA_CONFIG>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<SDA_CONFIG> resultData = null;
                if (valid)
                {
                    resultData = new SdaConfigGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
