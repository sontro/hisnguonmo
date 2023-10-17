using SDA.MANAGER.Core.SdaTrouble;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Manager
{
    public partial class SdaTroubleManager : ManagerBase
    {
        public object Create(object data)
        {
            object result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaTroubleBO bo = new SdaTroubleBO();
                if (bo.CreateByMessage(data))
                {
                    result = true;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        public object Scan()
        {
            object result = false;
            try
            {
                SdaTroubleBO bo = new SdaTroubleBO();
                if (bo.Scan())
                {
                    result = true;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
