using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.SdaConfig
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

        public ApiResultObject<List<SDA_CONFIG>> Get(SdaConfigFilter filter)
        {
            ApiResultObject<List<SDA_CONFIG>> result = null;
            
            #region Logging Input Data
            try
            {
                Input = LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => filter), filter) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param);
            }
            catch (Exception ex)
            {
                
                LogSystem.Error(ex);
            }
            #endregion

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
                result = this.PackResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            #region Logging
            if (param.HasException)
            {
                
                LogInOut();
            }
            #endregion
            TroubleCheck(); return result;
        }
    }
}
