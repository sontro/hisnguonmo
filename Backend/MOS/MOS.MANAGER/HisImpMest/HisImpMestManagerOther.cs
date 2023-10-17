using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Other;
using MOS.MANAGER.HisImpMest.Other.Donation.Create;
using MOS.MANAGER.HisImpMest.Other.Donation.Update;
using MOS.MANAGER.HisImpMest.Other.Init.Create;
using MOS.MANAGER.HisImpMest.Other.Init.Update;
using MOS.MANAGER.HisImpMest.Other.Inve.Create;
using MOS.MANAGER.HisImpMest.Other.Inve.Update;
using MOS.MANAGER.HisImpMest.Other.Other.Create;
using MOS.MANAGER.HisImpMest.Other.Other.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    public partial class HisImpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisImpMestInitSDO> InitCreate(HisImpMestInitSDO data)
        {
            ApiResultObject<HisImpMestInitSDO> result = new ApiResultObject<HisImpMestInitSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestInitSDO resultData = null;
                if (valid)
                {
                    new HisImpMestInitCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestInveSDO> InveCreate(HisImpMestInveSDO data)
        {
            ApiResultObject<HisImpMestInveSDO> result = new ApiResultObject<HisImpMestInveSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestInveSDO resultData = null;
                if (valid)
                {
                    new HisImpMestInveCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestOtherSDO> OtherCreate(HisImpMestOtherSDO data)
        {
            ApiResultObject<HisImpMestOtherSDO> result = new ApiResultObject<HisImpMestOtherSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestOtherSDO resultData = null;
                if (valid)
                {
                    new HisImpMestOtherCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestInitSDO> InitUpdate(HisImpMestInitSDO data)
        {
            ApiResultObject<HisImpMestInitSDO> result = new ApiResultObject<HisImpMestInitSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestInitSDO resultData = null;
                if (valid)
                {
                    new HisImpMestInitUpdate(param).Update(data, ref resultData);
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
        public ApiResultObject<HisImpMestInveSDO> InveUpdate(HisImpMestInveSDO data)
        {
            ApiResultObject<HisImpMestInveSDO> result = new ApiResultObject<HisImpMestInveSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestInveSDO resultData = null;
                if (valid)
                {
                    new HisImpMestInveUpdate(param).Update(data, ref resultData);
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
        public ApiResultObject<HisImpMestOtherSDO> OtherUpdate(HisImpMestOtherSDO data)
        {
            ApiResultObject<HisImpMestOtherSDO> result = new ApiResultObject<HisImpMestOtherSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestOtherSDO resultData = null;
                if (valid)
                {
                    new HisImpMestOtherUpdate(param).Update(data, ref resultData);
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
        public ApiResultObject<HisImpMestDonationSDO> DonationCreate(HisImpMestDonationSDO data)
        {
            ApiResultObject<HisImpMestDonationSDO> result = new ApiResultObject<HisImpMestDonationSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestDonationSDO resultData = null;
                if (valid)
                {
                    new HisImpMestDonationCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisImpMestDonationSDO> DonationUpdate(HisImpMestDonationSDO data)
        {
            ApiResultObject<HisImpMestDonationSDO> result = new ApiResultObject<HisImpMestDonationSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestDonationSDO resultData = null;
                if (valid)
                {
                    new HisImpMestDonationUpdate(param).Update(data, ref resultData);
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
