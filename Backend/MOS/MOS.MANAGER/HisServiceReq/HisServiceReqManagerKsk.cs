using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Ksk.KskExecute;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<KskExecuteResultSDO> KskExecute(HisServiceReqKskExecuteSDO data)
        {
            ApiResultObject<KskExecuteResultSDO> result = new ApiResultObject<KskExecuteResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                KskExecuteResultSDO resultData = null;
                bool rs = false;
                if (valid)
                {
                    rs = new HisServiceReqKskExecute(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, rs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<KskExecuteResultV2SDO> KskExecuteV2(HisServiceReqKskExecuteV2SDO data)
        {
            ApiResultObject<KskExecuteResultV2SDO> result = new ApiResultObject<KskExecuteResultV2SDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                KskExecuteResultV2SDO resultData = null;
                bool rs = false;
                if (valid)
                {
                    rs = new HisServiceReqKskExecuteV2(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, rs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }
    }
}
