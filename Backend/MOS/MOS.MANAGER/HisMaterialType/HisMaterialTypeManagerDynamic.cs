using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    public partial class HisMaterialTypeManager : BusinessBase
    {
        
        [Logger]
        public ApiResultObject<List<HisMaterialTypeViewDTO>> GetViewDynamic(HisMaterialTypeViewFilterQuery filter)
        {
            ApiResultObject<List<HisMaterialTypeViewDTO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialTypeViewDTO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetViewDynamic(filter);
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
