using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMaterialType
{
    public partial class HisBidMaterialTypeManager : BusinessBase
    {
		[Logger]
        public ApiResultObject<List<HisBidMaterialTypeSDO>> GetSdoByBidId(long bidId)
        {
            ApiResultObject<List<HisBidMaterialTypeSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisBidMaterialTypeSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisBidMaterialTypeGet(param).GetSdoByBidId(bidId);
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
