using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Moba;
using MOS.MANAGER.HisImpMest.Moba.Blood;
using MOS.MANAGER.HisImpMest.Moba.Depa;
using MOS.MANAGER.HisImpMest.Moba.OutPres;
using MOS.MANAGER.HisImpMest.Moba.Pres;
using MOS.MANAGER.HisImpMest.Moba.PresCabinet;
using MOS.MANAGER.HisImpMest.Moba.Sale;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    public partial class HisImpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisImpMestResultSDO> MobaPresCreate(HisImpMestMobaPresSDO data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestMobaPresCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestResultSDO> MobaBloodCreate(HisImpMestMobaBloodSDO data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestMobaBloodCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestResultSDO> MobaDepaCreate(HisImpMestMobaDepaSDO data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestMobaDepaCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestResultSDO> MobaSaleCreate(HisImpMestMobaSaleSDO data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestMobaSaleCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestResultSDO> MobaOutPresCreate(HisImpMestMobaOutPresSDO data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestMobaOutPresCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HisImpMestResultSDO>> MobaPresCabinetCreate(HisImpMestMobaPresCabinetSDO data)
        {
            ApiResultObject<List<HisImpMestResultSDO>> result = new ApiResultObject<List<HisImpMestResultSDO>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HisImpMestResultSDO> resultData = null;
                if (valid)
                {
                    new HisImpMestMobaPresCabinetCreate(param).Run(data, ref resultData);
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
