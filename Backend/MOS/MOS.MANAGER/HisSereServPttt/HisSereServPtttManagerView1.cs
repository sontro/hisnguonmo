using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServPttt
{
    public partial class HisSereServPtttManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERE_SERV_PTTT_1>> GetView1(HisSereServPtttView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_PTTT_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_PTTT_1> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServPtttGet(param).GetView1(filter);
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
