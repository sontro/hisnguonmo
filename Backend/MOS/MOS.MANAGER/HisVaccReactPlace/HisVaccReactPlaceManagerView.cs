using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactPlace
{
    public partial class HisVaccReactPlaceManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACC_REACT_PLACE>> GetView(HisVaccReactPlaceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACC_REACT_PLACE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACC_REACT_PLACE> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccReactPlaceGet(param).GetView(filter);
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
