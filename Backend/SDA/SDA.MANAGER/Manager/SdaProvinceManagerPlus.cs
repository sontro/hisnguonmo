using SDA.MANAGER.Core.SdaProvince;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Manager
{
    public partial class SdaProvinceManager : ManagerBase
    {
        public bool Delete(object data)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaProvinceBO bo = new SdaProvinceBO();
                if (bo.DeleteWithDelReference(data))
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

        public List<object> GetDynamic(object data)
        {
            List<object> result = default(List<object>);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaProvinceBO bo = new SdaProvinceBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.GetDynamic(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(List<object>);
            }
            return result;
        }
    }
}
