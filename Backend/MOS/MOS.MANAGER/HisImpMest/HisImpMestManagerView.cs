using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Aggr;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.MANAGER.HisImpMest.Manu;
using MOS.MANAGER.HisImpMest.Moba;
using MOS.MANAGER.HisImpMest.Other;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    public partial class HisImpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST_MANU>> GetManuView(HisImpMestManuViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MANU>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_MANU> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestGet(param).GetManuView(filter);
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
        public ApiResultObject<List<V_HIS_IMP_MEST_1>> GetView1(HisImpMestView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_1> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestGet(param).GetView1(filter);
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
        public ApiResultObject<List<V_HIS_IMP_MEST_2>> GetView2(HisImpMestView2FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_2>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_2> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestGet(param).GetView2(filter);
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
