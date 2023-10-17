using SAR.MANAGER.Core.SdaTrouble;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Manager
{
    public partial class SdaTroubleManager : ManagerBase
    {
        public SdaTroubleManager(CommonParam param)
            : base(param)
        {

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
