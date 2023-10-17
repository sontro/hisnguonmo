using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial.IsUsed;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMaterial
{
    public partial class HisExpMestMaterialManager : BusinessBase
    {
        public HisExpMestMaterialManager()
            : base()
        {

        }

        public HisExpMestMaterialManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST_MATERIAL>> Get(HisExpMestMaterialFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_MATERIAL>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMaterialGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>> GetView(HisExpMestMaterialViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_MEST_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMaterialGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>> GetViewByTreatmentId(long treatmentId)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_EXP_MEST_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMaterialGet(param).GetViewByTreatmentId(treatmentId);
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
        public ApiResultObject<HIS_EXP_MEST_MATERIAL> Used(long data)
        {
            ApiResultObject<HIS_EXP_MEST_MATERIAL> result = new ApiResultObject<HIS_EXP_MEST_MATERIAL>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_MATERIAL resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestMaterialUpdateIsUsed(param).Used(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST_MATERIAL> Unused(long data)
        {
            ApiResultObject<HIS_EXP_MEST_MATERIAL> result = new ApiResultObject<HIS_EXP_MEST_MATERIAL>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_MATERIAL resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestMaterialUpdateIsUsed(param).Unused(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
