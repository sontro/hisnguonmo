using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.Dashboard.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        public HisServiceReqManager()
            : base()
        {

        }

        public HisServiceReqManager(CommonParam param)
            : base(param)
        {

        }

        public ApiResultObject<List<ServiceReqGeneralByDepaDDO>> GetGeneralByDepartment(ServiceReqGeneralByDepaFilter filter)
        {
            ApiResultObject<List<ServiceReqGeneralByDepaDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<ServiceReqGeneralByDepaDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqGet(param).GetGeneralByDepartment(filter);
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

        public ApiResultObject<List<ServiceReqExamDateDDO>> GetGeneralExamDate(ServiceReqExamDateFilter filter)
        {
            ApiResultObject<List<ServiceReqExamDateDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<ServiceReqExamDateDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqGet(param).GetExamDate(filter);
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
