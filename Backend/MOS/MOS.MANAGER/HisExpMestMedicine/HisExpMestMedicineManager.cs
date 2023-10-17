using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine.IsUsed;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    public partial class HisExpMestMedicineManager : BusinessBase
    {
        public HisExpMestMedicineManager()
            : base()
        {

        }

        public HisExpMestMedicineManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST_MEDICINE>> Get(HisExpMestMedicineFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_MEDICINE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMedicineGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>> GetView(HisExpMestMedicineViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_MEST_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMedicineGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>> GetViewByTreatmentId(long treatmentId)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_EXP_MEST_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMedicineGet(param).GetViewByTreatmentId(treatmentId);
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
        public ApiResultObject<HIS_EXP_MEST_MEDICINE> UpdateCommonInfo(HIS_EXP_MEST_MEDICINE data)
        {
            ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_MEDICINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestMedicineUpdateCommonInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_MEDICINE> Used(long data)
        {
            ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_MEDICINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestMedicineUpdateIsUsed(param).Used(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_MEDICINE> Unused(long data)
        {
            ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_MEDICINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestMedicineUpdateIsUsed(param).Unused(data, ref resultData);
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
