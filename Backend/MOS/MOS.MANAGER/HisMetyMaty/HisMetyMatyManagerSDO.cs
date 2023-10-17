using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisMetyMaty
{
    public partial class HisMetyMatyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HIS_METY_MATY>> CreateSdo(HisMetyMatySDO data)
        {
            ApiResultObject<List<HIS_METY_MATY>> result = new ApiResultObject<List<HIS_METY_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_METY_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyCreateSDO(param).Create(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_METY_MATY>> CreateListSdo(List<HisMetyMatySDO> listData)
        {
            ApiResultObject<List<HIS_METY_MATY>> result = new ApiResultObject<List<HIS_METY_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_METY_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyCreateSDO(param).CreateList(listData, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

    }
}
