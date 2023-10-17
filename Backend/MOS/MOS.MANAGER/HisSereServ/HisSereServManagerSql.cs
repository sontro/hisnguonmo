using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    public partial class HisSereServManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<DHisSereServ2>> GetDHisSereServ2(DHisSereServ2Filter filter)
        {
            ApiResultObject<List<DHisSereServ2>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<DHisSereServ2> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetDHisSereServ2(filter);
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
        public ApiResultObject<List<HIS_SERE_SERV>> GetExceedMinDuration(HisSereServMinDurationFilter filter)
        {
            ApiResultObject<List<HIS_SERE_SERV>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetExceedMinDuration(filter);
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
        public ApiResultObject<List<HIS_SERE_SERV>> GetRecentPttt(HisSereServRecentPtttFilter filter)
        {
            ApiResultObject<List<HIS_SERE_SERV>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetRecentPttt(filter);
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
        public ApiResultObject<HisSereServCountSDO> GetCount(HisSereServCountFilter filter)
        {
            ApiResultObject<HisSereServCountSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisSereServCountSDO resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetCount(filter);
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

    }
}
