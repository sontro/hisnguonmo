using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    public partial class HisFormTypeCfgDataManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_FORM_TYPE_CFG_DATA>> GetView(HisFormTypeCfgDataViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_FORM_TYPE_CFG_DATA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_FORM_TYPE_CFG_DATA> resultData = null;
                if (valid)
                {
                    resultData = new HisFormTypeCfgDataGet(param).GetView(filter);
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
