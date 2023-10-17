using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Depa;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisExpMestResultSDO> DepaCreate(HisExpMestDepaSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestDepaCreate(param).Create(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestResultSDO> DepaUpdate(HisExpMestDepaSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestDepaUpdate(param).Update(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
