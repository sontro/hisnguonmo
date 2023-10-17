using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<D_HIS_EXP_MEST_DETAIL_1>> GetExpMestDetail1(DHisExpMestDetail1Filter filter)
        {
            ApiResultObject<List<D_HIS_EXP_MEST_DETAIL_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<D_HIS_EXP_MEST_DETAIL_1> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGet(param).GetExpMestDetail1(filter);
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

        [Logger]
        public ApiResultObject<List<ExpMestTutorialSDO>> GetExpMestTutorial(HisExpMestTutorialFilter filter)
        {
            ApiResultObject<List<ExpMestTutorialSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<ExpMestTutorialSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGet(param).GetExpMestTutorial(filter);
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

        [Logger]
        public ApiResultObject<List<HisExpMestGroupByTreatmentSDO>> GetExpMestGroupByTreatment(HisExpMestGroupByTreatmentFilter filter)
        {
            ApiResultObject<List<HisExpMestGroupByTreatmentSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisExpMestGroupByTreatmentSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGet(param).GetExpMestGroupByTreatment(filter);
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

        [Logger]
        public ApiResultObject<HisExpMestBcsMoreInfoSDO> GetBcsMoreInfo(HisExpMestBcsMoreInfoFilter filter)
        {
            ApiResultObject<HisExpMestBcsMoreInfoSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisExpMestBcsMoreInfoSDO resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGet(param).GetBcsMoreInfo(filter);
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
