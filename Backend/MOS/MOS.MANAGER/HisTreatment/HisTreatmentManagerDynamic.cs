using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {

        [Logger]
        public ApiResultObject<List<HisTreatmentDTO>> GetDynamic(HisTreatmentFilterQuery filter)
        {
            ApiResultObject<List<HisTreatmentDTO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisTreatmentDTO> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetDynamic(filter);
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
