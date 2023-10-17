using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisReportTypeCat
{
    public partial class HisReportTypeCatManager : BusinessBase
    {
        public HisReportTypeCatManager()
            : base()
        {

        }
        
        public HisReportTypeCatManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_REPORT_TYPE_CAT>> Get(HisReportTypeCatFilterQuery filter)
        {
            ApiResultObject<List<HIS_REPORT_TYPE_CAT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REPORT_TYPE_CAT> resultData = null;
                if (valid)
                {
                    resultData = new HisReportTypeCatGet(param).Get(filter);
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
        public ApiResultObject<HIS_REPORT_TYPE_CAT> Create(HIS_REPORT_TYPE_CAT data)
        {
            ApiResultObject<HIS_REPORT_TYPE_CAT> result = new ApiResultObject<HIS_REPORT_TYPE_CAT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REPORT_TYPE_CAT resultData = null;
                if (valid && new HisReportTypeCatCreate(param).Create(data))
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
        public ApiResultObject<HIS_REPORT_TYPE_CAT> Update(HIS_REPORT_TYPE_CAT data)
        {
            ApiResultObject<HIS_REPORT_TYPE_CAT> result = new ApiResultObject<HIS_REPORT_TYPE_CAT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REPORT_TYPE_CAT resultData = null;
                if (valid && new HisReportTypeCatUpdate(param).Update(data))
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
        public ApiResultObject<HIS_REPORT_TYPE_CAT> ChangeLock(HIS_REPORT_TYPE_CAT data)
        {
            ApiResultObject<HIS_REPORT_TYPE_CAT> result = new ApiResultObject<HIS_REPORT_TYPE_CAT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REPORT_TYPE_CAT resultData = null;
                if (valid && new HisReportTypeCatLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_REPORT_TYPE_CAT data)
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
                    resultData = new HisReportTypeCatTruncate(param).Truncate(data);
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
