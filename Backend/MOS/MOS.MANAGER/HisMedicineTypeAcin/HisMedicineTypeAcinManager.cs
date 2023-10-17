using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeAcin
{
    public partial class HisMedicineTypeAcinManager : BusinessBase
    {
        public HisMedicineTypeAcinManager()
            : base()
        {

        }

        public HisMedicineTypeAcinManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>> Get(HisMedicineTypeAcinFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_TYPE_ACIN> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeAcinGet(param).Get(filter);
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

        [Logger]
        public ApiResultObject<List<V_HIS_MEDICINE_TYPE_ACIN>> GetView(HisMedicineTypeAcinViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_TYPE_ACIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_TYPE_ACIN> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeAcinGet(param).GetView(filter);
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

        [Logger]
        public ApiResultObject<HIS_MEDICINE_TYPE_ACIN> Create(HIS_MEDICINE_TYPE_ACIN data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_ACIN> result = new ApiResultObject<HIS_MEDICINE_TYPE_ACIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE_ACIN resultData = null;
                if (valid && new HisMedicineTypeAcinCreate(param).Create(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>> CreateList(List<HIS_MEDICINE_TYPE_ACIN> data)
        {
            ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>> result = new ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDICINE_TYPE_ACIN> resultData = null;
                if (valid && new HisMedicineTypeAcinCreate(param).CreateList(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>> UpdateList(List<HIS_MEDICINE_TYPE_ACIN> data)
        {
            ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>> result = new ApiResultObject<List<HIS_MEDICINE_TYPE_ACIN>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDICINE_TYPE_ACIN> resultData = null;
                if (valid && new HisMedicineTypeAcinUpdate(param).UpdateList(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_MEDICINE_TYPE_ACIN> Update(HIS_MEDICINE_TYPE_ACIN data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_ACIN> result = new ApiResultObject<HIS_MEDICINE_TYPE_ACIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE_ACIN resultData = null;
                if (valid && new HisMedicineTypeAcinUpdate(param).Update(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_MEDICINE_TYPE_ACIN> ChangeLock(HIS_MEDICINE_TYPE_ACIN data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_ACIN> result = new ApiResultObject<HIS_MEDICINE_TYPE_ACIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE_ACIN resultData = null;
                if (valid && new HisMedicineTypeAcinLock(param).ChangeLock(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_MEDICINE_TYPE_ACIN data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMedicineTypeAcinTruncate(param).Truncate(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMedicineTypeAcinTruncate(param).TruncateList(ids);
                }
                result = this.PackSingleResult(resultData);
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
