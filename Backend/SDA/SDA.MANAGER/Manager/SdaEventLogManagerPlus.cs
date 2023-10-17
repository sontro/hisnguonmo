using SDA.MANAGER.Core.SdaEventLog;
using Inventec.Core;
using System;
using SDA.SDO;
using System.Collections.Generic;

namespace SDA.MANAGER.Manager
{
    public partial class SdaEventLogManager : ManagerBase
    {    
        public object Create(SdaEventLogSDO data)
        {
            object result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaEventLogBO bo = new SdaEventLogBO();
                if (bo.CreateSDO(data))
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

        public object CreateList(List<SDA.SDO.SdaEventLogSDO> data)
        {
            object result = false;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("Begin process method CreateList");
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaEventLogBO bo = new SdaEventLogBO();
                if (bo.CreateListSDO(data))
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
