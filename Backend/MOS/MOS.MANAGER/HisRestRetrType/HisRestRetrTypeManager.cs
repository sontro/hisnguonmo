using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRestRetrType
{
    public partial class HisRestRetrTypeManager : BusinessBase
    {
        public HisRestRetrTypeManager()
            : base()
        {

        }

        public HisRestRetrTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_REST_RETR_TYPE>> Get(HisRestRetrTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_REST_RETR_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REST_RETR_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisRestRetrTypeGet(param).Get(filter);
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

        [Logger]
        public ApiResultObject<List<V_HIS_REST_RETR_TYPE>> GetView(HisRestRetrTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REST_RETR_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REST_RETR_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisRestRetrTypeGet(param).GetView(filter);
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

        [Logger]
        public ApiResultObject<HIS_REST_RETR_TYPE> Create(HIS_REST_RETR_TYPE data)
        {
            ApiResultObject<HIS_REST_RETR_TYPE> result = new ApiResultObject<HIS_REST_RETR_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REST_RETR_TYPE resultData = null;
                if (valid && new HisRestRetrTypeCreate(param).Create(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_REST_RETR_TYPE> Update(HIS_REST_RETR_TYPE data)
        {
            ApiResultObject<HIS_REST_RETR_TYPE> result = new ApiResultObject<HIS_REST_RETR_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REST_RETR_TYPE resultData = null;
                if (valid && new HisRestRetrTypeUpdate(param).Update(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_REST_RETR_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_REST_RETR_TYPE> result = new ApiResultObject<HIS_REST_RETR_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_REST_RETR_TYPE resultData = null;
                if (valid)
                {
                    new HisRestRetrTypeLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_REST_RETR_TYPE data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRestRetrTypeTruncate(param).Truncate(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> DeleteByRehaServiceTypeId(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRestRetrTypeTruncate(param).TruncateByRehaServiceTypeId(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> DeleteByRehaTrainTypeId(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRestRetrTypeTruncate(param).TruncateByRehaTrainTypeId(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_REST_RETR_TYPE>> CreateList(List<HIS_REST_RETR_TYPE> listData)
        {
            ApiResultObject<List<HIS_REST_RETR_TYPE>> result = new ApiResultObject<List<HIS_REST_RETR_TYPE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_REST_RETR_TYPE> resultData = null;
                if (valid && new HisRestRetrTypeCreate(param).CreateList(listData))
                {
                    resultData = listData;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_REST_RETR_TYPE>> UpdateList(List<HIS_REST_RETR_TYPE> listData)
        {
            ApiResultObject<List<HIS_REST_RETR_TYPE>> result = new ApiResultObject<List<HIS_REST_RETR_TYPE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_REST_RETR_TYPE> resultData = null;
                if (valid && new HisRestRetrTypeUpdate(param).UpdateList(listData))
                {
                    resultData = listData;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> DeleteList(List<long> Ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(Ids);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRestRetrTypeTruncate(param).TruncateList(Ids);
                }
                result = this.PackSingleResult(resultData);
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
