using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineType
{
    public partial class HisMedicineTypeManager : BusinessBase
    {
       
        [Logger]
        public ApiResultObject<List<HisMedicineTypeViewDTO>> GetViewDynamic(HisMedicineTypeViewFilterQuery filter)
        {
            ApiResultObject<List<HisMedicineTypeViewDTO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineTypeViewDTO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).GetViewDynamic(filter);
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
