using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.Dashboard.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {
        public HisTreatmentManager()
            : base()
        {

        }

        public HisTreatmentManager(CommonParam param)
            : base(param)
        {

        }

        public ApiResultObject<List<TreatmentIcdDDO>> GetGeneralTopIcd(TreatmentIcdFilter filter)
        {
            ApiResultObject<List<TreatmentIcdDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<TreatmentIcdDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetTopIcd(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            this.Logger(result, filter);
            return result;
        }

        public ApiResultObject<List<TreatmentTimeDDO>> GetWithTime(TreatmentTimeFilter filter)
        {
            ApiResultObject<List<TreatmentTimeDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<TreatmentTimeDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetWithTime(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            this.Logger(result, filter);
            return result;
        }

        public ApiResultObject<List<TreatmentTimeAvgDDO>> GetTimeAvg(TreatmentTimeAvgFilter filter)
        {
            ApiResultObject<List<TreatmentTimeAvgDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<TreatmentTimeAvgDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetTimeAvg(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            this.Logger(result, filter);
            return result;
        }

        public ApiResultObject<List<TreatmentRegisterByTimeDDO>> GetRegisterByTime(TreatmentRegisterByTimeFilter filter)
        {
            ApiResultObject<List<TreatmentRegisterByTimeDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<TreatmentRegisterByTimeDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetRegisterByTime(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            this.Logger(result, filter);
            return result;
        }

    }
}
