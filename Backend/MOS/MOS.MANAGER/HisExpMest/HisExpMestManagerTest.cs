using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Depa;
using MOS.MANAGER.HisExpMest.Test;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<ExpMestTestResultSDO> TestCreate(ExpMestTestSDO data)
        {
            ApiResultObject<ExpMestTestResultSDO> result = new ApiResultObject<ExpMestTestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ExpMestTestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestTestCreate(param).Run(data, ref resultData);
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
