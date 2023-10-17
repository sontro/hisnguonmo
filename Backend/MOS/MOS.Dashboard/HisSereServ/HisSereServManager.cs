using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using MOS.Dashboard.HisSereServ;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.Dashboard.HisSereServ
{
    public partial class HisSereServManager : BusinessBase
    {
        public HisSereServManager()
            : base()
        {

        }

        public HisSereServManager(CommonParam param)
            : base(param)
        {

        }

        public ApiResultObject<List<SereServGeneralByDepaDDO>> GetGeneralByDepartment(SereServGeneralByDepaFilter filter)
        {
            ApiResultObject<List<SereServGeneralByDepaDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<SereServGeneralByDepaDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetGeneralByDepartment(filter);
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

        public ApiResultObject<List<SereServGeneralByDepaDDO>> GetGeneralTestByDepartment(SereServGeneralByDepaFilter filter)
        {
            ApiResultObject<List<SereServGeneralByDepaDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<SereServGeneralByDepaDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetGeneralTestByDepartment(filter);
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
