using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyVolume
{
    public partial class HisBltyVolumeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BLTY_VOLUME>> GetView(HisBltyVolumeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BLTY_VOLUME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BLTY_VOLUME> resultData = null;
                if (valid)
                {
                    resultData = new HisBltyVolumeGet(param).GetView(filter);
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
