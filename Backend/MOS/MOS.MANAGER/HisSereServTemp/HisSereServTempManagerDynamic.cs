using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServTemp
{
    public partial class HisSereServTempManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HisSereServTempDTO>> GetDynamic(HisSereServTempFilterQuery filter)
        {
            ApiResultObject<List<HisSereServTempDTO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisSereServTempDTO> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServTempGet(param).GetDynamic(filter);
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
