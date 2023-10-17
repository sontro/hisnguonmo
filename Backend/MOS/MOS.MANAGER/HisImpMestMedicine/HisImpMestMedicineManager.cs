using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestMedicine
{
    public partial class HisImpMestMedicineManager : BusinessBase
    {
        public HisImpMestMedicineManager()
            : base()
        {

        }

        public HisImpMestMedicineManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_IMP_MEST_MEDICINE>> Get(HisImpMestMedicineFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_MEDICINE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMedicineGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>> GetView(HisImpMestMedicineViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMedicineGet(param).GetView(filter);
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
        public ApiResultObject<List<HisImpMestMedicineWithInStockAmountSDO>> GetViewWithInStockAmount(long impMestId)
        {
            ApiResultObject<List<HisImpMestMedicineWithInStockAmountSDO>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisImpMestMedicineWithInStockAmountSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMedicineGet(param).GetViewWithInStockAmount(impMestId);
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
        public ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>> GetViewByAggrImpMestIdAndGroupByMedicine(long aggrImpMestId)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_IMP_MEST_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMedicineGet(param).GetViewByAggrImpMestIdAndGroupByMedicine(aggrImpMestId);
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
        public ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>> GetViewAndIncludeChildrenByImpMestId(long impMestId)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_IMP_MEST_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMedicineGet(param).GetViewAndIncludeChildrenByImpMestId(impMestId);
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
        //public ApiResultObject<HIS_IMP_MEST_MEDICINE> Create(HIS_IMP_MEST_MEDICINE data)
        //{
        //    ApiResultObject<HIS_IMP_MEST_MEDICINE> result = new ApiResultObject<HIS_IMP_MEST_MEDICINE>(null);
            
        //    try
        //    {
        //        bool valid = true;
        //        valid = valid && IsNotNull(param);
        //        valid = valid && IsNotNull(data);
        //        HIS_IMP_MEST_MEDICINE resultData = null;
        //        if (valid && new HisImpMestMedicineCreate(param).Create(data))
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
        //public ApiResultObject<HIS_IMP_MEST_MEDICINE> Update(HIS_IMP_MEST_MEDICINE data)
        //{
        //    ApiResultObject<HIS_IMP_MEST_MEDICINE> result = new ApiResultObject<HIS_IMP_MEST_MEDICINE>(null);
            
        //    try
        //    {
        //        bool valid = true;
        //        valid = valid && IsNotNull(param);
        //        valid = valid && IsNotNull(data);
        //        HIS_IMP_MEST_MEDICINE resultData = null;
        //        if (valid && new HisImpMestMedicineUpdate(param).Update(data))
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
        //public ApiResultObject<HIS_IMP_MEST_MEDICINE> ChangeLock(HIS_IMP_MEST_MEDICINE data)
        //{
        //    ApiResultObject<HIS_IMP_MEST_MEDICINE> result = new ApiResultObject<HIS_IMP_MEST_MEDICINE>(null);
            
        //    try
        //    {
        //        bool valid = true;
        //        valid = valid && IsNotNull(param);
        //        valid = valid && IsGreaterThanZero(data.ID);
        //        HIS_IMP_MEST_MEDICINE resultData = null;
        //        if (valid && new HisImpMestMedicineLock(param).ChangeLock(data))
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
