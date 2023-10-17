using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestMaterial
{
    public partial class HisImpMestMaterialManager : BusinessBase
    {
        public HisImpMestMaterialManager()
            : base()
        {

        }

        public HisImpMestMaterialManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_IMP_MEST_MATERIAL>> Get(HisImpMestMaterialFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_MATERIAL>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMaterialGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>> GetView(HisImpMestMaterialViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMaterialGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HisImpMestMaterialWithInStockAmountSDO>> GetViewWithInStockAmount(long impMestId)
        {
            ApiResultObject<List<HisImpMestMaterialWithInStockAmountSDO>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisImpMestMaterialWithInStockAmountSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMaterialGet(param).GetViewWithInStockAmount(impMestId);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>> GetViewByAggrImpMestIdAndGroupByMaterial(long aggrImpMestId)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_IMP_MEST_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMaterialGet(param).GetViewByAggrImpMestIdAndGroupByMaterial(aggrImpMestId);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>> GetViewAndIncludeChildrenByImpMestId(long expMestId)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_IMP_MEST_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMaterialGet(param).GetViewAndIncludeChildrenByImpMestId(expMestId);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        //Review lai
        //[Logger]
        //public ApiResultObject<HIS_IMP_MEST_MATERIAL> Create(HIS_IMP_MEST_MATERIAL data)
        //{
        //    ApiResultObject<HIS_IMP_MEST_MATERIAL> result = new ApiResultObject<HIS_IMP_MEST_MATERIAL>(null);
            
        //    try
        //    {
        //        bool valid = true;
        //        valid = valid && IsNotNull(param);
        //        valid = valid && IsNotNull(data);
        //        HIS_IMP_MEST_MATERIAL resultData = null;
        //        if (valid && new HisImpMestMaterialCreate(param).Create(data))
        //        {
        //            resultData = data;
        //        }
        //        result = this.PackSingleResult(resultData);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //        param.HasException = true;
        //    }

        //    return result;
        //}

        //[Logger]
        //public ApiResultObject<HIS_IMP_MEST_MATERIAL> Update(HIS_IMP_MEST_MATERIAL data)
        //{
        //    ApiResultObject<HIS_IMP_MEST_MATERIAL> result = new ApiResultObject<HIS_IMP_MEST_MATERIAL>(null);
            
        //    try
        //    {
        //        bool valid = true;
        //        valid = valid && IsNotNull(param);
        //        valid = valid && IsNotNull(data);
        //        HIS_IMP_MEST_MATERIAL resultData = null;
        //        if (valid && new HisImpMestMaterialUpdate(param).Update(data))
        //        {
        //            resultData = data;
        //        }
        //        result = this.PackSingleResult(resultData);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //        param.HasException = true;
        //    }

        //    return result;
        //}

        //[Logger]
        //public ApiResultObject<HIS_IMP_MEST_MATERIAL> ChangeLock(HIS_IMP_MEST_MATERIAL data)
        //{
        //    ApiResultObject<HIS_IMP_MEST_MATERIAL> result = new ApiResultObject<HIS_IMP_MEST_MATERIAL>(null);
            
        //    try
        //    {
        //        bool valid = true;
        //        valid = valid && IsNotNull(param);
        //        valid = valid && IsGreaterThanZero(data.ID);
        //        HIS_IMP_MEST_MATERIAL resultData = null;
        //        if (valid && new HisImpMestMaterialLock(param).ChangeLock(data))
        //        {
        //            resultData = data;
        //        }
        //        result = this.PackSingleResult(resultData);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //        param.HasException = true;
        //    }

        //    return result;
        //}
    }
}
