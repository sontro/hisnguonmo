using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Manu;
using MOS.MANAGER.HisImpMest.Manu.Create;
using MOS.MANAGER.HisImpMest.Manu.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    public partial class HisImpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisImpMestManuSDO> ManuCreate(HisImpMestManuSDO data)
        {
            ApiResultObject<HisImpMestManuSDO> result = new ApiResultObject<HisImpMestManuSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestManuSDO resultData = null;
                if (valid)
                {
                    new HisImpMestManuCreate(param).Create(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HisImpMestManuSDO> ManuUpdate(HisImpMestManuSDO data)
        {
            ApiResultObject<HisImpMestManuSDO> result = new ApiResultObject<HisImpMestManuSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestManuSDO resultData = null;
                if (valid)
                {
                    new HisImpMestManuUpdate(param).Update(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_IMP_MEST> ManuUpdateInfo(HIS_IMP_MEST data)
        {
            ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST resultData = null;
                if (valid)
                {
                    new HisImpMestManuUpdateInfo(param).ManuUpdateInfo(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
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
