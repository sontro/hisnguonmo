using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCertBook
{
    public partial class HisDeathCertBookManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEATH_CERT_BOOK>> GetView(HisDeathCertBookViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEATH_CERT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEATH_CERT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisDeathCertBookGet(param).GetView(filter);
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
    }
}
