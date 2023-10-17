using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServTein
{
    public class HisSereServTeinManager : BusinessBase
    {
        public HisSereServTeinManager()
            : base()
        {

        }

        public HisSereServTeinManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERE_SERV_TEIN>> Get(HisSereServTeinFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_TEIN>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                if (valid)
                {
                    HisSereServTeinGet getConcrete = new HisSereServTeinGet(param);
                    result = getConcrete.PackCollectionResult(getConcrete.Get(filter));
                }
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
        public ApiResultObject<List<V_HIS_SERE_SERV_TEIN>> GetView(HisSereServTeinViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_TEIN>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_TEIN> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServTeinGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_SERE_SERV_TEIN_1>> GetView1(HisSereServTeinView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_TEIN_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_TEIN_1> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServTeinGet(param).GetView1(filter);
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
        public ApiResultObject<TestMaterialByNormationCollectionSDO> GetMaterialAmountByNormation(HisSereServTeinAmountByNormationFilter filter)
        {
            ApiResultObject<TestMaterialByNormationCollectionSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                if (valid)
                {
                    HisSereServTeinGet getConcrete = new HisSereServTeinGet(param);
                    result = getConcrete.PackCollectionResult(getConcrete.GetMaterialAmountByNormation(filter));
                }
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
        public ApiResultObject<HIS_SERE_SERV_TEIN> Update(HIS_SERE_SERV_TEIN data)
        {
            ApiResultObject<HIS_SERE_SERV_TEIN> result = new ApiResultObject<HIS_SERE_SERV_TEIN>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_TEIN resultData = null;
                if (valid && new HisSereServTeinUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<bool> ChangeLock(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HisSereServTeinLock lockConcrete = new HisSereServTeinLock(param);
                    result = lockConcrete.PackSingleResult(lockConcrete.ChangeLock(id));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_SERE_SERV_TEIN data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisSereServTeinTruncate deleteConcrete = new HisSereServTeinTruncate(param);
                    result = deleteConcrete.PackSingleResult(deleteConcrete.Truncate(data));
                }
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
